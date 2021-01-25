using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Plugin.DateTimePicker.Models;
using Xamarin.Plugin.DateTimePicker.Controls;

namespace Xamarin.Plugin.DateTimePicker
{
    public class DateTimePickerDialog
    {
        public Task<DateTime?> Show(Page currentPage, DialogSettings settings = null)
        {
            //initialize settings 
            DialogSettings ds = new DialogSettings();
            DateTime defaultDate = DateTime.Now;
            ds.BackgroudColor = settings != null && settings.BackgroudColor.HasValue ? settings.BackgroudColor.Value : currentPage.BackgroundColor;
            ds.OkButtonText = settings != null && !string.IsNullOrEmpty(settings.OkButtonText) ? settings.OkButtonText : "OK";
            ds.CancelButtonText = settings != null && !string.IsNullOrEmpty(settings.CancelButtonText) ? settings.CancelButtonText : "CANCEL";
            ds.SelectedDate = settings != null && settings.SelectedDate.HasValue ? settings.SelectedDate.Value : defaultDate;
            ds.SelectedHour = settings != null && settings.SelectedHour.HasValue ? settings.SelectedHour.Value : defaultDate.Hour;
            ds.SelectedMinute = settings != null && settings.SelectedMinute.HasValue ? settings.SelectedMinute.Value : defaultDate.Minute;
            ds.MinimumDate = settings != null && settings.MinimumDate.HasValue ? settings.MinimumDate.Value : DateTime.MinValue;
            ds.MaximumDate = settings != null && settings.MaximumDate.HasValue ? settings.MaximumDate.Value : DateTime.MaxValue;
            var vm = new DialogModel(ds);

            return CreateDialog(currentPage, vm);
        }

        private Task<DateTime?> CreateDialog(Page currentPage, DialogModel vm)
        {
            var tcs = new TaskCompletionSource<DateTime?>();

            Device.BeginInvokeOnMainThread(async () =>
            {
                var page = new DialogPage(vm);

                await currentPage.Navigation.PushModalAsync(page);

                var value = await vm.GetValue();

                await currentPage.Navigation.PopModalAsync();

                tcs.SetResult(value);
            });

            return tcs.Task;
        }
    }

}
