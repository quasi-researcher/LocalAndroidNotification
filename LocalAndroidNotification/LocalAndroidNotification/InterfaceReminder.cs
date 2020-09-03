using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace LocalAndroidNotification
{
    public interface InterfaceReminder
    {
        void SetNotification(DateTime datetime, string title, string message, string notify_id);
        void CancelNotification(int id);
    }
}
