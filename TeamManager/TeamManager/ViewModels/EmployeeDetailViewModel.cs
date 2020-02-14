using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using TeamManager.Models;
using TeamManager.Services;
using Xamarin.Forms;

namespace TeamManager.ViewModels
{
    public class EmployeeDetailViewModel : BaseViewModel
    {
        #region Properties
        public Command LoadItemsCommand { get; set; }

        private ObservableCollection<SpentVacations> _SpentVacations;
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

        public ObservableCollection<SpentVacations> SpentVacations
        {
            get
            {
                return this._SpentVacations;
            }
            set
            {
                this._SpentVacations = value;
                OnPropertyChanged("SpentVacations");
            }
        }
        #endregion

        public EmployeeDetailViewModel(Employee item = null)
        {
            Title = item?.FirstName;
            if(item != null)
            {
                Employee = item;
            }
            else
            {
                Employee = new Employee();
                Employee.AdmissionDate = DateTime.Today;
                Employee.AcquisitivePeriod = DateTime.Today;
            }

            this.SpentVacations = new ObservableCollection<SpentVacations>();
            LoadItemsCommand = new Command(() => ExecuteLoadItemsCommand());
        }

        #region Methods
        async void ExecuteLoadItemsCommand()
        {
            //if (IsBusy)
            //    return;
            TimeSpan vacQty = new TimeSpan();
            if (Employee.AcquisitivePeriod < Employee.AdmissionDate)
            {
                vacQty = DateTime.Today - Employee.AdmissionDate;
                Employee.AcquisitivePeriod = Employee.AdmissionDate;
            }
            else
            {
                vacQty = DateTime.Today - Employee.AcquisitivePeriod;
            } 
            DateTime zeroTime = new DateTime(1, 1, 1);
            int years = (zeroTime + vacQty).Year - 1;

            IsBusy = true;

            try
            {
                this.SpentVacations.Clear();

                for(int i = 1; i <= years; i++)
                {
                    GetVacationDetails(i);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    IsBusy = false;
                });
            }
        }

        async void GetVacationDetails(int i)
        {
            int days = 0;
            var items = await App.Database._spentVacations.GetSpentVacationsPeriodsAsync(Employee.ID, (Employee.AcquisitivePeriod.Year + i));
            foreach(var item in items)
            {
                days += item.TotalDays;
            }

            this.SpentVacations.Add(new SpentVacations() { RefYear = (Employee.AcquisitivePeriod.Year + i), IDEmployee = Employee.ID, TotalDays = days });
        }
        #endregion
    }
}
