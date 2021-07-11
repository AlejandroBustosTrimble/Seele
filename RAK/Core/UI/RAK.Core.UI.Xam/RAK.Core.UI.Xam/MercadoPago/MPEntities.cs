using System.Collections.Generic;

namespace RAK.Core.UI.Xam.MercadoPago
{

    public class DocumentTypeListItem
    {
        public string id { get; set; }

        public string name { get; set; }

        public string type { get; set; }

        public int min_length { get; set; }

        public int max_length { get; set; }
    }

    public class MercadoPagoResponseBase
    {
        public bool IsSuccess { get; set; }
    }

    public class DocumentTypesResponse : MercadoPagoResponseBase
    {
        public List<DocumentTypeListItem> List { get; set; } = new List<DocumentTypeListItem>();
    }

    public class PaymentMethodResponse : MercadoPagoResponseBase
    {
        public string PaymentMethodID { get; set; }
    }

    public class CreateTokenResponse : MercadoPagoResponseBase
    {
        public string StatusCode { get; set; }

        public string ResponseStr { get; set; }

        public string Token { get; set; }
    }

    public class YearListItem
    {
        public string ID { get; set; }

        public string Name { get; set; }
    }

    public class MonthListItem
    {
        public string ID { get; set; }

        public string Name { get; set; }
    }

}
