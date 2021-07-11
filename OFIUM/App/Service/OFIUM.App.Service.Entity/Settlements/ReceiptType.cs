using RAK.Core.Service.Entity;

namespace OFIUM.App.Service.Entity
{
    /// <summary>
    /// Tipo de comprobante (Factura A, etc)
    /// </summary>
    public class ReceiptType : EntityBase
    {
        public virtual string Name { get; set; }

        public virtual string Letter { get; set; }

        public virtual decimal Sign { get; set; }
    }
}
