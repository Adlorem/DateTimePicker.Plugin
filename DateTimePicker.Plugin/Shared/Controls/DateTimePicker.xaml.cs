using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Xamarin.Plugin.DateTimePicker.Controls
{
    /// <summary>
    /// DateTimePicker plugin for Xamarin.Forms
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DateTimePicker : ContentView
    {
        // private proporties //
        private const uint CalendarSectionAnimationRate = 16;
        private const int CalendarSectionAnimationDuration = 200;
        private const string CalendarSectionAnimationId = nameof(CalendarSectionAnimationId);
        private readonly Animation _calendarSectionAnimateHide;
        private readonly Animation _calendarSectionAnimateShow;
        private bool _calendarSectionAnimating;
        private double _calendarSectionHeight;
        private string[] _minutes;
        private string[] _hours;
        private static DateTime _defaultDateTime = DateTime.Now;


        /// <summary>
        /// DateTimePicker plugin for Xamarin.Forms
        /// </summary>
        public DateTimePicker()
        {
            PrevMonthCommand = new Command(PrevMonth);
            NextMonthCommand = new Command(NextMonth);
            PrevYearCommand = new Command(PrevYear);
            NextYearCommand = new Command(NextYear);
            ShowHideCalendarCommand = new Command(ToggleCalendarSectionVisibility);
            _minutes = GenerateTimeRange(59);
            _hours = GenerateTimeRange(23);
            InitializeComponent();
            UpdateSelectedDateLabel();
            UpdateMonthLabel();

            _calendarSectionAnimateHide = new Animation(AnimateMonths, 1, 0);
            _calendarSectionAnimateShow = new Animation(AnimateMonths, 0, 1);

            calendarContainer.SizeChanged += OnCalendarContainerSizeChanged;

        }

        #region Public propoerties
        public string[] Minutes
        {
            get => _minutes;
        }
        public string[] Hours
        {
            get => _hours;
        }

        #endregion

        #region Bindable properties

        public static readonly BindableProperty ShowMonthPickerProperty =
          BindableProperty.Create(nameof(ShowMonthPicker), typeof(bool), typeof(DateTimePicker), true);

        public bool ShowMonthPicker
        {
            get => (bool)GetValue(ShowMonthPickerProperty);
            set => SetValue(ShowMonthPickerProperty, value);
        }

        public static readonly BindableProperty ShowYearPickerProperty =
          BindableProperty.Create(nameof(ShowYearPicker), typeof(bool), typeof(DateTimePicker), true);

        public bool ShowYearPicker
        {
            get => (bool)GetValue(ShowYearPickerProperty);
            set => SetValue(ShowYearPickerProperty, value);
        }


        public static readonly BindableProperty SelectedMinuteProperty =
            BindableProperty.Create(nameof(SelectedMinute), typeof(int), typeof(DateTimePicker), _defaultDateTime.Minute, BindingMode.TwoWay, propertyChanged: OnMinuteChanged);

        public int SelectedMinute
        {
            get => (int)GetValue(SelectedMinuteProperty);
            set => SetValue(SelectedMinuteProperty, value);
        }

        public static readonly BindableProperty SelectedHourProperty =
            BindableProperty.Create(nameof(SelectedHour), typeof(int), typeof(DateTimePicker), _defaultDateTime.Hour, BindingMode.TwoWay, propertyChanged: OnHourChanged);

        public int SelectedHour
        {
            get => (int)GetValue(SelectedHourProperty);
            set => SetValue(SelectedHourProperty, value);
        }

        public static readonly BindableProperty MonthProperty =
          BindableProperty.Create(nameof(Month), typeof(int), typeof(DateTimePicker), _defaultDateTime.Month, BindingMode.TwoWay, propertyChanged: OnMonthChanged);

        public int Month
        {
            get => (int)GetValue(MonthProperty);
            set => SetValue(MonthProperty, value);
        }

        public static readonly BindableProperty YearProperty =
          BindableProperty.Create(nameof(Year), typeof(int), typeof(DateTimePicker), _defaultDateTime.Year, BindingMode.TwoWay, propertyChanged: OnYearChanged);

        public int Year
        {
            get => (int)GetValue(YearProperty);
            set => SetValue(YearProperty, value);
        }

        public static readonly BindableProperty MonthYearProperty =
          BindableProperty.Create(nameof(MonthYear), typeof(DateTime), typeof(DateTimePicker), _defaultDateTime, BindingMode.TwoWay, propertyChanged: OnMonthYearChanged);

        public DateTime MonthYear
        {
            get => (DateTime)GetValue(MonthYearProperty);
            set => SetValue(MonthYearProperty, value);
        }

        public static readonly BindableProperty SelectedDateProperty =
          BindableProperty.Create(nameof(SelectedDate), typeof(DateTime), typeof(DateTimePicker), _defaultDateTime, BindingMode.TwoWay);

        public DateTime SelectedDate
        {
            get => (DateTime)GetValue(SelectedDateProperty);
            set => SetValue(SelectedDateProperty, value);
        }

        public static readonly BindableProperty CultureProperty =
          BindableProperty.Create(nameof(Culture), typeof(CultureInfo), typeof(DateTimePicker), CultureInfo.InvariantCulture, BindingMode.TwoWay);

        public CultureInfo Culture
        {
            get => (CultureInfo)GetValue(CultureProperty);
            set => SetValue(CultureProperty, value);
        }

        public static readonly BindableProperty EventTemplateProperty =
          BindableProperty.Create(nameof(EventTemplate), typeof(DataTemplate), typeof(DateTimePicker), null);

        public DataTemplate EventTemplate
        {
            get => (DataTemplate)GetValue(EventTemplateProperty);
            set => SetValue(EventTemplateProperty, value);
        }

        public static readonly BindableProperty EmptyTemplateProperty =
            BindableProperty.Create(nameof(EmptyTemplate), typeof(DataTemplate), typeof(DateTimePicker), null);

        public DataTemplate EmptyTemplate
        {
            get => (DataTemplate)GetValue(EmptyTemplateProperty);
            set => SetValue(EmptyTemplateProperty, value);
        }

        public static readonly BindableProperty HourMinTextColorProperty =
           BindableProperty.Create(nameof(HourMinTextColor), typeof(Color), typeof(DateTimePicker), Color.Black);

        public Color HourMinTextColor
        {
            get => (Color)GetValue(HourMinTextColorProperty);
            set => SetValue(HourMinTextColorProperty, value);
        }

        public static readonly BindableProperty MonthLabelColorProperty =
          BindableProperty.Create(nameof(MonthLabelColor), typeof(Color), typeof(DateTimePicker), Color.FromHex("#2196F3"));

        public Color MonthLabelColor
        {
            get => (Color)GetValue(MonthLabelColorProperty);
            set => SetValue(MonthLabelColorProperty, value);
        }

        public static readonly BindableProperty YearLabelColorProperty =
          BindableProperty.Create(nameof(YearLabelColor), typeof(Color), typeof(DateTimePicker), Color.FromHex("#2196F3"));

        public Color YearLabelColor
        {
            get => (Color)GetValue(YearLabelColorProperty);
            set => SetValue(YearLabelColorProperty, value);
        }

        public static readonly BindableProperty SelectedDateColorProperty =
          BindableProperty.Create(nameof(SelectedDateColor), typeof(Color), typeof(DateTimePicker), Color.FromHex("#2196F3"));

        public Color SelectedDateColor
        {
            get => (Color)GetValue(SelectedDateColorProperty);
            set => SetValue(SelectedDateColorProperty, value);
        }

        public static readonly BindableProperty DaysTitleColorProperty =
          BindableProperty.Create(nameof(DaysTitleColor), typeof(Color), typeof(DateTimePicker), Color.Default);

        public Color DaysTitleColor // this one is ok
        {
            get => (Color)GetValue(DaysTitleColorProperty);
            set => SetValue(DaysTitleColorProperty, value);
        }

        public static readonly BindableProperty SelectedDayTextColorProperty =
          BindableProperty.Create(nameof(SelectedDayTextColor), typeof(Color), typeof(DateTimePicker), Color.Black);

        public Color SelectedDayTextColor // sets color for currents month day text
        {
            get => (Color)GetValue(SelectedDayTextColorProperty);
            set => SetValue(SelectedDayTextColorProperty, value);
        }

        public static readonly BindableProperty OtherMonthDayColorProperty =
          BindableProperty.Create(nameof(OtherMonthDayColor), typeof(Color), typeof(DateTimePicker), Color.White);

        public Color OtherMonthDayColor //selected day text color
        {
            get => (Color)GetValue(OtherMonthDayColorProperty);
            set => SetValue(OtherMonthDayColorProperty, value);
        }

        public static readonly BindableProperty OtherMonthDayIsVisibleProperty =
          BindableProperty.Create(nameof(OtherMonthDayIsVisible), typeof(bool), typeof(DateTimePicker), true);

        public bool OtherMonthDayIsVisible
        {
            get => (bool)GetValue(OtherMonthDayIsVisibleProperty);
            set => SetValue(OtherMonthDayIsVisibleProperty, value);
        }

        public static readonly BindableProperty SelectedDayBackgroundColorProperty =
          BindableProperty.Create(nameof(SelectedDayBackgroundColor), typeof(Color), typeof(DateTimePicker), Color.FromHex("#2196F3"));

        public Color SelectedDayBackgroundColor
        {
            get => (Color)GetValue(SelectedDayBackgroundColorProperty);
            set => SetValue(SelectedDayBackgroundColorProperty, value);
        }


        public static readonly BindableProperty ArrowsColorProperty =
          BindableProperty.Create(nameof(ArrowsColor), typeof(Color), typeof(DateTimePicker), Color.FromHex("#2196F3"));

        public Color ArrowsColor
        {
            get => (Color)GetValue(ArrowsColorProperty);
            set => SetValue(ArrowsColorProperty, value);
        }

        public static readonly BindableProperty FooterArrowVisibleProperty =
            BindableProperty.Create(nameof(FooterArrowVisible), typeof(bool), typeof(DateTimePicker), true);

        public bool FooterArrowVisible
        {
            get => (bool)GetValue(FooterArrowVisibleProperty);
            set => SetValue(FooterArrowVisibleProperty, value);
        }

        public static readonly BindableProperty HeaderSectionVisibleProperty =
            BindableProperty.Create(nameof(HeaderSectionVisible), typeof(bool), typeof(DateTimePicker), true);

        public bool HeaderSectionVisible
        {
            get => (bool)GetValue(HeaderSectionVisibleProperty);
            set => SetValue(HeaderSectionVisibleProperty, value);
        }

        public static readonly BindableProperty DisplaySectionVisibleProperty =
            BindableProperty.Create(nameof(DisplaySectionVisible), typeof(bool), typeof(DateTimePicker), true);

        public bool DisplaySectionVisible
        {
            get => (bool)GetValue(DisplaySectionVisibleProperty);
            set => SetValue(DisplaySectionVisibleProperty, value);
        }

        public static readonly BindableProperty TodayOutlineColorProperty =
          BindableProperty.Create(nameof(TodayOutlineColor), typeof(Color), typeof(DateTimePicker), Color.FromHex("#FF4081"));

        public Color TodayOutlineColor
        {
            get => (Color)GetValue(TodayOutlineColorProperty);
            set => SetValue(TodayOutlineColorProperty, value);
        }

        public static readonly BindableProperty TodayFillColorProperty =
          BindableProperty.Create(nameof(TodayFillColor), typeof(Color), typeof(DateTimePicker), Color.Transparent);

        public Color TodayFillColor
        {
            get => (Color)GetValue(TodayFillColorProperty);
            set => SetValue(TodayFillColorProperty, value);
        }

        public static readonly BindableProperty HoursMinutesTextColorProperty =
  BindableProperty.Create(nameof(HoursMinutesTextColor), typeof(Color), typeof(DateTimePicker), Color.Red);

        public Color HoursMinutesTextColor
        {
            get => (Color)GetValue(HoursMinutesTextColorProperty);
            set => SetValue(HoursMinutesTextColorProperty, value);
        }

        public static readonly BindableProperty HeaderSectionTemplateProperty =
          BindableProperty.Create(nameof(HeaderSectionTemplate), typeof(DataTemplate), typeof(DateTimePicker), new DataTemplate(() => new DefaultHeaderSection()));

        public DataTemplate HeaderSectionTemplate
        {
            get => (DataTemplate)GetValue(HeaderSectionTemplateProperty);
            set => SetValue(HeaderSectionTemplateProperty, value);
        }

        public static readonly BindableProperty DispalySectionTemplateProperty =
          BindableProperty.Create(nameof(DisplaySectionTemplate), typeof(DataTemplate), typeof(DateTimePicker), new DataTemplate(() => new DefaultDisplaySection()));

        public DataTemplate DisplaySectionTemplate
        {
            get => (DataTemplate)GetValue(DispalySectionTemplateProperty);
            set => SetValue(DispalySectionTemplateProperty, value);
        }

        public static readonly BindableProperty TimeSectionTemplateProperty =
        BindableProperty.Create(nameof(TimeSectionTemplate), typeof(DataTemplate), typeof(DateTimePicker), new DataTemplate(() => new TimeSection()));

        public DataTemplate TimeSectionTemplate
        {
            get => (DataTemplate)GetValue(TimeSectionTemplateProperty);
            set => SetValue(TimeSectionTemplateProperty, value);
        }

        public static readonly BindableProperty MonthTextProperty =
          BindableProperty.Create(nameof(MonthText), typeof(string), typeof(DateTimePicker), null);

        public string MonthText
        {
            get => (string)GetValue(MonthTextProperty);
            set => SetValue(MonthTextProperty, value);
        }

        public static readonly BindableProperty SelectedDateTextProperty =
          BindableProperty.Create(nameof(SelectedDateText), typeof(string), typeof(DateTimePicker), null);

        public string SelectedDateText
        {
            get => (string)GetValue(SelectedDateTextProperty);
            set => SetValue(SelectedDateTextProperty, value);
        }

        public static readonly BindableProperty SelectedDateTextFormatProperty =
          BindableProperty.Create(nameof(SelectedDateTextFormat), typeof(string), typeof(DateTimePicker), "dd MMM yyyy HH:mm");

        public string SelectedDateTextFormat
        {
            get => (string)GetValue(SelectedDateTextFormatProperty);
            set => SetValue(SelectedDateTextFormatProperty, value);
        }

        public static readonly BindableProperty CalendarSectionShownProperty =
          BindableProperty.Create(nameof(CalendarSectionShown), typeof(bool), typeof(DateTimePicker), true);

        public bool CalendarSectionShown
        {
            get => (bool)GetValue(CalendarSectionShownProperty);
            set => SetValue(CalendarSectionShownProperty, value);
        }

        public static readonly BindableProperty DayViewSizeProperty =
          BindableProperty.Create(nameof(DayViewSize), typeof(double), typeof(DateTimePicker), 30.0);

        public double DayViewSize
        {
            get => (double)GetValue(DayViewSizeProperty);
            set => SetValue(DayViewSizeProperty, value);
        }

        public static readonly BindableProperty DayViewCornerRadiusProperty =
          BindableProperty.Create(nameof(DayViewCornerRadius), typeof(float), typeof(DateTimePicker), 15f);

        public float DayViewCornerRadius
        {
            get => (float)GetValue(DayViewCornerRadiusProperty);
            set => SetValue(DayViewCornerRadiusProperty, value);
        }

        public static readonly BindableProperty DaysTitleMaximumLengthProperty =
          BindableProperty.Create(nameof(DaysTitleMaximumLength), typeof(DaysTitleMaxLength), typeof(DateTimePicker), DaysTitleMaxLength.ThreeChars, propertyChanged: T);

        private static void T(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable is DateTimePicker vm)
                vm.monthDaysView.DaysTitleMaximumLength = (DaysTitleMaxLength)newValue;
        }

        public DaysTitleMaxLength DaysTitleMaximumLength
        {
            get => (DaysTitleMaxLength)GetValue(DaysTitleMaximumLengthProperty);
            set => SetValue(DaysTitleMaximumLengthProperty, value);
        }

        public static readonly BindableProperty DaysTitleHeightProperty =
          BindableProperty.Create(nameof(DaysTitleHeight), typeof(double), typeof(DateTimePicker), 30.0);

        public double DaysTitleHeight
        {
            get => (double)GetValue(DaysTitleHeightProperty);
            set => SetValue(DaysTitleHeightProperty, value);
        }

        public static readonly BindableProperty DaysLabelStyleProperty =
          BindableProperty.Create(nameof(DaysLabelStyle), typeof(Style), typeof(DateTimePicker), Device.Styles.BodyStyle);

        public Style DaysLabelStyle
        {
            get => (Style)GetValue(DaysLabelStyleProperty);
            set => SetValue(DaysLabelStyleProperty, value);
        }

        public static readonly BindableProperty DaysTitleLabelStyleProperty =
          BindableProperty.Create(nameof(DaysTitleLabelStyle), typeof(Style), typeof(DateTimePicker), null);

        public Style DaysTitleLabelStyle
        {
            get => (Style)GetValue(DaysTitleLabelStyleProperty);
            set => SetValue(DaysTitleLabelStyleProperty, value);
        }

        /// <summary> Bindable property for DisableSwipeDetection </summary>
        public static readonly BindableProperty DisableSwipeDetectionProperty =
          BindableProperty.Create(nameof(DisableSwipeDetection), typeof(bool), typeof(DateTimePicker), false);

        /// <summary>
        /// <para> Disables the swipe detection (needs testing on iOS) </para>
        /// Could be useful if your superview has its own swipe-detection logic.
        /// Also see if <seealso cref="SwipeUpCommand"/>, <seealso cref="SwipeUpToHideEnabled"/>, <seealso cref="SwipeLeftCommand"/>, <seealso cref="SwipeRightCommand"/> or <seealso cref="SwipeToChangeMonthEnabled"/> is useful to you.
        /// </summary>
        public bool DisableSwipeDetection
        {
            get => (bool)GetValue(DisableSwipeDetectionProperty);
            set => SetValue(DisableSwipeDetectionProperty, value);
        }

        /// <summary> Bindable property for SwipeUpCommand </summary>
        public static readonly BindableProperty SwipeUpCommandProperty =
          BindableProperty.Create(nameof(SwipeUpCommand), typeof(ICommand), typeof(DateTimePicker), null);

        /// <summary> Activated when user swipes-up over days view </summary>
        public ICommand SwipeUpCommand
        {
            get => (ICommand)GetValue(SwipeUpCommandProperty);
            set => SetValue(SwipeUpCommandProperty, value);
        }

        /// <summary> Bindable property for SwipeUpToHideEnabled </summary>
        public static readonly BindableProperty SwipeUpToHideEnabledProperty =
          BindableProperty.Create(nameof(SwipeUpToHideEnabled), typeof(bool), typeof(DateTimePicker), true);

        /// <summary> Enable/disable default swipe-up action for showing/hiding calendar </summary>
        public bool SwipeUpToHideEnabled
        {
            get => (bool)GetValue(SwipeUpToHideEnabledProperty);
            set => SetValue(SwipeUpToHideEnabledProperty, value);
        }

        /// <summary> Bindable property for SwipeLeftCommand </summary>
        public static readonly BindableProperty SwipeLeftCommandProperty =
          BindableProperty.Create(nameof(SwipeLeftCommand), typeof(ICommand), typeof(DateTimePicker), null);

        /// <summary> Activated when user swipes-left over days view </summary>
        public ICommand SwipeLeftCommand
        {
            get => (ICommand)GetValue(SwipeLeftCommandProperty);
            set => SetValue(SwipeLeftCommandProperty, value);
        }

        /// <summary> Bindable property for SwipeRightCommand </summary>
        public static readonly BindableProperty SwipeRightCommandProperty =
          BindableProperty.Create(nameof(SwipeRightCommand), typeof(ICommand), typeof(DateTimePicker), null);

        /// <summary> Activated when user swipes-right over days view </summary>
        public ICommand SwipeRightCommand
        {
            get => (ICommand)GetValue(SwipeRightCommandProperty);
            set => SetValue(SwipeRightCommandProperty, value);
        }


        /// <summary> Bindable property for SwipeToChangeMonthEnabled </summary>
        public static readonly BindableProperty SwipeToChangeMonthEnabledProperty =
          BindableProperty.Create(nameof(SwipeToChangeMonthEnabled), typeof(bool), typeof(DateTimePicker), true);

        /// <summary> Enable/disable default swipe actions for changing months </summary>
        public bool SwipeToChangeMonthEnabled
        {
            get => (bool)GetValue(SwipeToChangeMonthEnabledProperty);
            set => SetValue(SwipeToChangeMonthEnabledProperty, value);
        }

        /// <summary>
        /// Bindable property for DayTapped
        /// </summary>
        public static readonly BindableProperty DayTappedCommandProperty =
            BindableProperty.Create(nameof(DayTappedCommand), typeof(ICommand), typeof(DateTimePicker), null);

        /// <summary>
        /// Action to run after a day has been tapped.
        /// </summary>
        public ICommand DayTappedCommand
        {
            get => (ICommand)GetValue(DayTappedCommandProperty);
            set => SetValue(DayTappedCommandProperty, value);
        }

        /// <summary> Bindable property for MinimumDate </summary>
        public static readonly BindableProperty MinimumDateProperty =
          BindableProperty.Create(nameof(MinimumDate), typeof(DateTime), typeof(DateTimePicker), DateTime.MinValue);

        /// <summary> Minimum date which can be selected </summary>
        public DateTime MinimumDate
        {
            get => (DateTime)GetValue(MinimumDateProperty);
            set => SetValue(MinimumDateProperty, value);
        }

        /// <summary> Bindable property for MaximumDate </summary>
        public static readonly BindableProperty MaximumDateProperty =
          BindableProperty.Create(nameof(MaximumDate), typeof(DateTime), typeof(DateTimePicker), DateTime.MaxValue);

        /// <summary> Maximum date which can be selected </summary>
        public DateTime MaximumDate
        {
            get => (DateTime)GetValue(MaximumDateProperty);
            set => SetValue(MaximumDateProperty, value);
        }

        /// <summary> Bindable property for DisabledDayColor </summary>
        public static readonly BindableProperty DisabledDayColorProperty =
          BindableProperty.Create(nameof(DisabledDayColor), typeof(Color), typeof(DateTimePicker), Color.FromHex("#ECECEC"));

        /// <summary> Color for days which are out of MinimumDate - MaximumDate range </summary>
        public Color DisabledDayColor
        {
            get => (Color)GetValue(DisabledDayColorProperty);
            set => SetValue(DisabledDayColorProperty, value);
        }


        public static readonly BindableProperty AnimateCalendarProperty =
            BindableProperty.Create(nameof(AnimateCalendar), typeof(bool), typeof(DateTimePicker), true);

        public bool AnimateCalendar
        {
            get => (bool)GetValue(AnimateCalendarProperty);
            set => SetValue(AnimateCalendarProperty, value);
        }

        #endregion

        #region Public commands
        /// <summary>
        /// When executed calendar moves to previous month.
        /// Read only command to use in your <see cref="HeaderSectionTemplate"/> or <see cref="DisplaySectionTemplate"/>
        /// </summary>
        public ICommand PrevMonthCommand { get; }

        /// <summary>
        /// When executed calendar moves to next month.
        /// Read only command to use in your <see cref="HeaderSectionTemplate"/> or <see cref="DisplaySectionTemplate"/>
        /// </summary>
        public ICommand NextMonthCommand { get; }

        /// <summary>
        /// When executed calendar moves to previous year.
        /// Read only command to use in your <see cref="HeaderSectionTemplate"/> or <see cref="DisplaySectionTemplate"/>
        /// </summary>
        public ICommand PrevYearCommand { get; }

        /// <summary>
        /// When executed calendar moves to next year.
        /// Read only command to use in your <see cref="HeaderSectionTemplate"/> or <see cref="DisplaySectionTemplate"/>
        /// </summary>
        public ICommand NextYearCommand { get; }

        /// <summary>
        /// When executed shows/hides the calendar's current month days view.
        /// Read only command to use in your <see cref="HeaderSectionTemplate"/> or <see cref="DisplaySectionTemplate"/>
        /// </summary>
        public ICommand ShowHideCalendarCommand { get; }

        #endregion

        #region Private methods

        private string[] GenerateTimeRange(int number)
        {
            string[] result = new string[number + 1];
            for (int i = 0; i <= number; i++)
            {
                result[i] = i.ToString("00");
            }
            return result;
        }

        #endregion

        #region Event Handlers

        private static void OnYearChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable is DateTimePicker calendar && calendar.MonthYear.Year != (int)newValue)
                calendar.MonthYear = new DateTime((int)newValue, calendar.Month, 1);
        }

        private static void OnMonthChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (!(newValue is int newMonth) || newMonth <= 0 || newMonth > 12)
                throw new ArgumentException("Month must be between 1 and 12.");

            if (bindable is DateTimePicker calendar && calendar.MonthYear.Month != newMonth)
                calendar.MonthYear = new DateTime(calendar.Year, newMonth, 1);
        }

        private static void OnHourChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (!(newValue is int newHour) || newHour < 0 || newHour > 23)
                return;

            if (bindable is DateTimePicker calendar && oldValue is int oldHour && oldHour != newHour)
            {
                calendar.UpdateSelectedDateLabel();
            }
        }

        private static void OnMinuteChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (!(newValue is int newMinute) || newMinute < 0 || newMinute > 59)
                return;

            if (bindable is DateTimePicker calendar && oldValue is int oldMinute && oldMinute != newMinute)
            {
                calendar.UpdateSelectedDateLabel();
            }

        }

        private static void OnMonthYearChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable is DateTimePicker calendar && newValue is DateTime newDateTime)
            {
                if (calendar.Month != newDateTime.Month)
                    calendar.Month = newDateTime.Month;

                if (calendar.Year != newDateTime.Year)
                    calendar.Year = newDateTime.Year;
            }
        }

        /// <summary> Method that is called when a bound property is changed. </summary>
        /// <param name="propertyName">The name of the bound property that changed.</param>
        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            switch (propertyName)
            {
                case nameof(MonthYear):
                    UpdateMonthLabel();
                    break;

                case nameof(SelectedDate):
                    UpdateSelectedDateLabel();
                    break;

                case nameof(Culture):
                    if (MonthYear.Month > 0)
                        UpdateMonthLabel();
                        UpdateSelectedDateLabel();
                    break;

                case nameof(CalendarSectionShown):
                    ShowHideCalendarSection();
                    break;
            }
        }

        private void UpdateMonthLabel()
        {
            MonthText = Culture.DateTimeFormat.MonthNames[MonthYear.Month - 1].Capitalize();
        }

        private void UpdateSelectedDateLabel()
        {
            SelectedDateText = new DateTime(SelectedDate.Year, SelectedDate.Month, SelectedDate.Day, SelectedHour, SelectedMinute, 0, 0).ToString(SelectedDateTextFormat, Culture);
        }

        private void ShowHideCalendarSection()
        {
            if (_calendarSectionAnimating)
                return;

            _calendarSectionAnimating = true;

            var animation = CalendarSectionShown ? _calendarSectionAnimateShow : _calendarSectionAnimateHide;
            var prevState = CalendarSectionShown;

            animation.Commit(this, CalendarSectionAnimationId, CalendarSectionAnimationRate, CalendarSectionAnimationDuration, finished: (value, cancelled) =>
            {
                _calendarSectionAnimating = false;

                if (prevState != CalendarSectionShown)
                    ToggleCalendarSectionVisibility();
            });
        }

        private void UpdateCalendarSectionHeight()
        {
            _calendarSectionHeight = calendarContainer.Height;
        }


        private void OnCalendarContainerSizeChanged(object sender, EventArgs e)
        {
            if (calendarContainer.Height > 0 && !_calendarSectionAnimating)
                UpdateCalendarSectionHeight();
        }

        private void OnSwipedRight(object sender, EventArgs e)
        {
            SwipeRightCommand?.Execute(null);

            if (SwipeToChangeMonthEnabled)
                PrevMonth();
        }

        private void OnSwipedLeft(object sender, EventArgs e)
        {
            SwipeLeftCommand?.Execute(null);

            if (SwipeToChangeMonthEnabled)
                NextMonth();
        }

        private void OnSwipedUp(object sender, EventArgs e)
        {
            SwipeUpCommand?.Execute(null);

            if (SwipeUpToHideEnabled)
                ToggleCalendarSectionVisibility();
        }

        #endregion

        #region Other methods

        private void ChangeDisplayedMonth(int monthsToAdd)
        {
            var targetDisplayedMonth = MonthYear.AddMonths(monthsToAdd);
            if (targetDisplayedMonth <= MaximumDate && targetDisplayedMonth >= MinimumDate ||
                targetDisplayedMonth.Month == MaximumDate.Month ||
                targetDisplayedMonth.Month == MinimumDate.Month)
            {
                MonthYear = targetDisplayedMonth;
            }
        }

        private void ChangeDisplayedYear(int yearsToAdd)
        {
            var targetDisplayedMonth = MonthYear.AddYears(yearsToAdd);
            if (targetDisplayedMonth <= MaximumDate && targetDisplayedMonth >= MinimumDate)
            {
                MonthYear = targetDisplayedMonth;

            }
            else if (targetDisplayedMonth.Year == MaximumDate.Year)
            {
                MonthYear = MaximumDate;
            }
            else if (targetDisplayedMonth.Year == MinimumDate.Year)
            {
                MonthYear = MinimumDate;
            }
        }



        private void PrevMonth() => ChangeDisplayedMonth(-1);

        private void NextMonth() => ChangeDisplayedMonth(1);

        private void PrevYear() => ChangeDisplayedYear(-1);

        private void NextYear() => ChangeDisplayedYear(1);



        private void ToggleCalendarSectionVisibility()
            => CalendarSectionShown = !CalendarSectionShown;

        private void AnimateMonths(double currentValue)
        {
            calendarSectionRow.Height = new GridLength(_calendarSectionHeight * currentValue);
            calendarContainer.TranslationY = _calendarSectionHeight * (currentValue - 1);
            calendarContainer.Opacity = currentValue * currentValue * currentValue;
        }

        #endregion
    }
}