using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Xamarin.Plugin.DateTimePicker.Models
{
    internal class DialogModel
    {
        private TaskCompletionSource<DateTime?> _tcs = new TaskCompletionSource<DateTime?>();
        private string _cancelButton;
        private string _okButton;
        private int _selectedHour;
        private int _selectedMinute;
        private DateTime _selectedDate;
        private DateTime _minimumDate = DateTime.MinValue;
        private DateTime _maximumDate = DateTime.MaxValue;

        public DialogModel(string confirmButton, string cancelButton, DateTime? selectedDateTime, DateTime? minimumDate, DateTime? maximumDate)
        {
            if (selectedDateTime.HasValue)
            {
                _selectedDate = selectedDateTime.Value;
                _selectedHour = selectedDateTime.Value.Hour;
                _selectedMinute = selectedDateTime.Value.Minute;
            }
            else
            {
                var defaultValue = DateTime.Now;
                _selectedDate = defaultValue;
                _selectedHour = defaultValue.Hour;
                _selectedMinute = defaultValue.Minute;
            }

            if (minimumDate.HasValue)
            {
                _minimumDate = minimumDate.Value;
            }

            if (maximumDate.HasValue)
            {
                _maximumDate = maximumDate.Value;
            }

            _cancelButton = cancelButton;
            _okButton = confirmButton;
        }

        public ICommand CancelClickedCommand { get { return new Command(() => CancelPressed()); } }

        public ICommand OkClickedCommand { get { return new Command(() => OkPressed()); } }

        public string OkButton
        {
            get => _okButton;
        }
        public string CancelButton
        {
            get => _cancelButton;
        }

        public int SelectedHour
        {
            set { _selectedHour = value; }
            get => _selectedHour;
        }

        public int SelectedMinute
        {
            set { _selectedMinute = value; }
            get => _selectedMinute;
        }

        public DateTime SelectedDate
        {
            set { _selectedDate = value; }
            get => _selectedDate;
        }

        public DateTime MinimumDate
        {
            set { _minimumDate = value; }
            get => _minimumDate;
        }
        public DateTime MaximumDate
        {
            set { _maximumDate = value; }
            get => _maximumDate;
        }

        public void CancelPressed()
        {
            _tcs.SetResult(null);
        }

        public void OkPressed()
        {
            _tcs.SetResult(new DateTime (SelectedDate.Year, SelectedDate.Month, SelectedDate.Day, SelectedHour, SelectedMinute, 0,0));
        }

        public Task<DateTime?> GetValue()
        {
            return _tcs.Task;
        }
    }
}
