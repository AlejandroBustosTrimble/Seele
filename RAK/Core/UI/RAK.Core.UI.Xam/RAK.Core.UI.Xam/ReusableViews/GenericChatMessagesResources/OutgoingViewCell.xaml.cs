
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RAK.Core.UI.Xam.ReusableViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OutgoingViewCell : ViewCell
    {
        #region Properties

        /// <summary>
        /// Estatico para poder setearlo IOS
        /// </summary>
        public static Color OutcomingViewCellBackgroundColor { get; set; } = Color.FromHex("#03A9F4");

        /// <summary>
        /// Margen para el Chat
        /// </summary>
        public static Thickness OutCommingChatMargin { get; set; }

        /// <summary>
        /// Margen para la Descripcion
        /// </summary>
        public static Thickness OutCommingDescripcionMargin { get; set; }

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        public OutgoingViewCell()
        {
            InitializeComponent();
            oFrame.BackgroundColor = OutcomingViewCellBackgroundColor;
            oFrame.Margin = OutCommingChatMargin;
            LabelDescription.Margin = OutCommingDescripcionMargin;
        }

        #endregion
    }
}