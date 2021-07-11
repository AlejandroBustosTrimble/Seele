using System;
using System.Collections.Generic;

namespace RAK.Fwk.Common.Mapper
{
    /// <summary>
    /// Entidad para marcar un Mapper. 
    /// </summary>
    public interface IMapper
    {
        /// <summary>
        /// Mapea desde una entidad TFrom a una TTo
        /// </summary>
        /// <typeparam name="TFrom">Tipo origen</typeparam>
        /// <typeparam name="TTo">Tipo destino</typeparam>
        /// <param name="entityFrom">Instancia de tipo origen</param>
        /// <param name="entityTo">Instancia de tipo destino</param>
        /// <returns></returns>
        TTo Map<TFrom, TTo>(TFrom entityFrom, object entityTo = null);

        /// <summary>
        /// Agrega la equivalencia entre 2 tipos
        /// </summary>
        /// <typeparam name="TFrom"></typeparam>
        /// <typeparam name="TTo"></typeparam>
        void AddTypeEquivalence<TFrom, TTo>();

        /// <summary>
        /// Retorna las equivalencias del InnerMapper
        /// </summary>
        Dictionary<Type, Type> GetInnerMapperEquivalences();

        /// <summary>
        /// Equivalencias
        /// </summary>
        Dictionary<Type, Type> TypeEquivalences { get; }
    }
}
