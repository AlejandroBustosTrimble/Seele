
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RAK.Core.UI.Xam.ReusableViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class IncomingViewCell : ViewCell
    {
        #region Properties

        /// <summary>
        /// Estatico para poder setearlo
        /// </summary>
        public static Color IncomingViewCellBackgroundColor { get; set; } = Color.FromHex("#03A9F4");
        
        /// <summary>
        /// Margen para el Chat
        /// </summary>
        public static Thickness IncomingChatMargin { get; set; }

        /// <summary>
        /// Margen para la Descripcion
        /// </summary>
        public static Thickness IncomingDescriptionMargin { get; set; }

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        public IncomingViewCell()
        {
            InitializeComponent();
            this.oFrame.BackgroundColor = IncomingViewCellBackgroundColor;
            this.oFrame.Margin = IncomingChatMargin;
            this.LabelDescription.Margin = IncomingDescriptionMargin;
        }

        #endregion
    }
}