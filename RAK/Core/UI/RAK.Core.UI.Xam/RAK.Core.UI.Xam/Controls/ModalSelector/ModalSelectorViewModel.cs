using RAK.Core.UI.Xam.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace RAK.Core.UI.Xam.Controls
{
    public class ModalSelectorViewModel : RequestViewModelBase, IModalSelector
    {
        public long ID { get; set; }

        public string Text { get; set; }

        public string Detail { get; set; }
    }
}
