using OFIUM.App.Api.Model;
using RAK.Core.Api.Adapter.Abstraction;

namespace OFIUM.App.Api.Adapter.Abstraction
{
    /// <summary>
    /// Interfaz de Adapter para la entidad Tipo de comprobante (Factura A, etc)
    /// </summary>
    public interface IReceiptTypeAdapter : IConsultAdapter<ReceiptTypeM, ReceiptTypeListedItemM, ReceiptTypeListedCriteriaM>
    {
    }
}
