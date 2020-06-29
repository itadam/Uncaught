using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

using Java.Lang;
using Acr.UserDialogs;

namespace Sample.Uncaught.Droid
{
    [Activity(Label = "Sample.Uncaught", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity, Thread.IUncaughtExceptionHandler
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {

            Thread.DefaultUncaughtExceptionHandler = this;

            AppDomain.CurrentDomain.UnhandledException += (s, e) => App.OnUnhandledException(e.ExceptionObject as System.Exception);

            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);

            global::Xamarin.Forms.Forms.SetFlags("CollectionView_Experimental");
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);

            UserDialogs.Init(this);

            LoadApplication(new App());
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        public void UncaughtException(Thread t, Throwable e)
        {
            App.OnUnhandledException(Throwable.ToException(e));
        }
    }
}