using Foundation;
using RAK.Core.UI.Xam.Controls.Toast;
using RAK.Core.UI.Xam.iOS.Renders;
using UIKit;

[assembly: Xamarin.Forms.Dependency(typeof(ToastIOS))]
namespace RAK.Core.UI.Xam.iOS.Renders
{
    public class ToastIOS : IRAKToast
    {

        /// <summary>
        /// Toast de duracion mayor
        /// </summary>
        public void ShowLong(string message)
        {
            LongMessage(message);
        }

        /// <summary>
        /// Toast de duracion menor
        /// </summary        
        public void ShowShort(string message)
        {
            ShortMessage(message);
        }

        const double LONG_DELAY = 3.5;
        const double SHORT_DELAY = 2.0;

        NSTimer alertDelay;
        UIAlertController alert;

        public void ShortMessage(string mesaj)
        {
            Create(mesaj, SHORT_DELAY);
        }

        public void LongMessage(string mesaj)
        {
            Create(mesaj, LONG_DELAY);
        }

        void Create(string mesaj, double sure)
        {
            alertDelay = NSTimer.CreateRepeatingScheduledTimer(sure, (obj) =>
            {
                Dispose();
            });
            alert = UIAlertController.Create(null, mesaj, UIAlertControllerStyle.Alert);

            UIApplication.SharedApplication.KeyWindow.RootViewController.PresentViewController(alert, true, null);

        }

        void Dispose()
        {
            if (alert != null)
            {
                alert.DismissViewController(true, null);

            }
            if (alertDelay != null)
            {
                alertDelay.Dispose();
            }
        }

    }
}