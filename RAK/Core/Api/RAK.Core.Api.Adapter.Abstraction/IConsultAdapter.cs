using RAK.Core.Api.Model;
using RAK.Core.Api.Model.Interfaces;
using RAK.Fwk.Api.Adapter;

namespace RAK.Core.Api.Adapter.Abstraction
{
    /// <summary>
    /// Interfaz de Adapter de Entidades de SOLO Consulta
    /// </summary>
    /// <typeparam name="VM"></typeparam>
    /// <typeparam name="LIVM"></typeparam>
    /// <typeparam name="LCVM"></typeparam>
    public interface IConsultAdapter<VM, LIVM, LCVM> : IAdapterGeneric
        where VM : class, ISpecificModel
        where LIVM : class, IListedItemModel
        where LCVM : class, IListedCriteriaModel
    {
        /// <summary>
        /// Obtiene una entidad por ID
        /// </summary>
        /// <typeparam name="E"></typeparam>
        /// <param name="ID"></param>
        /// <returns></returns>
        VM GetById(IdentityModel ID);

        /// <summary>
        /// Obtiene la vista de listado de una entidad
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        ListModel<LIVM> GetListed(LCVM entity);

        /// <summary>
        /// Obtiene una lista de una entidad con todos sus registros
        /// </summary>
        /// <param name="count"></param>
        /// <param name="offset"></param>
        /// <returns></returns>
        ListModel<VM> GetAll(GetListModel criteria);
    }
}
