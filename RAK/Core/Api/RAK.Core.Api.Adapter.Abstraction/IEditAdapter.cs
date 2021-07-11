using RAK.Core.Api.Model;
using RAK.Core.Api.Model.Interfaces;

namespace RAK.Core.Api.Adapter.Abstraction
{
    /// <summary>
    /// Interfaz de Adapter de Entidades Editable
    /// </summary>
    /// <typeparam name="VM"></typeparam>
    /// <typeparam name="LIVM"></typeparam>
    /// <typeparam name="LCVM"></typeparam>
    public interface IEditAdapter<VM, LIVM, LCVM> : IConsultAdapter<VM, LIVM, LCVM>
        where VM : class, ISpecificModel
        where LIVM : class, IListedItemModel
        where LCVM : class, IListedCriteriaModel
    {
        /// <summary>
        /// Inserta una entidad
        /// </summary>
        /// <param name="entity"></param>
        ResultModel Add(VM entity);

        /// <summary>
        /// Actualiza una entidad
        /// </summary>
        /// <param name="entity"></param>
        ResultModel Update(VM entity);

        /// <summary>
        /// Delete en SQL (NO es un delete logico, borra el registro)
        /// </summary>
        /// <param name="ID"></param>
        ResultModel Delete(IdentityModel ID);
    }
}
