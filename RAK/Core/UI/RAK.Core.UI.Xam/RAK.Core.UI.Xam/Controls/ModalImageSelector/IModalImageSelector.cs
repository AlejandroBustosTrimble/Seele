using System;
using System.Collections.Generic;
using System.Text;

namespace RAK.Core.UI.Xam.Controls
{
    public interface IModalImageSelector
    {
        long ID { get; set; }

        string Text { get; set; }

        string Description { get; set; }

        string Image { get; set; }
    }
}
