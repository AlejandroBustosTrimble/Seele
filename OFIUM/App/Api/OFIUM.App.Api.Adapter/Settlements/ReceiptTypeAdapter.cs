using OFIUM.App.Api.Adapter.Abstraction;
using OFIUM.App.Api.Model;
using OFIUM.App.Service.Entity;
using OFIUM.App.Service.Logic.Abstraction;
using RAK.Core.Api.Adapter;
using RAK.Core.Api.Model;

namespace OFIUM.App.Api.Adapter
{
    /// <summary>
    /// Adapter para la entidad Tipo de comprobante (Factura A, etc)
    /// </summary>
    public class ReceiptTypeAdapter : ConsultAdapterBase<ReceiptTypeM, ReceiptType, ReceiptTypeListedItemM, ReceiptTypeListedItem, ReceiptTypeListedCriteriaM, ReceiptTypeListedCriteria, IReceiptTypeLogic>, IReceiptTypeAdapter
    {
        public override ListModel<ReceiptTypeListedItemM> GetListed(ReceiptTypeListedCriteriaM entity)
        {
            return base.GetListed(entity);
        }
    }
}
