using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Plugin.DateTimePicker.Models;

namespace Xamarin.Plugin.DateTimePicker.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DialogPage : ContentPage
    {
        private DateTime _defaulDatetValue;
        internal DialogPage(DialogModel model)
        {
            BindingContext = model;
            _defaulDatetValue = model.SelectedDate;
            InitializeComponent();
        }

        private void ResetButton_Clicked(object sender, EventArgs e)
        {
            DialogModel model = this.BindingContext as DialogModel;
            model.SelectedDate = _defaulDatetValue.Date;
            model.SelectedHour = _defaulDatetValue.Hour;
            model.SelectedMinute = _defaulDatetValue.Minute;
        }
    }
}