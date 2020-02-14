using System;
using System.ComponentModel;
using TeamManager.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TeamManager.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class CalendarPage : ContentPage
    {

        public CalendarViewModel Context
        {
            get
            {
                return this.BindingContext as CalendarViewModel;
            }
        }

        public CalendarPage()
        {
            InitializeComponent();

            ccControls.Month = Convert.ToString(DateTime.Now.Month);
            ccControls.Year = Convert.ToString(DateTime.Now.Year);
            ccControls.SelectedDay = Convert.ToString(DateTime.Now.Day);

            SetBindingContext();
        }

        public void SetBindingContext()
        {
            this.BindingContext = ViewModelCollection.Calendar;
        }

        private async void ccControls_DateClicked(object sender, EventArgs e)
        {
            var item = (Xamarin.Forms.Button)sender;
            await DisplayAlert("Demo Project", "Date Clicked " + item.Text, "Ok");
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            this.Context.LoadVacationsCalendarCommand.Execute(null);
        }
    }
}