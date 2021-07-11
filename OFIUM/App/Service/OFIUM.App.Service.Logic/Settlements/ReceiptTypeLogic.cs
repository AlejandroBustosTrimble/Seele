using OFIUM.App.Service.Entity;
using OFIUM.App.Service.Logic.Abstraction;
using OFIUM.App.Service.Repository.Abstraction;
using RAK.Core.Service.Logic;

namespace OFIUM.App.Service.Logic
{
    /// <summary>
    /// Logica para la entidad Tipo de comprobante (Factura A, etc)
    /// </summary>
    public class ReceiptTypeLogic : ConsultLogicBase<ReceiptType, ReceiptTypeListedItem, ReceiptTypeListedCriteria, IReceiptTypeRepository>, IReceiptTypeLogic
    {
    }
}
