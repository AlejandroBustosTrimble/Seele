using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace RAK.Core.UI.Xam.Controls
{
	public class HideableToolbarItem : ToolbarItem
	{
		//public HideableToolbarItem() : base()
		//{
		//	this.InitVisibility();
		//}

		//private async void InitVisibility()
		//{
		//	await Task.Delay(100);
		//	OnIsVisibleChanged(this, false, IsVisible);
		//}

		//public MasterDetailPage Parent { set; get; }

		//public bool IsVisible
		//{
		//	get { return (bool)GetValue(IsVisibleProperty); }
		//	set { SetValue(IsVisibleProperty, value); }
		//}

		//public static BindableProperty IsVisibleProperty = BindableProperty.Create<HideableToolbarItem, bool>(o => o.IsVisible, false, propertyChanged: OnIsVisibleChanged);


		//private static void OnIsVisibleChanged(BindableObject bindable, bool oldvalue, bool newvalue)
		//{
		//	var item = bindable as HideableToolbarItem;

		//	if (item.Parent == null)
		//		return;

		//	var items = item.Parent.ToolbarItems;

		//	if (newvalue && !items.Contains(item))
		//	{
		//		items.Add(item);
		//	}
		//	else if (!newvalue && items.Contains(item))
		//	{
		//		items.Remove(item);
		//	}
		//}

		public static readonly BindableProperty IsVisibleProperty = BindableProperty.Create(nameof(IsVisible), typeof(bool), typeof(HideableToolbarItem), true, BindingMode.TwoWay, propertyChanged: OnIsVisibleChanged);

		public bool IsVisible
		{
			get => (bool)GetValue(IsVisibleProperty);
			set => SetValue(IsVisibleProperty, value);
		}

		private static void OnIsVisibleChanged(BindableObject bindable, object oldvalue, object newvalue)
		{
			var item = bindable as HideableToolbarItem;

			if (item == null || item.Parent == null)
				return;

			var toolbarItems = ((ContentPage)item.Parent).ToolbarItems;

			if ((bool)newvalue && !toolbarItems.Contains(item))
			{
				Device.BeginInvokeOnMainThread(() => { toolbarItems.Add(item); });
				//toolbarItems.Add(item);
			}
			else if (!(bool)newvalue && toolbarItems.Contains(item))
			{
				Device.BeginInvokeOnMainThread(() => { toolbarItems.Remove(item); });
				//toolbarItems.Remove(item);
			}
		}
	}
}
