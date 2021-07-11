using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RAK.Core.UI.Xam.Controls
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class FakeRadioList : ContentView
	{
		#region Binding for Text

		public static readonly BindableProperty ItemsProperty = BindableProperty.Create(
			propertyName: nameof(Items),
			returnType: typeof(ObservableCollection<IFakeRadioItem>),
			declaringType: typeof(FakeRadioList),
			defaultValue: new ObservableCollection<IFakeRadioItem>(),
			defaultBindingMode: BindingMode.TwoWay
			);

		public ObservableCollection<IFakeRadioItem> Items
		{
			get => (ObservableCollection<IFakeRadioItem>)GetValue(ItemsProperty);
			set => SetValue(ItemsProperty, value);
		}

		#endregion

		public FakeRadioList()
		{
			InitializeComponent();
		}

		private void CheckBox_CheckedChanged(object sender, CheckedChangedEventArgs e)
		{
			var cb = (CheckBox)sender;
			var item = (IFakeRadioItem)cb.BindingContext;
			this.CheckBoxChecked(item);
		}

		private void CheckBoxChecked(IFakeRadioItem item)
		{
			// -- Si quedaron todos sin checkear, ponemos cheackeado el actual
			if (this.Items.Where(x => x.Checked).Count() == 0)
			{
				// -- Lo checkeamos de nuevo porque no puede haber uno deschequeado
				item.Checked = true;
				return;
			}

			// -- Si no cambia estado, seteamos para q cambie y salimos
			if (!item.CambiaEstado)
			{
				item.CambiaEstado = true;
				return;
			}


			// -- Ponemos los checkeados que no son el actual en false
			foreach (var tf in this.Items.Where(x => x.Checked && item.ID != x.ID))
			{
				tf.CambiaEstado = false;
				tf.Checked = false;
			}
		}
	}

	public interface IFakeRadioItem
	{
		int ID { get; set; }
		string Nombre { get; set; }
		bool Checked { get; set; }
		bool CambiaEstado { get; set; }
	}
}