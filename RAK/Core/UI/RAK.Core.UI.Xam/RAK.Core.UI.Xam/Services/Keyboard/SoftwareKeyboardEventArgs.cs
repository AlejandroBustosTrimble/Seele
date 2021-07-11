using System;
using System.Collections.Generic;
using System.Text;

namespace RAK.Core.UI.Xam.Services.Keyboard
{
    public class SoftwareKeyboardEventArgs : EventArgs
    {
        public SoftwareKeyboardEventArgs(bool isVisible, int keyboardHeigth = 0)
        {
            this.IsVisible = isVisible;
            this.KeyboardHeigth = keyboardHeigth;
        }

        public bool IsVisible { get; private set; }

        public int KeyboardHeigth { get; private set; }
    }
}
