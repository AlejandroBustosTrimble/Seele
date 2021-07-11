using RAK.Core.UI.Xam.Behaviors;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace RAK.Core.UI.Xam.Controls
{
    public class NumericEntry : Entry
    {
        public NumericEntry()
        {
            this.Behaviors.Add(new NumericValidationBehavior());
            this.Keyboard = Keyboard.Numeric;
        }
    }
}
