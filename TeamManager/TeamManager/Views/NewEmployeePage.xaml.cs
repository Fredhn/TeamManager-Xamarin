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
    public partial class NewEmployeePage : ContentPage
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


        public NewEmployeePage()
        {
            InitializeComponent();

            this.SetBindingContext();
        }

        #region Methods
        public void SetBindingContext()
        {
            this.BindingContext = ViewModelCollection.EmployeeDetails = new EmployeeDetailViewModel();
        }
        async void Save_Clicked(object sender, EventArgs e)
        {
            var emp = this.Context.Employee;
            if(!String.IsNullOrEmpty(emp.FirstName) && 
               !String.IsNullOrEmpty(emp.AdmissionDate.ToString()))
                await App.Database._employee.SaveEmployeeAsync(emp);
            //MessagingCenter.Send(this, "AddItem", Employee);
            await Navigation.PopModalAsync();
        }

        async void Cancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
        #endregion
    }
}