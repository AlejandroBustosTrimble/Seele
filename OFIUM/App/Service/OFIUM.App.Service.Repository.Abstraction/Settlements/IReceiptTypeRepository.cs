using OFIUM.App.Service.Entity;
using RAK.Core.Service.Repository.Abstraction;

namespace OFIUM.App.Service.Repository.Abstraction
{
    /// <summary>
    /// Interfaz de repositorio para la entidad Tipo de comprobante (Factura A, etc)
    /// </summary>
    public interface IReceiptTypeRepository : IConsultRepository<ReceiptType, ReceiptTypeListedItem, ReceiptTypeListedCriteria>
    {

    }
}
