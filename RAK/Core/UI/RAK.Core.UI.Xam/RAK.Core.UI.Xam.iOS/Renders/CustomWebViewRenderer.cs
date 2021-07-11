using RAK.Core.UI.Xam.iOS.Renders;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(WebView), typeof(CustomWebViewRenderer))]
namespace RAK.Core.UI.Xam.iOS.Renders
{
    public class CustomWebViewRenderer : WkWebViewRenderer
    {
    }
}