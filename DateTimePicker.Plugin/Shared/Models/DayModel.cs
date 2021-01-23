using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace Xamarin.Plugin.DateTimePicker.Models
{
    internal class DayModel : BindableBase<DayModel>
    {
        public DateTime Date
        {
            get => GetProperty<DateTime>();
            set => SetProperty(value)
                    .Notify(nameof(BackgroundColor),
                            nameof(OutlineColor));
        }

        public double DayViewSize
        {
            get => GetProperty<double>();
            set => SetProperty(value);
        }

        public float DayViewCornerRadius
        {
            get => GetProperty<float>();
            set => SetProperty(value);
        }

        public Style DaysLabelStyle
        {
            get => GetProperty(Device.Styles.BodyStyle);
            set => SetProperty(value);
        }

        public ICommand DayTappedCommand
        {
            get => GetProperty<ICommand>();
            set => SetProperty(value);
        }

        public bool IsThisMonth
        {
            get => GetProperty<bool>();
            set => SetProperty(value)
                    .Notify(nameof(TextColor),
                            nameof(IsVisible));
        }

        public bool IsSelected
        {
            get => GetProperty<bool>();
            set => SetProperty(value)
                    .Notify(nameof(TextColor),
                            nameof(BackgroundColor),
                            nameof(OutlineColor));
        }

        public bool OtherMonthIsVisible
        {
            get => GetProperty(true);
            set => SetProperty(value)
                    .Notify(nameof(IsVisible));
        }

        public bool IsDisabled
        {
            get => GetProperty<bool>();
            set => SetProperty(value)
                    .Notify(nameof(TextColor));
        }

        public Color SelectedTextColor
        {
            get => GetProperty(Color.White);
            set => SetProperty(value)
                    .Notify(nameof(TextColor));
        }

        public Color OtherMonthColor
        {
            get => GetProperty(Color.Silver);
            set => SetProperty(value)
                    .Notify(nameof(TextColor));
        }

        public Color DeselectedTextColor
        {
            get => GetProperty(Color.Default);
            set => SetProperty(value)
                    .Notify(nameof(TextColor));
        }

        public Color SelectedBackgroundColor
        {
            get => GetProperty(Color.FromHex("#2196F3"));
            set => SetProperty(value)
                    .Notify(nameof(BackgroundColor));
        }

        public Color DeselectedBackgroundColor
        {
            get => GetProperty(Color.Transparent);
            set => SetProperty(value)
                    .Notify(nameof(BackgroundColor));
        }

        public Color TodayOutlineColor
        {
            get => GetProperty(Color.FromHex("#FF4081"));
            set => SetProperty(value)
                    .Notify(nameof(OutlineColor));
        }

        public Color TodayFillColor
        {
            get => GetProperty(Color.Transparent);
            set => SetProperty(value)
                    .Notify(nameof(BackgroundColor));
        }

        public Color DisabledColor
        {
            get => GetProperty(Color.FromHex("#ECECEC"));
            set => SetProperty(value);
        }

        public bool BackgroundEventIndicator =>  false;

        public Color OutlineColor => IsToday()
                                   ? TodayOutlineColor
                                   : Color.Transparent;


        public Color BackgroundColor
        {
            get
            {
                if (!IsVisible) return DeselectedBackgroundColor;

                return (BackgroundEventIndicator, IsSelected, IsToday()) switch
                {
                    (false, true, _) => SelectedBackgroundColor,
                    (false, false, true) => TodayFillColor,
                    (_, _, _) => DeselectedBackgroundColor
                };
            }
        }

        public Color TextColor
        {
            get
            {
                if (!IsVisible) return OtherMonthColor;

                return (IsDisabled, IsSelected, IsThisMonth) switch
                {
                    (true, _, _) => DisabledColor,
                    (false, false, _) => SelectedTextColor,
                    (_, _, _) => OtherMonthColor
                };
            }
        }
        public bool IsVisible => IsThisMonth || OtherMonthIsVisible;

        private bool IsToday()
            => Date.Date == DateTime.Today && !IsSelected;
    }
}
