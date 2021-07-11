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

namespace RAK.Core.UI.Xam.Droid.Registers
{
    public static class RegisterComplements
    {
        public static void Register(Application application, Activity activity, Bundle bundle)
        {
            FormsWebViewRenderer.Initialize();
            Rg.Plugins.Popup.Popup.Init(activity, bundle);
            global::Xamarin.Forms.Forms.Init(activity, bundle);
            ImageCircleRenderer.Init();
            Xamarin.FormsMaps.Init(activity, bundle);
            global::Xamarin.Auth.Presenters.XamarinAndroid.AuthenticationConfiguration.Init(activity, bundle);
            ZXing.Mobile.MobileBarcodeScanner.Initialize(application);
            CrossCurrentActivity.Current.Init(activity, bundle);
            //PullToRefreshLayoutRenderer.Init();
            CachedImageRenderer.Init(true);
            ExperimentalFeatures.Enable("ShareFileRequest_Experimental");
        }
    }
}