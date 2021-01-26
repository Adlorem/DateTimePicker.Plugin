# DateTimePicker.Plugin
Used code from calendar project https://github.com/lilcodelab/Xamarin.Plugin.Calendar to create cross platform datetime picker for Xamarin Forms. Currently supports Android and iOs. Picker works in two modes. As embeded datetime picker and popup dialog datetime picker. Currently picker supports only 24 hours date format. Minimal code for popup:

            var result = await new DateTimePickerDialog().Show(this);
            if (result != null)
            await DisplayAlert("Result", "Selected: " + result.Value.ToString("dd MMM yyyy HH:mm"), "Cancel");

Package is also available via Nuget

https://www.nuget.org/packages/Xamarin.Plugin.DateTimePicker/

ver 1.0.1
- fixed Android popup backgroud problem
- fixed iOS 13 dialog problem
- moved popup settings to setting class

Example settings

            DialogSettings settings = new DialogSettings();
            settings.SelectedDate = DateTime.Now.AddDays(-1);
            settings.MinimumDate = DateTime.Now.AddDays(-10);
            settings.MaximumDate = DateTime.Now.AddDays(10);
            settings.SelectedHour = DateTime.Now.Hour;
            settings.SelectedMinute = DateTime.Now.Minute;
            var result = await new DateTimePickerDialog().Show(this, settings);
            if (result != null)
            await DisplayAlert("Result", "Selected: " + result.Value.ToString("dd MMM yyyy HH:mm"), "Cancel");

