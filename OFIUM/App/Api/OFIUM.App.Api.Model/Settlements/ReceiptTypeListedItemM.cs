using RAK.Core.Api.Model;

namespace OFIUM.App.Api.Model
{
    /// <summary>
    /// Item de lista de los Tipos de Facturas Model
    /// </summary>
    public class ReceiptTypeListedItemM : ListedItemModel
    {
        public string Name { get; set; }

        public string Letter { get; set; }

        public decimal Sign { get; set; }
    }
}
