using System;
using System.Collections.Generic;
using System.Text;

namespace RAK.Core.UI.Xam.Services.Keyboard
{
    public interface ISoftwareKeyboardService
    {
        event SoftwareKeyboardEventHandler Hide;

        event SoftwareKeyboardEventHandler Show;
    }
}
