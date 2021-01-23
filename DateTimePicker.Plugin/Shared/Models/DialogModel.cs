using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Xamarin.Plugin.DateTimePicker.Models
{
    public class DialogModel
    {
        private TaskCompletionSource<DateTime?> _tcs = new TaskCompletionSource<DateTime?>();
        private string _cancelButton;
        private string _okButton;
        private int _selectedHour;
        private int _selectedMinute;
        private DateTime _selectedDate;

        public DialogModel(string confirmButton, string cancelButton, DateTime? selectedDateTime)
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
