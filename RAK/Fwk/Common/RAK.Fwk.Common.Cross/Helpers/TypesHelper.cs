using System;
using System.Linq;
using System.Linq.Expressions;

namespace RAK.Fwk.Common.Cross.Helpers
{
    /// <summary>
    /// Helper para los tipos
    /// </summary>
    public static class TypesHelper
    {
        /// <summary>
        /// Obtiene si es una lista
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static Boolean IsListType(Type type)
        {
            return type.GetInterfaces().Any(f => f == typeof(System.Collections.IEnumerable));
        }

        /// <summary>
        /// Obtiene si es una lista
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static Boolean IsSimpleType(Type type)
        {
            if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>))
            {
                // nullable type, check if the nested type is simple.
                return IsSimpleType((type.GetGenericArguments()[0]));
            }
            return type.IsPrimitive
              || type.IsEnum
              || type.Equals(typeof(DateTime))
              || type.Equals(typeof(DateTime?))
              || type.Equals(typeof(Guid))
              || type.Equals(typeof(string))
              || type.Equals(typeof(bool))
              || type.Equals(typeof(double))
              || type.Equals(typeof(int))
              || type.Equals(typeof(decimal));
        }

        /// <summary>
        /// Obtiene si es una expresion
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static Boolean IsExpressionType(Type type)
        {
            var expressionType = typeof(System.Linq.Expressions.LambdaExpression);
            return (type == expressionType || type.BaseType == expressionType);
        }

        /// <summary>
        /// Obtiene las interfaces primarias (las que implementa ese tipo, sin tener en cuenta las que vienen por herencia)
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static Type[] GetPrimaryInterfaces(Type type)
        {
            //get all the interfaces for this type
            var interfaces = type.GetInterfaces();

            //get all the interfaces for the ancestor interfaces
            var baseInterfaces = interfaces.SelectMany(i => i.GetInterfaces());

            //filter based on only the direct interfaces
            var directInterfaces = interfaces.Where(i => baseInterfaces.All(b => b != i));

            return directInterfaces.ToArray();
        }

        /// <summary>
        /// A static method to get the Propertyname String of a Property
        /// It eliminates the need for "Magic Strings" and assures type safety when renaming properties.
        /// See: http://stackoverflow.com/questions/2820660/get-name-of-property-as-a-string
        /// </summary>
        /// <example>
        /// // Static Property
        /// string name = PropertyNameHelper.GetPropertyName(() => SomeClass.SomeProperty);
        /// // Instance Property
        /// string name = PropertyNameHelper.GetPropertyName(() => someObject.SomeProperty);
        /// </example>
        /// <typeparam name="T"></typeparam>
        /// <param name="propertyLambda"></param>
        /// <returns></returns>
        public static string GetPropertyName<T>(Expression<Func<T>> propertyLambda)
        {
            var me = propertyLambda.Body as MemberExpression;

            if (me == null)
            {
                throw new ArgumentException("You must pass a lambda of the form: '() => Class.Property' or '() => object.Property'");
            }

            return me.Member.Name;
        }
        /// <summary>
        /// Another way to get Instance Property names as strings.
        /// With this method you don't need to create a instance first.
        /// See the example.
        /// See: https://handcraftsman.wordpress.com/2008/11/11/how-to-get-c-property-names-without-magic-strings/
        /// </summary>
        /// <example>
        /// string name = PropertyNameHelper((Firma f) => f.Firmenumsatz_Waehrung);
        /// </example>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TReturn"></typeparam>
        /// <param name="expression"></param>
        /// <returns></returns>
        public static string GetPropertyName<T, TReturn>(Expression<Func<T, TReturn>> expression)
        {
            MemberExpression body = (MemberExpression)expression.Body;
            return body.Member.Name;
        }

        /// <summary>
        /// Construye el LazyLoad para una propiedad (get), contra un member.
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <param name="pEntity">Entidad</param>
        public static T BuildLazyLoad<T>(T pEntity)
        where T : class, new()
        {
            if (pEntity == null)
            {
                pEntity = default(T);
            }
            return pEntity;
        }

        /// <summary>
        /// Obtiene el valor por defecto de un tipo
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static object GetDefaultValue(Type type)
        {
            // Validate parameters.
            if (type == null) throw new ArgumentNullException("type");

            // We want an Func<object> which returns the default.
            // Create that expression here.
            Expression<Func<object>> e = Expression.Lambda<Func<object>>(
                // Have to convert to object.
                Expression.Convert(
                    // The default value, always get what the *code* tells us.
                    Expression.Default(type), typeof(object)
                )
            );

            // Compile and return the value.
            return e.Compile()();
        }
    }
}
