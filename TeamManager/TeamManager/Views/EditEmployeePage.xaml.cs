using System;
using System.Collections.Generic;
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
    public partial class EditEmployeePage : ContentPage
    {
        #region Properties
		public EmployeeEditViewModel Context
		{
			get
			{
				return this.BindingContext as EmployeeEditViewModel;
			}
		}

        public Employee Employee { get; set; }
        #endregion


        public EditEmployeePage(EmployeeEditViewModel viewModel)
        {
            InitializeComponent();

            BindingContext = ViewModelCollection.EmployeeEdit = viewModel;
        }

        #region Methods
        public void SetBindingContext()
        {
            this.BindingContext = ViewModelCollection.EmployeeEdit = new EmployeeEditViewModel();
        }
        async void Save_Clicked(object sender, EventArgs e)
        {
            var emp = this.Context.Employee;
            if(!String.IsNullOrEmpty(emp.FirstName) && 
               !String.IsNullOrEmpty(emp.AdmissionDate.ToString()))
            {
                await App.Database._employee.SaveEmployeeAsync(emp);
                ViewModelCollection.EmployeeDetails.Employee = emp;
                //MessagingCenter.Send(this, "AddItem", Employee);
                await Navigation.PopModalAsync();
            }
                
        }

        async void Cancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
        #endregion
    }
}