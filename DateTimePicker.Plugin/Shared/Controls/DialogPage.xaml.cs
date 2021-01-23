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
       
        public DialogPage(DialogModel model)
        {
            BindingContext = model;
            InitializeComponent();
        }

        private void Button_Clicked(object sender, EventArgs e)
        {

        }
    }
}