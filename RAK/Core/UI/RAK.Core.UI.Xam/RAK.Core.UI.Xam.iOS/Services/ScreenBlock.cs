using Xamarin.Forms;
using Foundation;
using UIKit;
using RAK.Core.UI.Xam.iOS;

[assembly: Dependency(typeof(ScreenBlock))]
namespace RAK.Core.UI.Xam.iOS
{
    public class ScreenBlock : IScreenOrientationBlock
    {
        public void SetHorizontal()
        {
            Orientation.Instance.SetLandscapeRight();
            LockOrientation(UIInterfaceOrientationMask.LandscapeRight, UIInterfaceOrientation.LandscapeRight);
            //UIApplication.SharedApplication.SetStatusBarOrientation(UIInterfaceOrientation.LandscapeRight, false);

        }



        public void SetVertical()
        {
            Orientation.Instance.SetPortrait();
            LockOrientation(UIInterfaceOrientationMask.Portrait, UIInterfaceOrientation.Portrait);
            //UIApplication.SharedApplication.SetStatusBarOrientation(UIInterfaceOrientation.Portrait, false);
        }

        private void LockOrientation(UIInterfaceOrientationMask uIInterfaceOrientationMask, UIInterfaceOrientation uIInterfaceOrientation)
        {
            if (UIApplication.SharedApplication.Delegate != null)
            {
                UIDevice.CurrentDevice.SetValueForKey(new NSNumber((int)uIInterfaceOrientation), new NSString("orientation"));
            }
        }
    }
}