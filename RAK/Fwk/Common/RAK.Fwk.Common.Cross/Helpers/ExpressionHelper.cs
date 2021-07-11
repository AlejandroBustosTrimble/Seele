using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace RAK.Fwk.Common.Cross.Helpers
{
    /// <summary>
    /// Helper para expressiones Lamda
    /// </summary>
    public static class ExpressionHelper
    {
        #region Const

        public static Expression<Func<T, bool>> BaseAnd<T>() { return f => true; }
        public static Expression<Func<T, bool>> BaseOr<T>() { return f => false; }

        #endregion

        #region Methods

        /// <summary>
        /// Cambia en la expresion "x => x.SomeProp etc" donde x es del tipo X por la misma expresion pero x se lleva
        /// al tipo Y en tipos de Expressiones Func<TObject, bool>
        /// </summary>
        /// <typeparam name="TFrom"></typeparam>
        /// <typeparam name="TTo"></typeparam>
        /// <param name="from"></param>
        /// <returns></returns>
        public static Expression<Func<TTo, bool>> ChangeParameter<TFrom, TTo>(
            this Expression<Func<TFrom, bool>> from)
        {
            if (from == null) return null;

            return ChangeParam<Func<TFrom, bool>, Func<TTo, bool>>(from);
        }

        /// <summary>
        /// Cambia en la expresion "x => x.SomeProp etc" donde x es del tipo X por la misma expresion pero x se lleva
        /// al tipo Y en cualquier expresion lambda
        /// </summary>
        /// <param name="exp"></param>
        /// <param name="typeFrom"></param>
        /// <param name="typeTo"></param>
        /// <returns></returns>
        public static LambdaExpression ChangeParameter(this LambdaExpression exp, Type typeFrom, Type typeTo)
        {
            if (exp == null) return null;

            return ChangeGeneralParam(exp, typeFrom, typeTo);
        }

        /// <summary>
        /// Combina 2 expresiones por medio del operador OR
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="expr1"></param>
        /// <param name="expr2"></param>
        /// <returns></returns>
        public static Expression<Func<T, bool>> CombineByOr<T>(
            this Expression<Func<T, bool>> expr1,
            Expression<Func<T, bool>> expr2)
        {
            var secondBody = expr2.Body.Replace(expr2.Parameters[0], expr1.Parameters[0]);
            return Expression.Lambda<Func<T, bool>>
                  (Expression.OrElse(expr1.Body, secondBody), expr1.Parameters);
        }

        /// <summary>
        /// COmbina 2 expresiones por medio del operador AND
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="expr1"></param>
        /// <param name="expr2"></param>
        /// <returns></returns>
        public static Expression<Func<T, bool>> CombineByAnd<T>(
            this Expression<Func<T, bool>> expr1,
            Expression<Func<T, bool>> expr2)
        {
            var secondBody = expr2.Body.Replace(expr2.Parameters[0], expr1.Parameters[0]);
            return Expression.Lambda<Func<T, bool>>
                  (Expression.AndAlso(expr1.Body, secondBody), expr1.Parameters);
        }

        /// <summary>
        /// Reemplaza en una expresion, determinada subExpresion por otra
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="searchEx"></param>
        /// <param name="replaceEx"></param>
        /// <returns></returns>
        public static Expression Replace(this Expression expression,
        Expression searchEx, Expression replaceEx)
        {
            return new ReplaceVisitor(searchEx, replaceEx).Visit(expression);
        }

        /// <summary>
        /// Combina expresiones del tipo AND y OR en una existente
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="combinedPredicate"></param>
        /// <param name="andPredicate"></param>
        /// <param name="orPredicate"></param>
        /// <returns></returns>
        public static Expression<Func<T, bool>> CombineOrPredicatesWithAndPredicates<T>(this Expression<Func<T, bool>> combinedPredicate,
            Expression<Func<T, bool>> andPredicate, Expression<Func<T, bool>> orPredicate)
        {
            combinedPredicate = combinedPredicate ?? BaseAnd<T>();
            if (andPredicate != null && orPredicate != null)
            {
                andPredicate = andPredicate.CombineByAnd(orPredicate);
                combinedPredicate = combinedPredicate.CombineByAnd(andPredicate);
            }
            else if (orPredicate != null)
            {
                combinedPredicate = combinedPredicate.CombineByAnd(orPredicate);
            }
            else
            {
                combinedPredicate = combinedPredicate.CombineByAnd(andPredicate);
            }
            return combinedPredicate;
        }

        /// <summary>
        /// Combina expresiones del tipo AND y OR (Dependiendo de isAND) en newExpression
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="andPredicate"></param>
        /// <param name="orPredicate"></param>
        /// <param name="newExpression"></param>
        /// <param name="isAnd"></param>
        public static void AddToPredicateTypeBasedOnIfAndOrOr<T>(ref Expression<Func<T, bool>> andPredicate,
            ref Expression<Func<T, bool>> orPredicate, Expression<Func<T, bool>> newExpression, bool isAnd)
        {
            if (isAnd)
            {
                andPredicate = andPredicate ?? BaseAnd<T>();
                andPredicate = andPredicate.CombineByAnd(newExpression);
            }
            else
            {
                orPredicate = orPredicate ?? BaseOr<T>();
                orPredicate = orPredicate.CombineByOr(newExpression);
            }
        }

        #region Protected

        #region General

        /// <summary>
        /// Cambia en la expresion "x => x.SomeProp etc" donde x es del tipo X por la misma expresion pero x se lleva
        /// al tipo Y
        /// </summary>
        /// <typeparam name="TFrom"></typeparam>
        /// <typeparam name="TTo"></typeparam>
        /// <param name="from"></param>
        /// <returns></returns>
        private static LambdaExpression ChangeGeneralParam(LambdaExpression exp, Type typeFrom, Type typeTo)
        {
            // figure out which types are different in the function-signature

            Dictionary<Type, Type> typeMap = new Dictionary<Type, Type>();
            typeMap.Add(typeFrom, typeTo);

            // re-map all parameters that involve different types
            Dictionary<Expression, Expression> parameterMap = new Dictionary<Expression, Expression>();
            ParameterExpression[] newParams = GenerateGeneralParameterMap(exp, typeMap, parameterMap);

            // rebuild the lambda
            var body = new TypeConversionVisitor<Object>(parameterMap).Visit(exp.Body);
            return Expression.Lambda(body, newParams);
        }

        private static ParameterExpression[] GenerateGeneralParameterMap(
            LambdaExpression exp,
            Dictionary<Type, Type> typeMap,
            Dictionary<Expression, Expression> parameterMap
        )
        {
            var newParams = new ParameterExpression[exp.Parameters.Count];

            for (int i = 0; i < newParams.Length; i++)
            {
                Type newType;
                if (typeMap.TryGetValue(exp.Parameters[i].Type, out newType))
                {
                    parameterMap[exp.Parameters[i]] = newParams[i] = Expression.Parameter(newType, exp.Parameters[i].Name);
                }
            }
            return newParams;
        }

        #endregion

        #region Generic

        /// <summary>
        /// Cambia en la expresion "x => x.SomeProp etc" donde x es del tipo X por la misma expresion pero x se lleva
        /// al tipo Y
        /// </summary>
        /// <typeparam name="TFrom"></typeparam>
        /// <typeparam name="TTo"></typeparam>
        /// <param name="from"></param>
        /// <returns></returns>
        private static Expression<TTo> ChangeParam<TFrom, TTo>(Expression<TFrom> from)
            where TFrom : class
            where TTo : class
        {
            // figure out which types are different in the function-signature

            var fromTypes = from.Type.GetGenericArguments();
            var toTypes = typeof(TTo).GetGenericArguments();

            if (fromTypes.Length != toTypes.Length)
                throw new NotSupportedException("Incompatible lambda function-type signatures");

            Dictionary<Type, Type> typeMap = new Dictionary<Type, Type>();
            for (int i = 0; i < fromTypes.Length; i++)
            {
                if (fromTypes[i] != toTypes[i])
                    typeMap[fromTypes[i]] = toTypes[i];
            }

            // re-map all parameters that involve different types
            Dictionary<Expression, Expression> parameterMap = new Dictionary<Expression, Expression>();
            ParameterExpression[] newParams = GenerateParameterMap<TFrom>(from, typeMap, parameterMap);

            // rebuild the lambda
            var body = new TypeConversionVisitor<TTo>(parameterMap).Visit(from.Body);
            return Expression.Lambda<TTo>(body, newParams);
        }

        /// <summary>
        /// Genera el mapeo del parametro X a Y
        /// </summary>
        /// <typeparam name="TFrom"></typeparam>
        /// <param name="from"></param>
        /// <param name="typeMap"></param>
        /// <param name="parameterMap"></param>
        /// <returns></returns>
        private static ParameterExpression[] GenerateParameterMap<TFrom>(
            Expression<TFrom> from,
            Dictionary<Type, Type> typeMap,
            Dictionary<Expression, Expression> parameterMap
        )
            where TFrom : class
        {
            var newParams = new ParameterExpression[from.Parameters.Count];

            for (int i = 0; i < newParams.Length; i++)
            {
                Type newType;
                if (typeMap.TryGetValue(from.Parameters[i].Type, out newType))
                {
                    parameterMap[from.Parameters[i]] = newParams[i] = Expression.Parameter(newType, from.Parameters[i].Name);
                }
            }
            return newParams;
        }

        #endregion

        #endregion

        #endregion

        #region InnerClasses

        /// <summary>
        /// Navegador del arbol de una expresion
        /// </summary>
        /// <typeparam name="T"></typeparam>
        internal class TypeConversionVisitor<T> : ExpressionVisitor
        {
            #region Members

            private readonly Dictionary<Expression, Expression> parameterMap;

            #endregion

            #region Constructors

            /// <summary>
            /// Ctor
            /// </summary>
            /// <param name="parameterMap"></param>
            public TypeConversionVisitor(Dictionary<Expression, Expression> parameterMap)
            {
                this.parameterMap = parameterMap;
            }

            #endregion

            #region Methods

            /// <summary>
            /// Visita un parametro de la expresion
            /// </summary>
            /// <param name="node"></param>
            /// <returns></returns>
            protected override Expression VisitParameter(ParameterExpression node)
            {
                // re-map the parameter
                Expression found;
                if (!parameterMap.TryGetValue(node, out found))
                    found = base.VisitParameter(node);
                return found;
            }

            /// <summary>
            /// Visita un nodo
            /// </summary>
            /// <param name="node"></param>
            /// <returns></returns>
            public override Expression Visit(Expression node)
            {
                LambdaExpression lambda = node as LambdaExpression;
                if (lambda != null && !parameterMap.ContainsKey(lambda.Parameters.First()))
                {
                    return new TypeConversionVisitor<T>(parameterMap).Visit(lambda.Body);
                }
                return base.Visit(node);
            }

            /// <summary>
            /// Visita una variable
            /// </summary>
            /// <param name="node"></param>
            /// <returns></returns>
            protected override Expression VisitMember(MemberExpression node)
            {
                // re-perform any member-binding
                var expr = Visit(node.Expression);
                if (expr != null && expr.Type != node.Type)
                {
                    if (expr.Type.GetMember(node.Member.Name).Any())
                    {
                        MemberInfo newMember = expr.Type.GetMember(node.Member.Name).FirstOrDefault();
                        return Expression.MakeMemberAccess(expr, newMember);
                    }
                }
                return base.VisitMember(node);
            }

            #endregion
        }


        class ReplaceVisitor : ExpressionVisitor
        {
            private readonly Expression from, to;

            public ReplaceVisitor(Expression from, Expression to)
            {
                this.from = from;
                this.to = to;
            }

            public override Expression Visit(Expression node)
            {
                return node == from ? to : base.Visit(node);
            }
        }

        #endregion
    }
}
