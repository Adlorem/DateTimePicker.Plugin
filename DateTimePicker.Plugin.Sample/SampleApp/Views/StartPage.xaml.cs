using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Plugin.DateTimePicker;

namespace SampleApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StartPage : ContentPage
    {
        public StartPage()
        {
            InitializeComponent();
        }

        private async void BtnOpenDialog_Clicked(object sender, EventArgs e)
        {
            DialogSettings settings = new DialogSettings();
            settings.SelectedDate = DateTime.Now.AddDays(-1);
            settings.MinimumDate = DateTime.Now.AddDays(-10);
            settings.MaximumDate = DateTime.Now.AddDays(10);
            settings.SelectedHour = DateTime.Now.Hour;
            settings.SelectedMinute = DateTime.Now.Minute;
            var result = await new DateTimePickerDialog().Show(this, settings);
            if (result != null)
            await DisplayAlert("Result", "Selected: " + result.Value.ToString("dd MMM yyyy HH:mm"), "Cancel");
        }

        private void BtnShowEmbed_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new SimplePage());
        }
        private void BtnShowEmbedAdvanced_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new AdvancedPage());
        }
    }
}