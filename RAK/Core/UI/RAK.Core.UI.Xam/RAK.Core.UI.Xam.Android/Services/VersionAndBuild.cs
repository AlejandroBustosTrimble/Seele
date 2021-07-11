using RAK.Core.UI.Xam.Droid.Services;
using RAK.Core.UI.Xam.General;

[assembly: Dependency(typeof(VersionAndBuild))]
namespace RAK.Core.UI.Xam.Droid.Services
{
    public class VersionAndBuild : IAppVersionAndBuild
    {
        PackageInfo _appInfo;
        public VersionAndBuild()
        {
            var context = Android.App.Application.Context;
            _appInfo = context.PackageManager.GetPackageInfo(context.PackageName, 0);
        }
        public string GetVersionNumber()
        {
            return _appInfo.VersionName;
        }
        public string GetBuildNumber()
        {
            return _appInfo.VersionCode.ToString();
        }
    }
}