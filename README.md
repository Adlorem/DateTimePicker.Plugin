# DateTimePicker.Plugin
Used code from calendar project https://github.com/lilcodelab/Xamarin.Plugin.Calendar to create cross platform datetime picker for Xamarin Forms. Currently supports Android and iOs. Picker works in two modes. As embeded datetime picker and popup dialog datetime picker. Minimal use as popup:

            var result = await new DateTimePickerDialog().Show(this);
            if (result != null)
            await DisplayAlert("Result", "Selected: " + result.Value.ToString("dd MMM yyyy HH:mm"), "Cancel");
