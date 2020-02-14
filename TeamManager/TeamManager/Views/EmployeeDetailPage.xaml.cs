using System;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using TeamManager.Models;
using TeamManager.ViewModels;

namespace TeamManager.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class EmployeeDetailPage : ContentPage
    {
        #region Properties
        public EmployeeDetailViewModel Context
        {
            get
            {
                return this.BindingContext as EmployeeDetailViewModel;
            }
        }

        public Employee Employee { get; set; }
        #endregion

        public EmployeeDetailPage(EmployeeDetailViewModel viewModel)
        {
            InitializeComponent();

            BindingContext = ViewModelCollection.EmployeeDetails = viewModel;
        }

        public EmployeeDetailPage()
        {
            InitializeComponent();

            this.SetBindingContext();
        }

        #region Methods
        public void SetBindingContext()
        {
            this.BindingContext = ViewModelCollection.EmployeeDetails;
        }
        async void Save_Clicked(object sender, EventArgs e)
        {
            var emp = this.Context.Employee;
            if (!String.IsNullOrEmpty(emp.FirstName) &&
               !String.IsNullOrEmpty(emp.AdmissionDate.ToString()))
                await App.Database._employee.SaveEmployeeAsync(emp);
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

            if (this.Context.SpentVacations.Count == 0)
                this.Context.LoadItemsCommand.Execute(null);

            this.SetBindingContext();
        }
        #endregion

        async void SpentVacationSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = e.SelectedItem as SpentVacations;
            if (item == null)
                return;

            await Navigation.PushAsync(new VacationDetailPage(new VacationDetailViewModel(item)));

            // Manually deselect item.
            SpentVacationsListView.SelectedItem = null;
        }

        async void EditItem_Clicked(object sender, EventArgs e)
        {
            var vm = new EmployeeEditViewModel();
            vm.Employee = this.Context.Employee;
            await Navigation.PushModalAsync(new NavigationPage(new EditEmployeePage(vm)));
        }
    }
}