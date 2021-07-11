using System;
using System.Collections.Generic;
using System.Text;
using XF=Xamarin.Forms;

namespace RAK.Core.UI.Xam
{
    public interface IToolbarItemBadgeService
    {
        void SetBadge(XF.Page page, XF.ToolbarItem item, string value, XF.Color backgroundColor, XF.Color textColor);
    }
}
