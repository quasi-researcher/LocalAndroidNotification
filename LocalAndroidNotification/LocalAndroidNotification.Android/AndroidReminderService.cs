using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Support.V4.App;

using Xamarin.Forms;

[assembly: Dependency(typeof(LocalAndroidNotification.Droid.AndroidReminderService))]
namespace LocalAndroidNotification.Droid
{
    public class AndroidReminderService : InterfaceReminder
    {
        public void SetNotification(DateTime dateTime, string title, string message, string notify_id)
        {

            Intent alarmIntent = new Intent(Android.App.Application.Context, typeof(AlarmReceiver));
            alarmIntent.PutExtra("message", message);
            alarmIntent.PutExtra("title", title);
            alarmIntent.PutExtra("notify_id", notify_id);
            int unique_id = 0;
            int.TryParse(notify_id.ToString(), out unique_id);

            PendingIntent pendingIntent = PendingIntent.GetBroadcast(Android.App.Application.Context, unique_id, alarmIntent, PendingIntentFlags.UpdateCurrent);
            AlarmManager alarmManager = (AlarmManager)Android.App.Application.Context.GetSystemService(Context.AlarmService);
            DateTimeOffset dateOffsetValue = DateTimeOffset.Parse(dateTime.ToString());
            var timefornotification = dateOffsetValue.ToUnixTimeMilliseconds();
            alarmManager.Set(AlarmType.RtcWakeup, timefornotification, pendingIntent);
            
        }

        public void CancelNotification(int id)
        {
            Intent alarmIntent = new Intent(Android.App.Application.Context, typeof(AlarmReceiver));
            PendingIntent pendingIntent = PendingIntent.GetBroadcast(Android.App.Application.Context, id, alarmIntent, PendingIntentFlags.UpdateCurrent);
            AlarmManager alarmManager = (AlarmManager)Android.App.Application.Context.GetSystemService(Context.AlarmService);
            alarmManager.Cancel(pendingIntent);
            var notificationManager = NotificationManagerCompat.From(Android.App.Application.Context);
            //notificationManager.CancelAll();
            notificationManager.Cancel(id);
        }
    }
}