using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace LocalAndroidNotification
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void notify_Clicked(object sender, EventArgs e)
        {
            string notification_id = "0"; //set it as you need
            int iyear = 2020;
            int imonth = 1;
            int iday = 1;
            int ihour = 12;
            int iminute = 0;
            int isecond = 0;
            int.TryParse(year.Text, out iyear);
            int.TryParse(month.Text, out imonth);
            int.TryParse(day.Text, out iday);
            int.TryParse(hour.Text, out ihour);
            int.TryParse(minute.Text, out iminute);
            DateTime notify_at_date = new DateTime(iyear, imonth, iday, ihour, iminute, isecond);

            var reminder = DependencyService.Get<InterfaceReminder>();
            reminder.SetNotification(notify_at_date, "Local Android Notification", "Seems it works", notification_id);
            DisplayAlert("Local Android Notification", "Notification is set", "OK");


        }
        private void cancel_Clicked(object sender, EventArgs e)
        {
            var reminder = DependencyService.Get<InterfaceReminder>();
            int notification_id = 0;
            reminder.CancelNotification(notification_id);
            DisplayAlert("Local Android Notification", "Notification is cancelled", "OK");
        }
    }
}
