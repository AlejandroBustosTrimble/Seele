using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace RAK.Core.UI.Xam.Controls
{
	public class SelectMultipleBasePage : ContentPage
	{	
		public class WrappedSelection : INotifyPropertyChanged
		{
			public long ID { get; set; }
			public String Name { get; set; }

			bool isSelected = false;
			public bool IsSelected
			{
				get
				{
					return isSelected;
				}
				set
				{
					if (isSelected != value)
					{
						isSelected = value;
						PropertyChanged(this, new PropertyChangedEventArgs("IsSelected"));
					}
				}
			}
			public event PropertyChangedEventHandler PropertyChanged = delegate { };
		}


		public class WrappedItemSelectionTemplate : ViewCell
		{
			public WrappedItemSelectionTemplate() : base()
			{
				Label name = new Label();
				name.SetBinding(
					Label.TextProperty,
					new Binding("Name")
				);
				Switch mainSwitch = new Switch();
				mainSwitch.SetBinding(
					Switch.IsToggledProperty,
					new Binding("IsSelected")
				);
				RelativeLayout layout = new RelativeLayout();
				layout.Children.Add(
					name,
					Constraint.Constant(5),
					Constraint.Constant(5),
					Constraint.RelativeToParent(p => p.Width - 60),
					Constraint.RelativeToParent(p => p.Height - 10)
				);
				layout.Children.Add(
					mainSwitch,
					Constraint.RelativeToParent(p => p.Width - 55),
					Constraint.Constant(5),
					Constraint.Constant(50),
					Constraint.RelativeToParent(p => p.Height - 10)
				);
				View = layout;
			}
		}
		public List<WrappedSelection> WrappedItems = new List<WrappedSelection>();
		public SelectMultipleBasePage(List<WrappedSelection> items)
		{
			WrappedItems = items.Select(item => new WrappedSelection()
			{
				Name = item.Name,
				ID = item.ID,
				IsSelected = item.IsSelected
			}).ToList();
			ListView mainList = new ListView()
			{
				ItemsSource = WrappedItems,
				ItemTemplate = new DataTemplate(typeof(WrappedItemSelectionTemplate)),
			};
			mainList.ItemSelected += (sender, e) =>
			{
				//Supresses highlighting of ListItems
				if (e.SelectedItem == null) return;
				var o = (WrappedSelection)e.SelectedItem;
				o.IsSelected = !o.IsSelected;
				((ListView)sender).SelectedItem = null;
			};

			StackLayout stack = new StackLayout();
			stack.Children.Add(mainList);

			Button button = new Button();
			button.Text = "Aceptar";
			button.VerticalOptions = LayoutOptions.End;
			button.Clicked += new EventHandler((sender, e) => GetSelection());

			stack.Children.Add(button);

			Content = stack;
		}
		public void DeselectAll()
		{
			foreach (var wi in WrappedItems)
			{
				wi.IsSelected = false;
			}
		}
		public void GetSelection()
		{
			var items = WrappedItems.Where(item => item.IsSelected).ToList();
			this.OnFinishedEvent(items);
		}

		public event OnFinishedEventHandler OnFinishedEvent = delegate { };

		public delegate void OnFinishedEventHandler(List<WrappedSelection> ids);
	}


}
