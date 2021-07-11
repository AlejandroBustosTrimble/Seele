using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using Fwk.XAM.Controls;
using Fwk.XAM.iOS.Control;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(CustomViewCell), typeof(CustomViewCellRender))]
namespace Fwk.XAM.iOS.Control
{
	public class CustomViewCellRender : ViewCellRenderer
	{
		public override UITableViewCell GetCell(Cell item, UITableViewCell reusableCell, UITableView tv)
		{
			var cell = base.GetCell(item, reusableCell, tv);

			cell.SelectedBackgroundView = new UIView { BackgroundColor = UIColor.FromRGB(237, 237, 237) };

			return cell;
		}
	}
}