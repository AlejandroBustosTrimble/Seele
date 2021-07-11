using System;
using System.Collections.Generic;
using System.Text;

namespace RAK.Core.UI.Xam.Controls
{
    public interface IModalSelector
    {
        long ID { get; set; }

        string Text { get; set; }

        string Detail { get; set; }
    }
}
