using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using CoreGraphics;
using Foundation;
using Fwk.XAM.iOS.Control;
using RAK.Core.UI.Xam.Controls;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(RAKIconEntry), typeof(KitsIconEntryRenderer))]
namespace Fwk.XAM.iOS.Control
{
	public class KitsIconEntryRenderer : EntryRenderer
	{
		/// <summary>
		/// Used for registration with dependency service
		/// </summary>
		public async static void Init()
		{
			var temp = DateTime.Now;
		}

		/// <summary>
		/// Event for Element Changed
		/// </summary>
		/// <param name="e"></param>
		protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
		{
			base.OnElementChanged(e);

			var view = (RAKIconEntry)Element;

			if (view != null)
			{
				SetIcon(view);
			}
		}

		/// <summary>
		/// Event for Element Property Changed
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected override void OnElementPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
		{
			base.OnElementPropertyChanged(sender, e);

			var view = (RAKIconEntry)Element;
			if (e.PropertyName == RAKIconEntry.IconProperty.PropertyName)
				SetIcon(view);
		}

		private void SetIcon(RAKIconEntry view)
		{
			if (!string.IsNullOrEmpty(view.Icon))
			{
				Control.LeftViewMode = UITextFieldViewMode.Always;

				//var img = UIImage.FromBundle(view.Icon);

				var img = this.GetColoredImage(view.Icon);
				var imgView = new UIImageView(img) { Frame = new CGRect(10, 0, 20, 20) };

				UIView objLeftView = new UIView(new CGRect(0, 0, 30, 20));
				objLeftView.AddSubview(imgView);

				Control.LeftView = objLeftView;
				//Control.LeftView = imgView;
			}
			else
			{
				Control.LeftViewMode = UITextFieldViewMode.Never;
				Control.LeftView = null;
			}
		}

		private UIImage GetColoredImage(string imageName)
		{
			UIImage image = UIImage.FromBundle(imageName);
			UIImage coloredImage = null;

			//UIGraphics.BeginImageContext(image.Size);
			UIGraphics.BeginImageContextWithOptions(image.Size, false, UIScreen.MainScreen.Scale);
			using (CGContext context = UIGraphics.GetCurrentContext())
			{
				context.TranslateCTM(0, image.Size.Height);
				context.ScaleCTM(1.0f, -1.0f);

				var rect = new RectangleF(0, 0, (float)image.Size.Width, (float)image.Size.Height);

				context.ClipToMask(rect, image.CGImage);
				context.SetFillColor(UIColor.LightGray.CGColor);
				context.FillRect(rect);

				coloredImage = UIGraphics.GetImageFromCurrentImageContext();
				UIGraphics.EndImageContext();
			}
			return coloredImage;
		}
	}
}