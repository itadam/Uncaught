using System;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

using Acr.UserDialogs;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.Generic;

namespace Sample.Uncaught.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {

        public ICommand OpenWebCommand => new Command(async () => await Browser.OpenAsync("https://xamarin.com"));

        public ICommand CrashTestCommand => new Command(() => OutOfBoundsCrashTest());

        private static readonly string[] testArr = new string[] { "0", "1", "2", "3", "4" };

        private void OutOfBoundsCrashTest()
        {

            var random = new Random();

            var value = testArr[random.Next(5, 10)];

            using var dlg = UserDialogs.Instance.Alert($"You drew the value: {value}", "Random");

        }

        bool isBusy = false;
        public bool IsBusy
        {
            get { return isBusy; }
            set { SetProperty(ref isBusy, value); }
        }

        string title = "Crash Test";
        public string Title
        {
            get { return title; }
            set { SetProperty(ref title, value); }
        }

        protected bool SetProperty<T>(ref T backingStore, T value,
            [CallerMemberName] string propertyName = "",
            Action onChanged = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
                return false;

            backingStore = value;
            onChanged?.Invoke();
            OnPropertyChanged(propertyName);
            return true;
        }

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            var changed = PropertyChanged;
            if (changed == null)
                return;

            changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}