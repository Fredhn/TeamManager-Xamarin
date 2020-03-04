using Rg.Plugins.Popup.Services;
using System;
using System.ComponentModel;
using System.Linq;
using TeamManager.Controls;
using TeamManager.Services;
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
        public InformationPopup informationPopup { get; private set; }

        public CalendarPage()
        {
            InitializeComponent();

            ccControls.Month = Convert.ToString(DateTime.Now.Month);
            ccControls.Year = Convert.ToString(DateTime.Now.Year);
            //ccControls.SelectedDay = Convert.ToString(DateTime.Now.Day);
            //ccControls.CreateAlertDate();

            SetBindingContext();
        }

        public void SetBindingContext()
        {
            this.BindingContext = ViewModelCollection.Calendar;
        }

        private async void ccControls_DateClicked(object sender, EventArgs e)
        {
            var item = (Xamarin.Forms.Button)sender;

            var day = int.Parse(item.Text);
            var month = int.Parse(ccControls.Month);
            var year = int.Parse(ccControls.Year);

            var data = ccControls.AlertDates.Where(x => x.ExpiringDates.Any(y => y.Day == day && y.Month == month && y.Year == year));

            if(data.FirstOrDefault() != null)
            {
                string content = "\nExpired vacations date(s): \n\n";
                int itemCounter = 1;
                foreach (var d in data.FirstOrDefault().ExpiringDates)
                {
                    content += "•\t\t\t\t" + d.ToShortDateString() + "\n";
                    itemCounter++;
                }

                informationPopup = new InformationPopup("Expired Vacations", data.FirstOrDefault().EmployeeName, content);
                await PopupNavigation.Instance.PushAsync(informationPopup);
            }
            
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            ccControls.CreateAlertDate();
            //this.Context.LoadVacationsCalendarCommand.Execute(null);
        }
    }
}