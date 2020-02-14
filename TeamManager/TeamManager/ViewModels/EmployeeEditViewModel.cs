using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using TeamManager.Models;
using Xamarin.Forms;

namespace TeamManager.ViewModels
{
    public class EmployeeEditViewModel : BaseViewModel
    {
        #region Properties
        private Employee _Employee;
        public Employee Employee 
        { 
            get 
            {
                return this._Employee;
            } 
            set 
            {
                this._Employee = value;
                OnPropertyChanged("Employee");
            } 
        }
        #endregion

        public EmployeeEditViewModel(Employee item = null)
        {
            Title = item?.FirstName;
            if(item != null)
            {
                Employee = item;
            }

        }

        #region Methods

        #endregion
    }
}
