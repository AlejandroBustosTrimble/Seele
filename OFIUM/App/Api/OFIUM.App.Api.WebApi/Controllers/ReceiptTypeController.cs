using OFIUM.App.Api.Adapter.Abstraction;
using OFIUM.App.Api.Model;
using RAK.Core.Api.WebApi;

namespace OFIUM.App.Api.WebApi.Controllers
{
    /// <summary>
    /// Controller Api de Tipo de comprobante (Factura A, etc)
    /// </summary>
    public class ReceiptTypeController : ConsultWebApiControllerBase<ReceiptTypeM, ReceiptTypeListedItemM, ReceiptTypeListedCriteriaM, IReceiptTypeAdapter>
    {
    }
}