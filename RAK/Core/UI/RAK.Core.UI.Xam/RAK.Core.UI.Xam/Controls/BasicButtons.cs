using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace RAK.Core.UI.Xam.Controls
{
    public static class BasicButtonConfig
    {
        public static Thickness Margin { get; set; } = new Thickness(20, 15, 20, 15);
        public static string AppColor
        {
            get; set;
        }
    }

    /// <summary>
    /// Boton Basico con fondo color segun APP
    /// </summary>
    public class FondoBasicButton : Button
    {
        public FondoBasicButton()
        {
            this.BackgroundColor = Color.FromHex(BasicButtonConfig.AppColor);
            this.TextColor = Color.White;
            this.CornerRadius = 5;
            this.Margin = BasicButtonConfig.Margin;
        }
    }

    public class BordeBasicButton : Button
    {
        public BordeBasicButton()
        {
            {
                this.BorderColor = Color.FromHex(BasicButtonConfig.AppColor);
                this.BorderWidth = 1;
                this.BackgroundColor = Color.Transparent;
                this.TextColor = Color.FromHex(BasicButtonConfig.AppColor);
                this.CornerRadius = 5;
                this.Margin = BasicButtonConfig.Margin;
            }
        }
    }

    /// <summary>
    /// Boton Transparente TLM
    /// </summary>
    public class BasicTransparenteButton : Button
    {
        public BasicTransparenteButton()
        {
            this.TextColor = Color.FromHex(BasicButtonConfig.AppColor);
            this.BackgroundColor = Color.Transparent;
        }
    }

}
