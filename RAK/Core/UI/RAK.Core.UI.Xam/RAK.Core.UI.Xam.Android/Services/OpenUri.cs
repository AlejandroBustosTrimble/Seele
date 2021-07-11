using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

[assembly: Dependency(typeof(OpenUri))]
namespace RAK.Core.UI.Xam.Droid.Services
{
    public class OpenUri : IOpenUri
    {
        void IOpenUri.OpenUri(string uri)
        {
            Device.OpenUri(new Uri(uri));
        }
    }
}