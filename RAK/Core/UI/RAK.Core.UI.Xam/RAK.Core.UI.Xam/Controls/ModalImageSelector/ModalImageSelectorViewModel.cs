using RAK.Core.UI.Xam.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace RAK.Core.UI.Xam.Controls
{
    public class ModalImageSelectorViewModel : ResponseViewModelBase, IModalImageSelector
    {
        public long ID { get; set; }

        public string Text { get; set; }

        public string Description { get; set; }

        public string Image { get; set; }

        public bool IsRegistred { get { return ID != 0; } }
        public string AccountName { get; set; }
    }

    public class ModalImageSelectorRequest : RequestViewModelBase
    {
        public string URL { get; set; }
        public string Text { get; set; }

    }
}
