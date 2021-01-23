using SampleApp.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace SampleApp.ViewModels
{
    public class AdvancedPageViewModel : BasePageViewModel, INotifyPropertyChanged
    {
        public ICommand DayTappedCommand => new Command<DateTime>(async (date) => await DayTapped(date));
        public ICommand SwipeLeftCommand => new Command(() => { MonthYear = MonthYear.AddMonths(2); });
        public ICommand SwipeRightCommand => new Command(() => { MonthYear = MonthYear.AddMonths(-2); });
        public ICommand SwipeUpCommand => new Command(() => { MonthYear = DateTime.Today; });

        public AdvancedPageViewModel() : base()
        {
            Culture = CultureInfo.CreateSpecificCulture("en-GB");
            // testing all kinds of adding events
            // when initializing collection

            MonthYear = MonthYear.AddMonths(1);

            Task.Delay(5000).ContinueWith(_ =>
            {
                // indexer - update later



                Task.Delay(3000).ContinueWith(t =>
                {
                    MonthYear = MonthYear.AddMonths(-2);

                    // get observable collection later

                }, TaskScheduler.FromCurrentSynchronizationContext());
            }, TaskScheduler.FromCurrentSynchronizationContext());
        }

        private DateTime _monthYear = DateTime.Today;
        public DateTime MonthYear
        {
            get => _monthYear;
            set => SetProperty(ref _monthYear, value);
        }

        private DateTime _selectedDate = DateTime.Today;
        public DateTime SelectedDate
        {
            get => _selectedDate;
            set => SetProperty(ref _selectedDate, value);
        }


        private CultureInfo _culture = CultureInfo.InvariantCulture;
        public CultureInfo Culture
        {
            get => _culture;
            set => SetProperty(ref _culture, value);
        }

        private static async Task DayTapped(DateTime date)
        {
            var message = $"Received tap event from date: {date}";
            await App.Current.MainPage.DisplayAlert("DayTapped", message, "Ok");
            Console.WriteLine(message);
        }

    }
}
