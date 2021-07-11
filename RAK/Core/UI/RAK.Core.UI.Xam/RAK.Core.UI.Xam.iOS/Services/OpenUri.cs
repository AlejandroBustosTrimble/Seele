using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using RAK.Core.UI.Xam.iOS;
using UIKit;
using Xamarin.Forms;

[assembly: Dependency(typeof(OpenUri))]
namespace RAK.Core.UI.Xam.iOS
{
    public class OpenUri : IOpenUri
    {
        void IOpenUri.OpenUri(string uri)
        {
            var nsUrl = new NSUrl(new System.Uri(uri).AbsoluteUri);
            Device.OpenUri(nsUrl);
        }
    }
}