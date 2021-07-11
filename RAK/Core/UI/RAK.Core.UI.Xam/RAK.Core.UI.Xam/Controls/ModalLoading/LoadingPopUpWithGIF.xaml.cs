using Rg.Plugins.Popup.Pages;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RAK.Core.UI.Xam.Controls.ModalLoading
{

	/// <summary>
	/// PopUp Loading con GIF 
	/// </summary>
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LoadingPopUpWithGIF : PopupPage, ILoadingPopUp
	{

		/// <summary>
		/// Ctor
		/// </summary>
		public LoadingPopUpWithGIF()
		{
			InitializeComponent();
		}

		#region Binding for Nombre

		public static readonly BindableProperty GifSourceProperty = BindableProperty.Create(
			propertyName: "GifSource",
			returnType: typeof(string),
			declaringType: typeof(LoadingPopUpWithGIF),
			defaultValue: string.Empty,
			defaultBindingMode: BindingMode.TwoWay
			);

		/// <summary>
		/// Source del Gif
		/// </summary>
		public string GifSource
		{
			get => (string)GetValue(GifSourceProperty);
			set => SetValue(GifSourceProperty, value);
		}

		#endregion

		#region Binding for Imagen

		public static readonly BindableProperty LabelTextProperty = BindableProperty.Create(
			propertyName: "LabelText",
			returnType: typeof(string),
			declaringType: typeof(LoadingPopUpWithGIF),
			defaultValue: string.Empty,
			defaultBindingMode: BindingMode.TwoWay
			);

		/// <summary>
		/// Texto de Label Principal
		/// </summary>
		public string LabelText
		{
			get => (string)GetValue(LabelTextProperty);
			set => SetValue(LabelTextProperty, value);
		}

		#endregion

	}

}