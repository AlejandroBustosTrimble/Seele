using OFIUM.App.Service.Entity;
using RAK.Core.Service.Logic.Abstraction;

namespace OFIUM.App.Service.Logic.Abstraction
{
    /// <summary>
    /// Interfaz de Logica para la entidad Tipo de comprobante (Factura A, etc)
    /// </summary>
    public interface IReceiptTypeLogic : IConsultLogic<ReceiptType, ReceiptTypeListedItem, ReceiptTypeListedCriteria>
    {
    }
}
