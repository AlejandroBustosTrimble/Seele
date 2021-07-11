using RAK.Core.UI.Xam.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace RAK.Core.UI.Xam.Controls.RequiredLabel
{
    public class RequiredLabelViewModel : ViewModelBase
    {
        private string labelText = "";
        private string requiredIdentifier = "*";
        private Color requiredColor = Color.Red;
        /// <summary>
        /// Texto que mostrara el label
        /// </summary>
        public string LabelText
        {
            get { return labelText; }
            set
            {
                labelText = value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// Indicador de requerido.[Por defecto *]
        /// </summary>
        public string RequiredIdentifier
        {
            get { return requiredIdentifier; }
            set
            {
                requiredIdentifier = value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// Color por defecto del RequiredIdentifier
        /// </summary>
        public Color RequiredColor
        {
            get { return requiredColor; }
            set
            {
                requiredColor = value;
                RaisePropertyChanged();
            }
        }
    }
}
