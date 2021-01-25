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
        private DateTime _minimumDate;
        private DateTime _maximumDate;
        private Color _backgroudColor;

        public DialogModel(DialogSettings settings)
        {

            _selectedDate = settings.SelectedDate.Value;
            _selectedHour = settings.SelectedHour.Value;
            _selectedMinute = settings.SelectedMinute.Value;
            _minimumDate = settings.MinimumDate.Value;
            _maximumDate = settings.MaximumDate.Value;
            _cancelButton = settings.CancelButtonText;
            _okButton = settings.OkButtonText;
            _backgroudColor = settings.BackgroudColor.Value;
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

        public Color BackgroundColor
        {
            set { _backgroudColor = value; }
            get => _backgroudColor;
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
