using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace RAK.Core.UI.Xam.Controls
{
    public class CustomListView : ListView
    {
        public CustomListView() : base()
        {
            if (Device.RuntimePlatform == Device.iOS)
                this.HeightRequest = DeviceDisplay.MainDisplayInfo.Height;
        }
    }
}
