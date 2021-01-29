using System;
using Xamarin.Forms;

namespace Xamarin.Plugin.DateTimePicker
{
    public class DialogSettings
    {
        public string OkButtonText { set; get; }
        public string CancelButtonText { set; get; }
        public DateTime? SelectedDate { set; get; }
        public DateTime? MaximumDate { set; get; }
        public DateTime? MinimumDate { set; get; }
        public int? SelectedHour { set; get; }
        public int? SelectedMinute { set; get; }
        public Color? BackgroudColor { set; get; }
        public Color? DaysColor { set; get; }
        public Color? HourMinTextColor { set; get; }
        public Color? ButtonsColor { set; get; }
        public Color? CurrentMonthDaysColor { get; set; }
    }
}
