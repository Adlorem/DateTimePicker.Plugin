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
            var result = await new DateTimePickerDialog().Show(this);
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