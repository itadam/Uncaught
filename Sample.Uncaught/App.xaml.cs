using System;
using Xamarin.Forms;

using Acr.UserDialogs;

namespace Sample.Uncaught
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();
            MainPage = new MainPage();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }

        public static void OnUnhandledException(Exception exception)
        {

            using var dlg = UserDialogs.Instance.Alert(exception?.Message, "Error");

        }
    }
}
