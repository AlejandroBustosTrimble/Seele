using RAK.Fwk.Common.Cross.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace RAK.Fwk.Common.Mapper
{
    /// <summary>
    /// Mapper por nombre de propiedades y por tipo base de cada propiedad
    /// </summary>
    /// <remarks>
    /// EJ: si tengo PersonaVM : IPersona (ID (int), Name (string), Friends (List<PersonaVM>) puedo mapear a
    /// PersonaEntity : IPersona (ID (int), Name (string), Friends (List<PersonaEntity>) si coinciden
    /// los tipos de datos y nombre de propiedad (datos simples) y en lo que son instancias de entidades complejas (Ej Friends)
    /// Si concide nombre de propiedad y tipo base (EJ: IPersona)
    /// </remarks>
    public class Mapper : IMapper
    {
        #region Const

        /*
        TODO_RAK: ver si hay posibilidad de sacar esto y resolverlo desde nhibernate, para que no tenga que hacer esto
        Hay que probarlo sin esto porque como hice que habra la sesion desde la logic, entonces no deberia hacer falta
        Igualmente si por algun motivo usara el mapper dentro de la logic, tendria en las entidades que recupere de 

        */
        private const string IPROXY_NH_TYPE_FULLNAME = "NHibernate.Proxy.DynamicProxy.IProxy";

        #endregion

        #region Constructor

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mustEmptyEquivalences">Indica si vacia las equivalencias despues de mapear la entidad</param>
        public Mapper(bool mustEmptyEquivalences = false)
        {
            this.mustEmptyEquivalences = mustEmptyEquivalences;
        }

        #endregion

        #region Members

        private bool mustEmptyEquivalences;

        private Dictionary<Type, Type> typeEquivalences;

        private Dictionary<Object, Object> objsAlreadyMapped;

        #endregion

        #region Properties

        /// <summary>
        /// Equivalencias de tipos
        /// </summary>
        public Dictionary<Type, Type> TypeEquivalences
        {
            get
            {

                if (this.typeEquivalences == null)
                {
                    this.typeEquivalences = new Dictionary<Type, Type>();
                }

                return this.typeEquivalences;
            }
        }

        /// <summary>
        /// Objetos que ya fueron mapeados
        /// (Key ObjetoFrom, ValueObjetoTo)
        /// Ej Key=ParamVM instancia, Value=Param instancia
        /// </summary>
        protected Dictionary<Object, Object> ObjsAlreadyMapped
        {
            get
            {
                if (this.objsAlreadyMapped == null)
                {
                    this.objsAlreadyMapped = new Dictionary<object, object>();
                }

                return objsAlreadyMapped;
            }
        }

        #endregion

        #region Methods

        #region Public

        /// <summary>
        /// Realiza un mapeo 
        /// </summary>
        /// <typeparam name="TFrom"></typeparam>
        /// <typeparam name="TTo"></typeparam>
        /// <param name="entityFrom"></param>
        /// <returns></returns>
        public TTo Map<TFrom, TTo>(TFrom entityFrom, object entityTo = null)
        {
            this.AddTypeEquivalence<TFrom, TTo>();

            entityTo = this.Map(entityFrom, entityTo);

            this.ObjsAlreadyMapped.Clear();

            if (this.mustEmptyEquivalences)
                this.ClearTypeEquivalences();

            return (TTo)entityTo;
        }

        /// <summary>
        /// Agrega una tipo y su equivalencia
        /// </summary>
        /// <typeparam name="TFrom"></typeparam>
        /// <typeparam name="TTo"></typeparam>
        public virtual void AddTypeEquivalence<TFrom, TTo>()
        {
            this.AddTypeEquivalence(typeof(TFrom), typeof(TTo));
        }

        /// <summary>
        /// Agrega un tipo y su equivalencia
        /// </summary>
        /// <param name="typeFrom"></param>
        /// <param name="typeTo"></param>
        public virtual void AddTypeEquivalence(Type typeFrom, Type typeTo)
        {
            if (!this.TypeEquivalences.ContainsKey(typeFrom))
            {
                this.TypeEquivalences.Add(typeFrom, typeTo);
            }
        }

        /// <summary>
        /// Limpia las equivalencias
        /// </summary>
        private void ClearTypeEquivalences()
        {
            this.TypeEquivalences.Clear();
        }

        #endregion

        #region Protected

        /// <summary>
        /// Ejecuto el mapeo de una entidad
        /// </summary>
        /// <param name="entityFrom"></param>
        /// <returns></returns>
        protected Object Map(Object entityFrom, Object entityTo = null, Type entityFromOriginalType = null, Type pPropToType = null)
        {
            if (entityFrom == null)
            {
                return null;
            }
            if (this.ObjsAlreadyMapped.ContainsKey(entityFrom))
            {
                return this.ObjsAlreadyMapped[entityFrom];
            }
            else
            {
                Type entityFromType;
                if (entityFromOriginalType != null)
                {
                    entityFromType = entityFromOriginalType;
                }
                else
                {
                    entityFromType = entityFrom.GetType();

                    // Comparo asi para no tener referencias a NHibernate aca
                    if (entityFromType.GetInterfaces() != null && entityFromType.GetInterfaces().Any(i => i.FullName == IPROXY_NH_TYPE_FULLNAME))
                    {
                        // Si es un proxy NH entonces obtengo el base type que es la clase posta que quiero usar
                        entityFromType = entityFromType.BaseType;
                    }
                }

                if (entityTo == null)
                {
                    if (this.HasTypeEquivalence(entityFromType) || pPropToType == null)
                    {
                        entityTo = this.CreateInstance(entityFromType);
                    }
                    else
                    {
                        entityTo = this.CreateInstance(pPropToType);
                    }
                }


                if (!TypesHelper.IsSimpleType(entityFromType) && !this.ObjsAlreadyMapped.ContainsKey(entityFrom))
                {
                    this.ObjsAlreadyMapped.Add(entityFrom, entityTo);
                }

                if (!TypesHelper.IsSimpleType(entityFrom.GetType()) && entityTo != null)
                {
                    var entityFromProperties = entityFrom.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.GetProperty);

                    var entityToProperties = entityTo.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.SetProperty | BindingFlags.GetProperty).Where(e => e.CanRead && e.CanWrite);

                    // -- Recorro las propiedad Get de la entidadFrom y las seteo en su equivalente Set en la entidadTo
                    // -- A su vez si encuentro propiedades de tipo complejos (Referencia a clases propias, etc) realizo su mapeo
                    foreach (PropertyInfo propFrom in entityFromProperties)
                    {
                        // -- Me interesa mapear las propiedades que no tienen parametros, para evitar aquellas que son listas
                        if (propFrom.GetGetMethod().GetParameters().Count() > 0)
                        {
                            continue;
                        }

                        var propFromValue = propFrom.GetValue(entityFrom, null);
                        if (propFromValue != null)
                        {
                            // TODO_RAK: si en un futuro quiero agregar alias (para que mapee objFrom.NameX a objTo.NameY, donde 
                            // varia el nombre de la propiedad, puedo agregar una lista de alias [objFrom.Namex->objTo.NameY] y cambiar este Where incluyendo
                            // un Contains sobre esa lista

                            PropertyInfo propTo = null;

                            var isSimpleType = TypesHelper.IsSimpleType(propFrom.PropertyType);

                            #region Obtiene la propiedad destino

                            if (isSimpleType)
                            {
                                propTo = entityToProperties.Where(p =>
                                                            p.Name == propFrom.Name &&
                                                            p.PropertyType == propFrom.PropertyType).FirstOrDefault();
                            }
                            else if (TypesHelper.IsExpressionType(propFrom.PropertyType))
                            {
                                // -- Si es una expresion busco su equivalente en la entidadTo
                                propTo = entityToProperties.Where(x =>
                                                        x.Name == propFrom.Name &&
                                                        x.PropertyType.BaseType == propFrom.PropertyType.BaseType
                                         ).FirstOrDefault();
                            }
                            else if (TypesHelper.IsListType(propFrom.PropertyType))
                            {
                                Type listType;
                                if (propFrom.PropertyType == typeof(Array))
                                {
                                    Array arrayFrom = (propFromValue as Array);
                                    // -- Registro como un tipo el tipo equivalente de lista (Array[VMEntity] -> Array<Entity>)
                                    var elementFromType = propFromValue.GetType().GetElementType();
                                    var elementToType = this.GetTypeEquivalence(elementFromType);
                                    var arrayTo = Array.CreateInstance(elementToType, arrayFrom.Length);
                                    listType = arrayTo.GetType();
                                }
                                else if (propFromValue is System.Collections.IDictionary)
                                {
                                    // -- Registro como un tipo el tipo equivalente de lista (Dictionary<Key,VMEntity> -> Dictionary<Key,Entity>)
                                    var genericArgInListTypes = propFromValue.GetType().GetGenericArguments();
                                    var keyFromType = genericArgInListTypes[0];
                                    var elementFromType = genericArgInListTypes[1];
                                    var keyToType = this.GetTypeEquivalence(keyFromType);
                                    var elementToType = this.GetTypeEquivalence(elementFromType);

                                    listType = typeof(Dictionary<,>)
                                                        .MakeGenericType(new Type[] { keyToType, elementToType });
                                }
                                else
                                {
                                    // -- Registro como un tipo el tipo equivalente de lista (List<VMEntity> -> List<Entity>)
                                    var genericArgInListTypes = propFromValue.GetType().GetGenericArguments();
                                    var elementFromType = genericArgInListTypes[0];
                                    var elementToType = this.GetTypeEquivalence(elementFromType);

                                    listType = typeof(List<>)
                                                        .MakeGenericType(elementToType);
                                }

                                propTo = entityToProperties.Where(p => p.Name == propFrom.Name &&
                                                         p.PropertyType.IsAssignableFrom(listType)
                                            ).FirstOrDefault();



                                if (propTo == null)
                                {
                                    // -- Si es una lista, y no encontre la propiedadTo igualando nombre y tipo, lo busco solo por nombre
                                    propTo = entityToProperties.Where(p => p.Name == propFrom.Name
                                            ).FirstOrDefault();
                                }
                            }
                            else if (propFrom.PropertyType.IsInterface)
                            {
                                // -- Matchea por nombre de propiedad e interfaz implementada
                                var interfacesFrom = TypesHelper.GetPrimaryInterfaces(propFrom.PropertyType);

                                propTo = entityToProperties.Where(p =>
                                    p.Name == propFrom.Name &&
                                    TypesHelper.GetPrimaryInterfaces(p.PropertyType).Any(f => interfacesFrom.Contains(f))).FirstOrDefault();
                            }
                            else
                            {
                                // -- Matchea solo por nombre de propiedad cuando es una propiedad compleja y concreta
                                propTo = entityToProperties.Where(p => p.Name == propFrom.Name).FirstOrDefault();
                            }

                            #endregion

                            if (propTo != null)
                            {
                                var propToValue = this.MapValue(propFromValue, propFrom.PropertyType, propTo.PropertyType);

                                if (propToValue != null)
                                {
                                    propTo.SetValue(entityTo, propToValue, null);
                                }
                            }
                        }
                    }
                }
                else
                {
                    entityTo = entityFrom;
                }

                return entityTo;
            }
        }

        /// <summary>
        /// Mapeo un valor (Simple, lista, array, entidad compleja, etc)
        /// </summary>
        /// <param name="propFromValue"></param>
        /// <returns></returns>
        protected Object MapValue(Object propFromValue, Type propertyFromType, Type propToType)
        {
            if (propFromValue != null)
            {
                if (TypesHelper.IsSimpleType(propFromValue.GetType()))
                {
                    return propFromValue;
                }
                else if (TypesHelper.IsListType(propFromValue.GetType()))
                {
                    #region Mapeo Listas

                    if (propFromValue is Array)
                    {
                        Array arrayFrom = (propFromValue as Array);
                        // -- Registro como un tipo el tipo equivalente de lista (Array[VMEntity] -> Array<Entity>)
                        var elementFromType = propFromValue.GetType().GetElementType();
                        Type elementToType = null;
                        if (this.HasTypeEquivalence(elementFromType))
                        {
                            elementToType = this.GetTypeEquivalence(elementFromType);
                        }
                        else
                        {
                            // -- Registro como un tipo el tipo equivalente de lista (Array[VMEntity] -> Array<Entity>)
                            elementToType = propToType.GetElementType();

                        }

                        var arrayTo = Array.CreateInstance(elementToType, arrayFrom.Length);
                        var arrayToType = arrayTo.GetType();

                        this.AddTypeEquivalence(arrayFrom.GetType(), arrayToType);

                        Object mappedList = this.CreateInstance(propFromValue.GetType(), arrayFrom.Length);
                        Array toArray = mappedList as Array;

                        for (int i = 0; i < arrayFrom.Length; i++)
                        {
                            var valueFromArray = arrayFrom.GetValue(i);

                            var valueToArray = this.Map(valueFromArray, null, null, elementToType);

                            arrayTo.SetValue(valueToArray, i);
                        }

                        return arrayTo;

                    }
                    else
                    {
                        Object mappedList = null;

                        var propFromValueDictionary = (propFromValue as System.Collections.IDictionary);

                        if (propFromValueDictionary != null)
                        {
                            // -- Registro como un tipo el tipo equivalente de lista (Dictionary<Key,VMEntity> -> Dictionary<Key,Entity>)
                            var genericArgInListTypes = propFromValue.GetType().GetGenericArguments();
                            var keyFromType = genericArgInListTypes[0];
                            var elementFromType = genericArgInListTypes[1];

                            var propToGenericArgInListTypes = propToType.GetGenericArguments();
                            var propToKeyType = propToGenericArgInListTypes[0];
                            var propToElementType = propToGenericArgInListTypes[1];
                            Type keyToType = null;
                            if (this.HasTypeEquivalence(elementFromType))
                            {
                                keyToType = this.GetTypeEquivalence(keyFromType);
                            }
                            else
                            {
                                keyToType = propToKeyType;
                            }

                            Type elementToType = null;
                            if (this.HasTypeEquivalence(elementFromType))
                            {
                                elementToType = this.GetTypeEquivalence(elementFromType);
                            }
                            else
                            {
                                elementToType = propToElementType;
                            }

                            var listToType = typeof(Dictionary<,>)
                                                .MakeGenericType(new Type[] { keyToType, elementToType });

                            this.AddTypeEquivalence(propFromValue.GetType(), listToType);

                            mappedList = this.CreateInstance(propFromValue.GetType());

                            foreach (var key in propFromValueDictionary.Keys)
                            {
                                var mappedItem = this.Map(propFromValueDictionary[key], null, null, elementToType);

                                (mappedList as System.Collections.IDictionary).Add(key, mappedItem);
                            }
                        }
                        else
                        {
                            // -- Registro como un tipo el tipo equivalente de lista (List<VMEntity> -> List<Entity>)
                            var genericArgInListTypes = propFromValue.GetType().GetGenericArguments();
                            var propToGenericArgInListTypes = propToType.GetGenericArguments();
                            var elementFromType = genericArgInListTypes[0];
                            var propToElementType = propToGenericArgInListTypes[0];

                            Type elementToType = null;
                            if (this.HasTypeEquivalence(elementFromType))
                            {
                                elementToType = this.GetTypeEquivalence(elementFromType);
                            }
                            else
                            {
                                elementToType = propToElementType;
                            }

                            var listToType = typeof(List<>)
                                                .MakeGenericType(elementToType);

                            this.AddTypeEquivalence(propFromValue.GetType(), listToType);

                            mappedList = this.CreateInstance(propFromValue.GetType());

                            var propFromValueList = (propFromValue as System.Collections.IList);

                            foreach (var item in propFromValueList)
                            {
                                var mappedItem = this.Map(item, null, null, elementToType);

                                (mappedList as System.Collections.IList).Add(mappedItem);
                            }
                        }

                        return mappedList;
                    }

                    #endregion
                }
                else if (TypesHelper.IsExpressionType(propFromValue.GetType()))
                {
                    // Si es una expresion tomo el parametro que tenga, encuentro su equivalente
                    // y llamo a convertir la expresion con el nuevo parametro
                    var expFrom = propFromValue as System.Linq.Expressions.LambdaExpression;
                    var param = expFrom.Parameters.FirstOrDefault();
                    var typeFrom = param.Type;
                    var typeTo = this.GetTypeEquivalence(typeFrom);

                    // TODO_RAK: falta obtener los valores para PropTo

                    var expTo = ExpressionHelper.ChangeParameter(expFrom, typeFrom, typeTo);

                    return expTo;
                }
                else
                {
                    // -- Mapea entidades complejas
                    var mappedValue = this.Map(propFromValue, null, propertyFromType, propToType);

                    return mappedValue;
                }
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Crea una instancia
        /// </summary>
        /// <remarks>
        /// Si para el tipo que quiero crear una instancia tengo una equivalencia para ese tipo instancia la equivalencia
        /// </remarks>
        /// <param name="type"></param>
        /// <returns></returns>
        protected Object CreateInstance(Type type, params object[] args)
        {
            var typeToInstantiate = this.GetTypeEquivalence(type);

            Object obj = null;
            try
            {
                var emptyCtor = type.GetConstructor(Type.EmptyTypes);
                if (emptyCtor != null)
                {
                    obj = Activator.CreateInstance(typeToInstantiate, args);
                }
                else
                {
                    obj = null;
                }
            }
            catch (MissingMethodException)
            {
                obj = null;
            }
            return obj;
        }

        /// <summary>
        /// Obtiene la equivalencia de un tipo
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        protected virtual Type GetTypeEquivalence(Type type)
        {
            // -- Si el tipo que quiero instanciar tiene un equivalente, entonces instancio ese
            var typeToInstantiate = this.HasTypeEquivalence(type) ? this.TypeEquivalences[type] : type;

            return typeToInstantiate;
        }

        /// <summary>
        /// Obtiene si tiene equivalencia
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        protected bool HasTypeEquivalence(Type type)
        {
            return this.TypeEquivalences.ContainsKey(type);
        }

        public Dictionary<Type, Type> GetInnerMapperEquivalences()
        {
            return this.TypeEquivalences;
        }

        #endregion

        #endregion
    }
}
