using RAK.Core.UI.Xam.General;
using System;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RAK.Core.UI.Xam.ReusableViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GenericChatMessagesView : ContentView
    {

        #region Binding Properties

        /// <summary>
        /// Binding Property -> Source para el CropView
        /// </summary>
        public static readonly BindableProperty CHATSourceBindingProperty = BindableProperty.Create(
            propertyName: "CHATSource",
            returnType: typeof(TrulyObservableCollection<MessageDTO>),
            declaringType: typeof(GenericChatMessagesView),
            defaultValue: new TrulyObservableCollection<MessageDTO>(),
            defaultBindingMode: BindingMode.TwoWay
            );

        /// <summary>
        /// Binding Property -> Source para ENTRY Text
        /// </summary>
        public static readonly BindableProperty TEXTBindingProperty = BindableProperty.Create(
            propertyName: "TEXTSource",
            returnType: typeof(string),
            declaringType: typeof(GenericChatMessagesView),
            defaultBindingMode: BindingMode.TwoWay
            );

        /// <summary>
        /// Mensajes 
        /// PD: Setear esta propiedad desde el backend de la pantalla que lo consuma, NO BINDEAR DESDE EL XAML!!
        /// </summary>
        public TrulyObservableCollection<MessageDTO> CHATSource
        {
            get
            {
                return (TrulyObservableCollection<MessageDTO>)GetValue(CHATSourceBindingProperty);
            }
            set
            {
                SetValue(CHATSourceBindingProperty, value);
            }
        }

        /// <summary>
        /// Texto asociado al Entry
        /// </summary>
        public string TEXTSource
        {
            get
            {
                return (string)GetValue(TEXTBindingProperty);
            }
            set => SetValue(TEXTBindingProperty, value);
        }
       
        #endregion

        /// <summary>
        /// Ctor
        /// </summary>
        public GenericChatMessagesView()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Evento del ListView
        /// </summary
        public void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            MessagesListView.SelectedItem = null;
        }

        /// <summary>
        /// Evento Tapper del ListView
        /// </summary>
        public void OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            MessagesListView.SelectedItem = null;
        }

        /// <summary>
        /// Registra el boton para la vista
        /// </summary>
        public void RegisterButton(Button Button)
        {
            this.stackButton.Children.Add(Button);
        }

        /// <summary>
        /// Configura los colores para los mensajes
        /// Estos ya poseen un color por defecto (Ver IncomingViewCell/OutgoingViewCell)
        /// </summary>
        public void ConfigureBackgroundColors(Color incomingColor, Color outgoingColor)
        {
            IncomingViewCell.IncomingViewCellBackgroundColor = incomingColor;
            OutgoingViewCell.OutcomingViewCellBackgroundColor = outgoingColor;
        }

        /// <summary>
        /// Metodo publico que permite scrolear hacia abajo cuando sea necesario
        /// </summary>
        public void ScrollToEnd()
        {
            var Messages = this.CHATSource;
            if (Messages != null && Messages.Count > 0)
            {
                if (Device.RuntimePlatform == Device.iOS)
                {
                    Device.StartTimer(TimeSpan.FromMilliseconds(25), () =>
                    {
                        MessagesListView.ScrollTo(Messages.Last(), ScrollToPosition.End, false);
                        return false;
                    });
                }
                else
                {
                    this.MessagesListView.ScrollTo(Messages.Last(), ScrollToPosition.End, true);
                }
            }
        }

        /// <summary>
        /// Configuracion de Margenes para el Chat
        /// </summary>
        public void ConfigureChatMargins(Thickness IncomingThickness, Thickness OutCommingThickness)
        {
            IncomingViewCell.IncomingChatMargin = IncomingThickness;
            OutgoingViewCell.OutCommingChatMargin = OutCommingThickness;
        }

        /// <summary>
        /// Configuracion de Margenes para la Descripcion
        /// </summary>
        public void ConfigureDescriptionMargins(Thickness IncomingThickness, Thickness OutCommingThickness)
        {
            IncomingViewCell.IncomingDescriptionMargin = IncomingThickness;
            OutgoingViewCell.OutCommingDescripcionMargin = OutCommingThickness;
        }
    }
}