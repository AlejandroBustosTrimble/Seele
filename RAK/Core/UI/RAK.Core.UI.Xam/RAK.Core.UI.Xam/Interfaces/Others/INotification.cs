using RAK.Core.UI.Xam.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace RAK.Core.UI.Xam
{
    public interface INotification
    {
        void SetNotification(string mensaje, Dictionary<string, string> parameters, NotificationActivity activityEnum);
            void NotificationClear(int notificacionID);
    }
}
