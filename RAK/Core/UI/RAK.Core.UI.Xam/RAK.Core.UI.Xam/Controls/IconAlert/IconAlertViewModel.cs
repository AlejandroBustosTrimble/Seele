using RAK.Core.UI.Xam.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace RAK.Core.UI.Xam.Controls
{
	public class IconAlertViewModel : GenericVM
	{
		private string icon;
		public string Icon { get { return this.icon; } set { this.icon = value; RaisePropertyChanged(); } }

		private string title;
		public string Title { get { return this.title; } set { this.title = value; RaisePropertyChanged(); } }

		private string ok;
		public string OK { get { return this.ok; } set { this.ok = value; RaisePropertyChanged(); } }

		private string cancel;
		public string Cancel { get { return this.cancel; } set { this.cancel = value; RaisePropertyChanged(); } }

		private string text;
		public string Text { get { return this.text; } set { this.text = value; RaisePropertyChanged(); } }

		private string buttonColor = "#119F86";
		public string ButtonColor { get { return this.buttonColor; } set { this.buttonColor = value; RaisePropertyChanged(); } }

		private bool hasAccept;
		public bool HasAccept { get { return this.hasAccept; } set { this.hasAccept = value; RaisePropertyChanged(); } }

		public ICommand CancelCommand { get; private set; }

		public ICommand OKCommand { get; private set; }

		public IconAlertViewModel()
		{
			this.CancelCommand = new Command(CancelAlert);
			this.OKCommand = new Command(OKAlert);
		}

		private void CancelAlert()
		{
			(AssociatePage as IconAlert).SetResult(false);
		}

		private void OKAlert()
		{
			(AssociatePage as IconAlert).SetResult(true);
		}
	}
}
