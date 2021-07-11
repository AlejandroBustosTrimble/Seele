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
using Plugin.CurrentActivity;
using RAK.Core.UI.Xam.Droid.Services;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;

[assembly: Dependency(typeof(ScreenBlock))]
namespace RAK.Core.UI.Xam.Droid.Services
{
    public class ScreenBlock : IScreenOrientationBlock
    {
        /// <summary>
        /// Metodo no necesario por el momento
        /// </summary>
        public void SetHorizontal()
        {
            this.SetOrientation(Android.Content.PM.ScreenOrientation.Landscape);
        }

        /// <summary>
        /// Metodo no necesario por el momento
        /// </summary>
        public void SetVertical()
        {
            this.SetOrientation(Android.Content.PM.ScreenOrientation.Portrait);
        }

        /// <summary>
        /// Setea una orientacion de pantalla
        /// </summary>
        /// <param name="orientation"></param>
        private void SetOrientation(Android.Content.PM.ScreenOrientation orientation)
        {
            if (CrossCurrentActivity.Current.Activity.RequestedOrientation != orientation)
            {
                CrossCurrentActivity.Current.Activity.RequestedOrientation = orientation;
            }
        }
    }
}