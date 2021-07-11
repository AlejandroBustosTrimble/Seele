using Foundation;
using RAK.Core.UI.Xam.iOS;
using Xamarin.Forms;

[assembly: Dependency(typeof(VersionAndBuild))]
namespace RAK.Core.UI.Xam.iOS
{
    public class VersionAndBuild : IAppVersionAndBuild
    {
        public string GetVersionNumber()
        {
            //var VersionNumber = NSBundle.MainBundle.InfoDictionary.ValueForKey(new NSString("CFBundleShortVersionString")).ToString();   
            return NSBundle.MainBundle.ObjectForInfoDictionary("CFBundleShortVersionString").ToString();
        }
        public string GetBuildNumber()
        {
            //var BuildNumber = NSBundle.MainBundle.InfoDictionary.ValueForKey(new NSString("CFBundleVersion")).ToString();   
            return NSBundle.MainBundle.ObjectForInfoDictionary("CFBundleVersion").ToString();
        }
    }
}