using RAK.Core.UI.Xam.Model;
using System.Collections.Generic;

namespace RAK.Core.UI.Xam.MsgCenter
{
    public class MessagingCenterGenericResponse<T>
    where T : IResponseXam
    { 
        public bool Result { get; set; }
        public bool InputValidation { get; set; }

        public T Data { get; set; }
        public string ErrorMessage { get; set; }

        public string InfoMessage { get; set; }

        public string WarningMessage { get; set; }

        public string SuccessMessage { get; set; }
    }

    public class MessagingCenterGenericListResponse<T>
    where T : IResponseXam
    {
        public bool Result { get; set; }
        public List<T> Data { get; set; }
        public string Error { get; set; }
    }

}
