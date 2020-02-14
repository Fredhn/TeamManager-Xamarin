using System;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using TeamManager.Models;
using TeamManager.ViewModels;
using TeamManager.Controls;
using Rg.Plugins.Popup.Services;
using System.Threading.Tasks;

namespace TeamManager.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class VacationDetailPage : ContentPage
    {
        #region Properties
        public VacationDetailViewModel Context
        {
            get
            {
                return this.BindingContext as VacationDetailViewModel;
            }
        }
        public DateRangePopup DatesPopup { get; private set; }
        #endregion

        public VacationDetailPage(VacationDetailViewModel viewModel)
        {
            InitializeComponent();

            this.BindingContext = ViewModelCollection.VacationDetails = viewModel;
        }

        #region Methods
        public void SetBindingContext()
        {
            this.BindingContext = ViewModelCollection.VacationDetails;
        }
        async void Save_Clicked(object sender, EventArgs e)
        {
            var emp = this.Context.Vacation;
            //if (!String.IsNullOrEmpty(emp.FirstName) &&
            //   !String.IsNullOrEmpty(emp.AdmissionDate.ToString()))
            //    await App.Database._employee.SaveEmployeeAsync(emp);
            //MessagingCenter.Send(this, "AddItem", Employee);
            await Navigation.PopModalAsync();
        }

        async void Cancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();


            if (this.Context.Vacations.Count == 0)
                this.Context.LoadItemsCommand.Execute(null);

            this.SetBindingContext();
        }
        private async Task ShowPopup()
        {
            DatesPopup = new DateRangePopup(this.Context.Vacation);
            await PopupNavigation.Instance.PushAsync(DatesPopup);
        }

        private void TapGestureRecognizer_BtnDatesPopup(object sender, EventArgs e)
        {
            Task.Run(async () => await this.ShowPopup());
        }
        #endregion

        private void TapGestureRecognizer_BtnDeletePeriod(object sender, EventArgs e)
        {
            var item = e;
        }
    }
}