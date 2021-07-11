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
using RAK.Core.UI.Xam.Controls.Toast;
using RAK.Core.UI.Xam.Droid.Renders;

[assembly: Xamarin.Forms.Dependency(typeof(ToastAndroid))]
namespace RAK.Core.UI.Xam.Droid.Renders
{
    public class ToastAndroid : IRAKToast
    {

        public void ShowLong(string message)
        {
            Android.Widget.Toast.MakeText(Android.App.Application.Context, message, Android.Widget.ToastLength.Long).Show();
        }

        public void ShowShort(string message)
        {
            Android.Widget.Toast.MakeText(Android.App.Application.Context, message, Android.Widget.ToastLength.Short).Show();
        }

    }
}