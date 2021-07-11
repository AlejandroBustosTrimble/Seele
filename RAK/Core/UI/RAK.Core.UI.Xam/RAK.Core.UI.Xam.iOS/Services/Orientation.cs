using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using UIKit;

namespace RAK.Core.UI.Xam.iOS
{
    public class Orientation
    {
        private UIInterfaceOrientationMask currentOrientation = UIInterfaceOrientationMask.Portrait;
        private static Orientation _instance;
        public static Orientation Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new Orientation();
                return _instance;
            }
        }
        public UIInterfaceOrientationMask GetCurrent()
        {
            return currentOrientation;
        }
        public void SetLandscapeRight()
        {
            currentOrientation = UIInterfaceOrientationMask.LandscapeRight;
        }

        public void SetPortrait()
        {
            currentOrientation = UIInterfaceOrientationMask.Portrait;
        }
    }
}