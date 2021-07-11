using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace RAK.Core.UI.Xam.Droid.Controls
{
	public abstract class CustomViewCellRender : ViewCellRenderer
	{

		//private Android.Views.View _cellCore;
		//private bool _selected = false;

		//protected override Android.Views.View GetCellCore(Cell item, Android.Views.View convertView, ViewGroup parent, Context context)
		//{
		//	_cellCore = base.GetCellCore(item, convertView, parent, context);
		//	return _cellCore;
		//}

		//protected override void OnCellPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs args)
		//{
		//	base.OnCellPropertyChanged(sender, args);
		//	if (args.PropertyName == "IsSelected")
		//	{
		//		_selected = !_selected;
		//		var extendedViewCell = sender as ViewCell;
		//		if (_selected)
		//			_cellCore.SetBackgroundColor(Android.Graphics.Color.Red);
		//		else
		//			_cellCore.SetBackgroundColor(Android.Graphics.Color.Transparent);
		//	}
		//}

		/// <summary>
		/// Implementar asi: Resource.Drawable.ViewCellBackground
		/// </summary>
		protected abstract int ViewCellBackground { get; }

		protected override Android.Views.View GetCellCore(Cell item, Android.Views.View convertView, Android.Views.ViewGroup parent, Android.Content.Context context)
		{
			var cell = base.GetCellCore(item, convertView, parent, context);

			cell.SetBackgroundResource(ViewCellBackground);

			return cell;
		}
	}
}