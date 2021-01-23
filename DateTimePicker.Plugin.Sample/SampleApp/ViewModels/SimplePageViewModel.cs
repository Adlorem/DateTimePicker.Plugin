using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Forms;
using SampleApp.Model;

namespace SampleApp.ViewModels
{
    public class SimplePageViewModel : BasePageViewModel, INotifyPropertyChanged
    {
        public ICommand TodayCommand => new Command(() => { Year = DateTime.Today.Year; Month = DateTime.Today.Month; SelectedDate = DateTime.Today;});
        
        public SimplePageViewModel() : base()
        {
            // testing all kinds of adding events
            // when initializing collection


            Task.Delay(5000).ContinueWith(_ =>
            {
                // indexer - update later

                Month += 1;

                Task.Delay(3000).ContinueWith(t =>
                {
                    Month += 1;
                }, TaskScheduler.FromCurrentSynchronizationContext());
            }, TaskScheduler.FromCurrentSynchronizationContext());
        }

        private int _month = DateTime.Today.Month;
        public int Month
        {
            get => _month;
            set => SetProperty(ref _month, value);
        }

        public int _year = DateTime.Today.Year;
        public int Year
        {
            get => _year;
            set => SetProperty(ref _year, value);
        }


        private DateTime _selectedDate = DateTime.Today;
        public DateTime SelectedDate
        {
            get => _selectedDate;
            set => SetProperty(ref _selectedDate, value);
        }

        private DateTime _minimumDate = new DateTime(2019, 4, 29);
        public DateTime MinimumDate
        {
            get => _minimumDate;
            set => SetProperty(ref _minimumDate, value);
        }

        private DateTime _maximumDate = DateTime.Today.AddMonths(5);
        public DateTime MaximumDate
        {
            get => _maximumDate;
            set => SetProperty(ref _maximumDate, value);
        }

    }
}
