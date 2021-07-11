using RAK.Core.UI.Xam.ViewModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace RAK.Core.UI.Xam.ReusableViews
{

    /// <summary>
    /// DTO para Mensajes
    /// </summary>
    public class MessageDTO : GenericVM
    {

        // -- Texto del Msj
        string text;
        public string Text
        {
            get { return text; }
            set { text = value; RaisePropertyChanged(); }
        }

        string messageDescription;
        public string MessageDescription
        {
            get
            {
                return messageDescription;
            }
            set
            {
                messageDescription = value; RaisePropertyChanged();
            }
        }

        // -- Si es mensaje entrante
        bool isIncoming;
        public bool IsIncoming
        {
            get { return isIncoming; }
            set { isIncoming = value; RaisePropertyChanged(); }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void RaisePropertyChanged([CallerMemberName] string caller = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(caller));
            }
        }

    }
}
