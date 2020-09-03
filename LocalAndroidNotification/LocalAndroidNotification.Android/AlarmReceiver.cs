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

namespace LocalAndroidNotification.Droid
{
    [BroadcastReceiver(Enabled = true)]
    public class AlarmReceiver : BroadcastReceiver
    {
        public const string CHANNEL = "basic_channel";
        public override void OnReceive(Context context, Intent intent)
        {

            var message = intent.GetStringExtra("message");
            var title = intent.GetStringExtra("title");
            var notification_id = intent.GetStringExtra("notify_id");
            int unique_id = 0;
            int.TryParse(notification_id.ToString(), out unique_id);

            var notIntent = new Intent(context, typeof(MainActivity));
            var contentIntent = PendingIntent.GetActivity(context, unique_id, notIntent, PendingIntentFlags.CancelCurrent);
            
            var channel = new NotificationChannel(CHANNEL, "Notification", NotificationImportance.Default)
            {
                LockscreenVisibility = NotificationVisibility.Public
            };

            int resourceId;
            resourceId = Resource.Drawable.local_rem_n;

            //Generate a notification with just short text and small icon
            var builder = new NotificationCompat.Builder(context)
                            .SetContentIntent(contentIntent)
                            .SetSmallIcon(Resource.Drawable.local_rem_n)
                            .SetContentTitle(title)
                            .SetContentText(message)
                            .SetWhen(Java.Lang.JavaSystem.CurrentTimeMillis())
                            .SetChannelId(CHANNEL)
                            .SetAutoCancel(true);
            //.SetVisibility(NotificationVisibility.Public);


            NotificationManager manager = (NotificationManager)Android.App.Application.Context.GetSystemService(Context.NotificationService);
            manager.CreateNotificationChannel(channel);

            var notification = builder.Build();
            
            manager.Notify(unique_id, notification);

        }
    }
}