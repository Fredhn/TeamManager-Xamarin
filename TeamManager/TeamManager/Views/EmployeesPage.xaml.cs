using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using TeamManager.Models;
using TeamManager.Views;
using TeamManager.ViewModels;

namespace TeamManager.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class EmployeesPage : ContentPage
    {

        #region Properties
        public EmployeesViewModel Context
        {
            get
            {
                return this.BindingContext as EmployeesViewModel;
            }
        }
        #endregion

        public EmployeesPage()
        {
            InitializeComponent();

            this.SetBindingContext();
        }

        #region Methods
        public void SetBindingContext()
        {
            this.BindingContext = ViewModelCollection.Employees;
        }
        async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var item = args.SelectedItem as Employee;
            if (item == null)
                return;

            await Navigation.PushAsync(new EmployeeDetailPage(new EmployeeDetailViewModel(item)));

            // Manually deselect item.
            ItemsListView.SelectedItem = null;
        }

        async void AddItem_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new NewEmployeePage()));
        }
        
        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (this.Context.Employees.Count == 0)
                this.Context.LoadItemsCommand.Execute(null);

            this.SetBindingContext();
        }
        #endregion
    }
}