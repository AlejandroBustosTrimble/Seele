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

[assembly: ExportRenderer(typeof(CustomNavigationPage), typeof(NavigationPageRenderer))]
namespace RAK.Core.UI.Xam.Droid.Controls
{
    public abstract class NavigationPageRenderer : Xamarin.Forms.Platform.Android.AppCompat.NavigationPageRenderer
    {
        /// <summary>
        /// Implementar asi: Resource.Drawable.ic_back
        /// </summary>
        protected abstract int ic_back { get; }

        public AppCompToolbar toolbar;
        public Activity context;

        public NavigationPageRenderer(Context context) : base(context)
        {
            this.context = (Activity)context;
        }

        protected override Task<bool> OnPushAsync(Xamarin.Forms.Page view, bool animated)
        {
            var retVal = base.OnPushAsync(view, animated);

            //context = (Activity)Forms.Context;
            toolbar = context.FindViewById<AppCompToolbar>(Droid.Resource.Id.toolbar);

            if (toolbar != null)
            {
                if (toolbar.NavigationIcon != null)
                {
                    toolbar.NavigationIcon = Android.Support.V7.Content.Res.AppCompatResources.GetDrawable(context, ic_back);
                    toolbar.SetBackgroundColor(Android.Graphics.Color.White);
                    //toolbar.Title = "返回";
                }
            }

            return retVal;
        }
    }
}