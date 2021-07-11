using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace RAK.Core.UI.Xam.Extension
{
    public static class VisualElementExtensions
    {
        public static void SetTranslation(this VisualElement element, double y)
        {
            element.TranslationY = y;
            var rectangle = new Rectangle
            {
                Left = element.X,
                Top = element.Y //+ y
                ,
                Width = element.Width,
                Height = element.Height + (y < 0 ? -y : 0)
            };
            element.Layout(rectangle);
        }
    }
}
