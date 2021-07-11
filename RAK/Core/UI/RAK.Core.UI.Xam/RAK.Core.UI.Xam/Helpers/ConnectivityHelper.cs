using Plugin.Connectivity;
using System;
using System.Collections.Generic;
using System.Text;

namespace RAK.Core.UI.Xam.Helpers
{
    public class ConnectivityHelper
    {
        public static bool HasInternet
        {
            get
            {
                return (CrossConnectivity.Current.IsConnected);
            }
        }
    }
}
