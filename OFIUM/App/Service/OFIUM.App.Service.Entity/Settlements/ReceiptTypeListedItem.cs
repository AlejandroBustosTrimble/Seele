using RAK.Core.Service.Entity;

namespace OFIUM.App.Service.Entity
{
    /// <summary>
    /// Item de lista de los Tipo de comprobante (Factura A, etc) Entidad
    /// </summary>
    public class ReceiptTypeListedItem : ListedItemEntity
    {
        public string Name { get; set; }

        public string Letter { get; set; }

        public decimal Sign { get; set; }
    }
}
