using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamManager.Models;
using TeamManager.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TeamManager.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CalendarControl : ContentView
    {
        public event EventHandler DateClicked;
        public static readonly BindableProperty YearProperty = BindableProperty.Create(
        propertyName: "Year",
        returnType: typeof(string),
        declaringType: typeof(CalendarControl),
        defaultValue: "2020",
        defaultBindingMode: BindingMode.TwoWay,
        propertyChanged: YearPropertyChanged);

        public static readonly BindableProperty MonthProperty = BindableProperty.Create(
        propertyName: "Month",
        returnType: typeof(string),
        declaringType: typeof(CalendarControl),
        defaultValue: "1",
        defaultBindingMode: BindingMode.TwoWay,
        propertyChanged: MonthPropertyChanged);

        public static readonly BindableProperty SelectedDayProperty = BindableProperty.Create(
        propertyName: "SelectedDay",
        returnType: typeof(string),
        declaringType: typeof(CalendarControl),
        defaultValue: "0",
        defaultBindingMode: BindingMode.TwoWay,
        propertyChanged: SelectedDayPropertyChanged);

        public static readonly BindableProperty AlertDatesProperty = BindableProperty.Create(
        propertyName: "AlertDates",
        returnType: typeof(ObservableCollection<ExpiredVacations>),
        declaringType: typeof(CalendarControl),
        defaultValue: null,
        defaultBindingMode: BindingMode.TwoWay,
        propertyChanged: AlertDatesPropertyChanged);
        public ObservableCollection<ExpiredVacations> AlertDates
        {
            get { return (ObservableCollection<ExpiredVacations>)GetValue(AlertDatesProperty); }
            set { SetValue(AlertDatesProperty, value); }
        }
        public string SelectedDay
        {
            get { return GetValue(SelectedDayProperty).ToString(); }
            set { SetValue(SelectedDayProperty, value); }
        }
        public string Year
        {
            get { return GetValue(YearProperty).ToString(); }
            set { SetValue(YearProperty, value); }
        }
        private static void YearPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (CalendarControl)bindable;
            control.lblYear.Text = newValue.ToString();
            control.lblY.Text = control.lblYear.Text;
        }
        private static void SelectedDayPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (CalendarControl)bindable;
            control.lblSelectedDay.Text = newValue.ToString();
        }
        private static void AlertDatesPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (CalendarControl)bindable;
            //control.lblSelectedDay.Text = newValue.ToString();
        }
        public string Month
        {
            get { return GetValue(MonthProperty).ToString(); }
            set { SetValue(MonthProperty, value); }
        }
        private static void MonthPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (CalendarControl)bindable;
            control.lblMonth.Text = newValue.ToString();
            control.lblM.Text = control.lblMonth.Text;
        }
        long SelectedYear;
        string SelectedMonth = "";
        static int SelectedMonthInt = 0;
        string selectedday;
        Grid gridCalendar;

        public CalendarControl()
        {
            InitializeComponent();

            lblM.TextChanged += MonthChanged_TextChanged;
            lblY.TextChanged += YearChanged_TextChanged;
            lblM.TextChanged += MonthChanged_TextChanged;
            lblSelectedDay.TextChanged += YearChanged_TextChanged;
        }

        private void YearChanged_TextChanged(object sender, TextChangedEventArgs e)
        {
            var y = Year;
            var m = Month;
            selectedday = SelectedDay;
            SelectedYear = Convert.ToInt64(y);
            lblSelectedMonth.Text = new DateTime(Convert.ToInt32(y), Convert.ToInt32(m), 1).ToString("MMMM");
            SelectedMonthInt = Convert.ToInt32(m);
            lblYear.Text = Convert.ToString(y);
            CreateGridCalendar();
            CreateDate(selectedday);
        }
        private void MonthChanged_TextChanged(object sender, TextChangedEventArgs e)
        {
            var y = Year;
            var m = Month;
            selectedday = SelectedDay;
            SelectedYear = Convert.ToInt64(y);
            lblSelectedMonth.Text = new DateTime(Convert.ToInt32(y), Convert.ToInt32(m), 1).ToString("MMMM");
            SelectedMonthInt = Convert.ToInt32(m);
            lblYear.Text = Convert.ToString(y);
            CreateGridCalendar();
            CreateDate(selectedday);
        }
        private void CreateGridCalendar()
        {
            #region Grid Calendar
            gridCalendar = new Grid
            {
                BackgroundColor = Color.LightBlue,
                Padding = 0,
                ColumnSpacing = 0,
                Margin = new Thickness(10, 0, 10, 0),
                RowDefinitions =
                    {
                    new RowDefinition { Height = 1 },
                    new RowDefinition { Height = 50 },
                    new RowDefinition { Height = 1 },
                    new RowDefinition { Height = 50 },
                    new RowDefinition { Height = 1 },
                    new RowDefinition { Height = 50 },
                    new RowDefinition { Height = 1 },
                    new RowDefinition { Height = 50 },
                    new RowDefinition { Height = 1 },
                    new RowDefinition { Height = 50 },
                    new RowDefinition { Height = 1 },
                    new RowDefinition { Height = 50 },
                    new RowDefinition { Height = 1 },
                    new RowDefinition { Height = 50 },
                    new RowDefinition { Height = 1 },
                    },
                ColumnDefinitions =
                    {
                    new ColumnDefinition { Width =  1 },
                    new ColumnDefinition { Width =  50 },
                    new ColumnDefinition { Width =  1 },
                    new ColumnDefinition { Width =  50 },
                    new ColumnDefinition { Width =  1 },
                    new ColumnDefinition { Width =  50 },
                    new ColumnDefinition { Width =  1 },
                    new ColumnDefinition { Width =  50 },
                    new ColumnDefinition { Width =  1 },
                    new ColumnDefinition { Width =  50 },
                    new ColumnDefinition { Width =  1 },
                    new ColumnDefinition { Width =  50 },
                    new ColumnDefinition { Width =  1 },
                    new ColumnDefinition { Width =  50 },
                    new ColumnDefinition { Width =  1 },

                    }
            };
            Button[] btn = new Button[7];
            string[] weekname = new string[7] { "Mo", "Tu", "We", "Th", "Fr", "Sa", "Su" };
            for (int i = 0; i <= 6; i++)
            {
                btn[i] = new Button()
                {
                    Text = weekname[i],
                    BackgroundColor = Color.LightYellow,
                    FontSize = 10,
                    CornerRadius = 0,
                    FontAttributes = FontAttributes.Bold,
                    TextColor = Color.Black,
                    HorizontalOptions = LayoutOptions.Center,
                    VerticalOptions = LayoutOptions.Center
                };
            }
            int k = 1, l = 1;
            for (int i = 0; i <= 6; i++)
            {
                gridCalendar.Children.Add(btn[i], k, l);
                k = k + 2;
            }
            // Grid.SetColumnSpan(Title, 5);
            BoxView[] boxr = new BoxView[8];

            for (int i = 0; i <= 7; i++)
            {
                boxr[i] = new BoxView()
                {
                    HeightRequest = 1,
                    BackgroundColor = Color.Black,
                    HorizontalOptions = LayoutOptions.FillAndExpand,

                };

            }
            k = 0; l = 0;
            // Creating Vertical Line
            for (int i = 0; i <= 7; i++)
            {
                gridCalendar.Children.Add(boxr[i], k, l);
                Grid.SetRowSpan(boxr[i], 15);
                k = k + 2;
            }

            k = 0; l = 0;

            BoxView[] boxc = new BoxView[8];

            for (int i = 0; i <= 7; i++)
            {
                boxc[i] = new BoxView()
                {
                    HeightRequest = 1,
                    BackgroundColor = Color.Black,
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                };
            }
            // Creating Horizontal line
            for (int i = 0; i <= 7; i++)
            {
                gridCalendar.Children.Add(boxc[i], k, l);
                Grid.SetColumnSpan(boxc[i], 15);
                l = l + 2;
            }
            gridstack.Content = null;
            gridstack.Content = gridCalendar;

            #endregion
        }

        private void btnBack_Clicked(object sender, EventArgs e)
        {
            var y = Year;
            var m = Month;
            if (Convert.ToInt32(m).Equals(1))
            {
                m = "12";
                y = (Convert.ToInt32(y) - 1).ToString();
                SelectedMonthInt = (Convert.ToInt32(m));
                lblSelectedMonth.Text = new DateTime(Convert.ToInt32(y), (Convert.ToInt32(m)), 1).ToString("MMMM");
            }
            else
            {
                SelectedMonthInt = (Convert.ToInt32(m) - 1);
                lblSelectedMonth.Text = new DateTime(Convert.ToInt32(y), (Convert.ToInt32(m) - 1), 1).ToString("MMMM");
            }

            selectedday = SelectedDay;

            Month = SelectedMonthInt.ToString();

            SelectedYear = Convert.ToInt64(y);
            lblYear.Text = Convert.ToString(y);
            Year = y;

            CreateGridCalendar();
            CreateAlertDate();
        }

        private void btnForward_Clicked(object sender, EventArgs e)
        {
            var y = Year;
            var m = Month;

            if (Convert.ToInt32(m).Equals(12))
            {
                m = "1";
                y = (Convert.ToInt32(y) + 1).ToString();
                SelectedMonthInt = (Convert.ToInt32(m));
                lblSelectedMonth.Text = new DateTime(Convert.ToInt32(y), (Convert.ToInt32(m)), 1).ToString("MMMM");
            }
            else
            {
                lblSelectedMonth.Text = new DateTime(Convert.ToInt32(y), (1 + Convert.ToInt32(m)), 1).ToString("MMMM");
                SelectedMonthInt = (Convert.ToInt32(m) + 1);
            }

            selectedday = SelectedDay;

            Month = SelectedMonthInt.ToString();

            SelectedYear = Convert.ToInt64(y);
            lblYear.Text = Convert.ToString(y);
            Year = y;

            CreateGridCalendar();
            CreateAlertDate();
        }


        private void lblmonth_tapped(object sender, EventArgs e)
        {
            lblDialogTitle.Text = "Select Month";
            lstView_data.ItemsSource = null;
            try
            {
                System.Collections.Generic.List<DataMaster> VList = new List<DataMaster>();
                for (int i = 1; i <= 12; i++)
                {
                    VList.Add(new DataMaster() { dataid = i, data = CultureInfo.CurrentUICulture.DateTimeFormat.MonthNames[i - 1] });
                }
                lstView_data.ItemsSource = VList;
            }
            catch
            {

            }

            overlay.IsVisible = true;
        }


        private void lblYear_tapped(object sender, EventArgs e)
        {
            lblDialogTitle.Text = "Select Year";
            lstView_data.ItemsSource = null;
            try
            {
                List<int> listYears = Enumerable.Range(1980, DateTime.Now.AddYears(50).Year - 1980 + 1).ToList();
                List<DataMaster> VList = new List<DataMaster>();
                for (int i = 1; i < listYears.Count; i++)
                {
                    VList.Add(new DataMaster() { dataid = i, data = Convert.ToString(listYears[i]) });
                }

                lstView_data.ItemsSource = VList;
            }
            catch
            {

            }

            overlay.IsVisible = true;
        }

        private void dialogclose_Clicked(object sender, EventArgs e)
        {
            overlay.IsVisible = false;
        }

        private void lstView_data_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            #region FirstdayLastDay

            var item1 = (DataMaster)e.SelectedItem;
            var isNumeric = int.TryParse(item1.data, out int n);



            if (isNumeric == true)
            {
                SelectedYear = Convert.ToInt64(item1.data);
                lblYear.Text = Convert.ToString(SelectedYear);
            }
            else
            {
                SelectedMonth = Convert.ToString(item1.data);
                SelectedMonthInt = Convert.ToInt32(item1.dataid);
                lblSelectedMonth.Text = SelectedMonth;
            }

            if (SelectedMonthInt == DateTime.Now.Month && SelectedYear == DateTime.Now.Year)
            {
                //CreateDate(Convert.ToString(DateTime.Now.Day));
                CreateAlertDate();
            }
            else
            {
                CreateAlertDate();
            }

            //----------------------------------------------------------------------
            #endregion
        }

        private void CreateDate(string selectedday)
        {
            Button[] btn = new Button[31];

            //getting First Day and Last Day---------------------------------------

            DateTime startOfMonth = new DateTime(Convert.ToInt32(SelectedYear), SelectedMonthInt, 1);   //new DateTime(year, month, 1);

            DateTime endOfMonth = new DateTime(Convert.ToInt32(SelectedYear), SelectedMonthInt, DateTime.DaysInMonth(Convert.ToInt32(SelectedYear), SelectedMonthInt)); //new DateTime(year, month, DateTime.DaysInMonth(year, month));

            // DateTime dtSelectedDate = DateTime.Now;
            string dtFirstDayOfMonth = startOfMonth.ToString("ddd");
            string dtLastDayOfMonth = endOfMonth.ToString("ddd");

            int day = (int)startOfMonth.DayOfWeek;


            gridCalendar.Children.Clear();
            CreateGridCalendar();
            for (int i = startOfMonth.Day; i <= endOfMonth.Day; i++)
            {
                FontAttributes fontattributes = FontAttributes.None;
                Color color = Color.Transparent;
                Color colortext = Color.Black;
                if (i == Convert.ToInt32(selectedday))
                {
                    fontattributes = FontAttributes.Bold;
                    color = Color.White;
                    colortext = Color.Red;
                }


                btn[i - 1] = new Button()
                {
                    Text = i.ToString(),
                    FontSize = 12,
                    FontAttributes = fontattributes,
                    BackgroundColor = color,
                    TextColor = colortext


                };
                btn[i - 1].Clicked += Calendar_Clicked;
            }
            int r = 3;
            int c = 0;
            if (day == 1)
            {
                c = 1;
            }
            else if (day == 2)
            {
                c = 3;
            }
            else if (day == 3)
            {
                c = 5;
            }
            else if (day == 4)
            {
                c = 7;
            }
            else if (day == 5)
            {
                c = 9;
            }
            else if (day == 6)
            {
                c = 11;
            }
            else
            {
                c = 13;
            }


            int m = 1;

            // adding button to gridview

            for (int k = 1; k <= 7; k++)
            {
                for (int j = 1; j <= 7; j = j + 1)
                {
                    if (m > endOfMonth.Day)
                    {
                        break;
                    }
                    else
                    {

                        gridCalendar.Children.Add(btn[m - 1], c, r);
                        c = c + 2;
                        m++;

                        if (c == 15)
                        {
                            break;
                        }
                    }
                }
                c = 1;
                r = r + 2;
            }
            overlay.IsVisible = false;
        }
        
        public async void CreateAlertDate()
        {
            Button[] btn = new Button[31];

            //getting First Day and Last Day---------------------------------------

            DateTime startOfMonth = new DateTime(Convert.ToInt32(SelectedYear), SelectedMonthInt, 1);   //new DateTime(year, month, 1);

            DateTime endOfMonth = new DateTime(Convert.ToInt32(SelectedYear), SelectedMonthInt, DateTime.DaysInMonth(Convert.ToInt32(SelectedYear), SelectedMonthInt)); //new DateTime(year, month, DateTime.DaysInMonth(year, month));

            // DateTime dtSelectedDate = DateTime.Now;
            string dtFirstDayOfMonth = startOfMonth.ToString("ddd");
            string dtLastDayOfMonth = endOfMonth.ToString("ddd");

            int day = (int)startOfMonth.DayOfWeek;

            gridCalendar.Children.Clear();
            CreateGridCalendar();

            var lista = await Service_Calendar.GetAllExpiredVacations();
            AlertDates = new ObservableCollection<ExpiredVacations>(lista);

            var alertDatesToCreate = new List<DateTime>();
            foreach (var it in lista)
            {
                foreach (var d in it.ExpiringDates)
                {
                    if (d.Year == SelectedYear && d.Month == SelectedMonthInt)
                        alertDatesToCreate.Add(d);
                }
            }

            for (int i = startOfMonth.Day; i <= endOfMonth.Day; i++)
            {
                foreach (var d in alertDatesToCreate)
                {
                    if (SelectedYear == d.Year &&
                        SelectedMonthInt == d.Month &&
                        i == Convert.ToInt32(d.Day))
                    {
                        btn[i - 1] = new Button()
                        {
                            Text = i.ToString(),
                            FontSize = 12,
                            FontAttributes = FontAttributes.Bold,
                            BackgroundColor = Color.White,
                            TextColor = Color.Red
                        };
                        btn[i - 1].Clicked += Calendar_Clicked;
                    }
                    else
                    {
                        btn[i - 1] = new Button()
                        {
                            Text = i.ToString(),
                            FontSize = 12,
                            FontAttributes = FontAttributes.None,
                            BackgroundColor = Color.Transparent,
                            TextColor = Color.Black
                        };
                        btn[i - 1].Clicked += Calendar_Clicked;
                    }
                }
                if (btn[i - 1] == null)
                {
                    btn[i - 1] = new Button()
                    {
                        Text = i.ToString(),
                        FontSize = 12,
                        FontAttributes = FontAttributes.None,
                        BackgroundColor = Color.Transparent,
                        TextColor = Color.Black
                    };
                    btn[i - 1].Clicked += Calendar_Clicked;
                }
            }

            int r = 3;
            int c = 0;
            if (day == 1)
            {
                c = 1;
            }
            else if (day == 2)
            {
                c = 3;
            }
            else if (day == 3)
            {
                c = 5;
            }
            else if (day == 4)
            {
                c = 7;
            }
            else if (day == 5)
            {
                c = 9;
            }
            else if (day == 6)
            {
                c = 11;
            }
            else
            {
                c = 13;
            }


            int m = 1;

            // adding button to gridview

            for (int k = 1; k <= 7; k++)
            {
                for (int j = 1; j <= 7; j = j + 1)
                {
                    if (m > endOfMonth.Day)
                    {
                        break;
                    }
                    else
                    {

                        gridCalendar.Children.Add(btn[m - 1], c, r);
                        c = c + 2;
                        m++;

                        if (c == 15)
                        {
                            break;
                        }
                    }
                }
                c = 1;
                r = r + 2;
            }
            overlay.IsVisible = false;
        }
        private void Calendar_Clicked(object sender, EventArgs e)
        {
            DateClicked?.Invoke(sender, e);
            //await ("Demo Project", "Date Clicked " + item.Text, "Ok");
        }
    }
}