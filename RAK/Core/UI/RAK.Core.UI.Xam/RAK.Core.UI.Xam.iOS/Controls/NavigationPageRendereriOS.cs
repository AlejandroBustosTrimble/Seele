using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Foundation;
using Fwk.XAM.Controls;
using Fwk.XAM.iOS.Control;
using UIKit;
using Xamarin.Forms;

[assembly: ExportRenderer(typeof(CustomNavigationPage), typeof(NavigationPageRendereriOS))]
namespace Fwk.XAM.iOS.Control
{
	public abstract class NavigationPageRendereriOS : Xamarin.Forms.Platform.iOS.NavigationRenderer
	{
		protected abstract Type InicioType { get; }

		private readonly Stack<NavigationPage> _navigationPageStack = new Stack<NavigationPage>();
		private NavigationPage CurrentNavigationPage => _navigationPageStack.Peek();
		private List<Type> VistasExcluidas { get; set; } = new List<Type>();

		public NavigationPageRendereriOS() : base()
		{
			this.VistasExcluidas.Add(this.InicioType);
		}

		protected override Task<bool> OnPushAsync(Xamarin.Forms.Page page, bool animated)
		{
			var retVal = base.OnPushAsync(page, animated);

			SetBackButtonOnPage(page);

			return retVal;
		}

		protected override Task<bool> OnPopViewAsync(Xamarin.Forms.Page page, bool animated)
		{
			var retVal = base.OnPopViewAsync(page, animated);

			var stack = page.Navigation.NavigationStack;

			var returnPage = stack[stack.Count - 2];

			if (returnPage != null)
			{
				SetBackButtonOnPage(returnPage);
			}

			return retVal;
		}


		void SetBackButtonOnPage(Xamarin.Forms.Page page)
		{
			// Below is what I added for common usage.
			if (this.VistasExcluidas.Contains(page.GetType()))
			{
				SetDefaultBackButton();
			}
			//if (page.GetType() == typeof(Inicio))
			//{
			//	SetDefaultBackButton();
			//}
			else
			{
				SetImageTitleBackButton("ic_back.png", "", -15);
			}

			// Below is the previous implementation which is from the other GitHub repository

			//if (page is INavigationActionBarConfig incomingPage)
			//{
			//    SetImageTitleBackButton("Down", "返回", -15);
			//}
			//else
			//{
			//    SetDefaultBackButton();
			//}
		}

		void SetImageTitleBackButton(string imageBundleName, string buttonTitle, int horizontalOffset)
		{
			var topVC = this.TopViewController;

			// Create the image back button
			var backButtonImage = new UIBarButtonItem(
					UIImage.FromBundle(imageBundleName),
					UIBarButtonItemStyle.Plain,
					(sender, args) =>
					{
						topVC.NavigationController.PopViewController(true);
					});

			// Create the Text Back Button
			var backButtonText = new UIBarButtonItem(
				buttonTitle,
				UIBarButtonItemStyle.Plain,
				(sender, args) =>
				{
					topVC.NavigationController.PopViewController(true);
				});

			backButtonText.SetTitlePositionAdjustment(new UIOffset(horizontalOffset, 0), UIBarMetrics.Default);

			// Add buttons to the Top Bar
			UIBarButtonItem[] buttons = new UIBarButtonItem[2];
			buttons[0] = backButtonImage;
			buttons[1] = backButtonText;

			NavigationBar.ShadowImage = new UIImage();

			topVC.NavigationItem.LeftBarButtonItems = buttons;
		}

		void SetDefaultBackButton()
		{
			NavigationBar.ShadowImage = new UIImage();
			this.TopViewController.NavigationItem.LeftBarButtonItems = null;
		}
	}
}