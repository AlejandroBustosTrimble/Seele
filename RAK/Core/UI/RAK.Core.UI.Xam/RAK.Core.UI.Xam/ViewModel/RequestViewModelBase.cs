using RAK.Core.UI.Xam.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace RAK.Core.UI.Xam.ViewModel
{
    public class RequestViewModelBase : IRequestXam
    {
        /// <summary>
        /// EntityID
        /// </summary>
        public long EntityID
        {
            get; set;
        }
    }

    public class ResponseViewModelBase : IResponseXam
    {
        /// <summary>
        /// EntityID
        /// </summary>
        public long EntityID
        {
            get; set;
        }
    }
}
