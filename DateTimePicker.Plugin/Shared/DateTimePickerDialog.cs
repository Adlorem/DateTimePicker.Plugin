using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Plugin.DateTimePicker.Models;
using Xamarin.Plugin.DateTimePicker.Controls;

namespace Xamarin.Plugin.DateTimePicker
{
    public class DateTimePickerDialog
    {
        public Task<DateTime?> Show(Page currentPage)
        {
            var vm = new DialogModel("OK", "Cancel", null);

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

        public Task<DateTime?> Show(Page currentPage, string confirmButton, string cancelButton, DateTime? seletedDateTime)
        {
            var vm = new DialogModel(confirmButton, cancelButton, seletedDateTime);

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

        public Task<DateTime?> Show(Page currentPage, DateTime? seletedDateTime)
        {
            var vm = new DialogModel("OK", "Cancel", seletedDateTime);

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
