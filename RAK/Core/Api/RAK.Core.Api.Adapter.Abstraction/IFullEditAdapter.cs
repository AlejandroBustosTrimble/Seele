using RAK.Core.Api.Model.Interfaces;

namespace RAK.Core.Api.Adapter.Abstraction
{
    /// <summary>
    /// Interfaz de Adapter de Entidades Editable con vista basica
    /// </summary>
    /// <typeparam name="VM"></typeparam>
    /// <typeparam name="LIVM"></typeparam>
    /// <typeparam name="BIVM"></typeparam>
    /// <typeparam name="LCVM"></typeparam>
    /// <typeparam name="BCVM"></typeparam>
    public interface IFullEditAdapter<VM, LIVM, BIVM, LCVM, BCVM> : IEditAdapter<VM, LIVM, LCVM>
        where VM : class, ISpecificModel
        where LIVM : class, IListedItemModel
        where BIVM : class, IBasicModel
        where LCVM : class, IListedCriteriaModel
        where BCVM : class, IBasicCriteriaModel
    {
        /// <summary>
        /// Obtiene la vista basica de listado de una entidad
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        IListModel<BIVM> GetBasic(BCVM entity);
    }
}
