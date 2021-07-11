using RAK.Core.Service.Entity;
using RAK.Core.Service.Entity.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace RAK.Core.Service.Logic.Abstraction
{
    /// <summary>
    /// Interfaz de Logica de Entidades de Editables con vista basica
    /// </summary>
    /// <typeparam name="E"></typeparam>
    /// <typeparam name="LIE"></typeparam>
    /// <typeparam name="BIE"></typeparam>
    /// <typeparam name="LCE"></typeparam>
    /// <typeparam name="BCE"></typeparam>
    public interface IFullEditLogic<E, LIE, BIE, LCE, BCE> : IEditLogic<E, LIE, LCE>
        where E : class, IEntityBase
        where LIE : class, IListedItemEntity
        where BIE : class, IBasicEntity
        where LCE : class, IListedCriteriaEntity
        where BCE : class, IBasicCriteriaEntity
    {
        /// <summary>
        /// Obtiene la vista basica de listado de una entidad
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        ListEntity<BIE> GetBasic(BCE entity);
    }
}
