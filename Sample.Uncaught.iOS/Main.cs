using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using Foundation;
using ObjCRuntime;
using UIKit;

namespace Sample.Uncaught.iOS
{
    public class Application
    {

        public delegate void NSUncaughtExceptionHandler(IntPtr exception);

        [DllImport("/System/Library/Frameworks/Foundation.framework/Foundation")]
        private static extern void NSSetUncaughtExceptionHandler(IntPtr handler);

        // This is the main entry point of the application.
        static void Main(string[] args)
        {

            NSSetUncaughtExceptionHandler(Marshal.GetFunctionPointerForDelegate(new NSUncaughtExceptionHandler(MyUncaughtExceptionHandler)));

            AppDomain.CurrentDomain.UnhandledException += (s, e) => App.OnUnhandledException(e.ExceptionObject as Exception);

            // if you want to use a different Application Delegate class from "AppDelegate"
            // you can specify it here.
            UIApplication.Main(args, null, "AppDelegate");
        }

        [MonoPInvokeCallback(typeof(NSUncaughtExceptionHandler))]
        private static void MyUncaughtExceptionHandler(IntPtr handle)
        {

            NSException exception = (NSException)Runtime.TryGetNSObject(handle);

            App.OnUnhandledException(new Exception(exception.Reason));
        }
    }
}
