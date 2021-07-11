using Fwk.XAM.iOS.Control;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(NavigationPage), typeof(CustomNavigationPageRender))]
namespace Fwk.XAM.iOS.Control
{
    public class CustomNavigationPageRender : NavigationRenderer
    {
        protected override void OnElementChanged(VisualElementChangedEventArgs e)
        {
            base.OnElementChanged(e);

            NavigationBar.SetBackgroundImage(new UIKit.UIImage(), UIKit.UIBarMetrics.Default);
            NavigationBar.ShadowImage = new UIKit.UIImage();
			NavigationBar.TitleTextAttributes = new UIStringAttributes()
            {
                Font = UIFont.FromName("HelveticaNeue-Light", 30)
            };
        }

		//public override void ViewWillAppear(bool animated)
		//{
		//	base.ViewWillAppear(animated);

		//	NavigationBar.TopItem.BackBarButtonItem = new UIBarButtonItem("", UIBarButtonItemStyle.Plain, null);
		//}
	}
}