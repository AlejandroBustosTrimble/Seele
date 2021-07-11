using RAK.Core.Api.Model;

namespace OFIUM.App.Api.Model
{
    /// <summary>
    /// Tipo de comprobante (Factura A, etc)
    /// </summary>
    public class ReceiptTypeM : SpecificModelBase
    {
        public string Name { get; set; }

        public string Letter { get; set; }

        public decimal Sign { get; set; }
    }
}
