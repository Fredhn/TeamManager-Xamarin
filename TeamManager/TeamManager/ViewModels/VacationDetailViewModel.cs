using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using TeamManager.Controls;
using TeamManager.Models;
using Xamarin.Forms;

namespace TeamManager.ViewModels
{
    public class VacationDetailViewModel : BaseViewModel
    {
        #region Properties
        public Command LoadItemsCommand { get; set; }
        public ICommand TapCommand_DeletePeriod { get; set; }

        private int _TDays;
        public int TDays
        {
            get
            {
                return this._TDays;
            }
            set
            {
                this._TDays = value;
                OnPropertyChanged("TDays");

                if (this.TDays >= 30)
                {
                    this.Spent = true;
                }
                else
                {
                    this.Spent = false;
                }
            }
        }

        private SpentVacations _Vacation;
        public SpentVacations Vacation
        { 
            get 
            {
                return this._Vacation;
            } 
            set 
            {
                this._Vacation = value;
                OnPropertyChanged("Vacation");
            } 
        }

        private ObservableCollection<SpentVacations> _Vacations = new ObservableCollection<SpentVacations>();
        public ObservableCollection<SpentVacations> Vacations
        {
            get
            {
                return this._Vacations;
            }
            set
            {
                this._Vacations = value;
                OnPropertyChanged("Vacations");
                OnPropertyChanged("Spent");
            }
        }

        private bool _Spent;
        public bool Spent
        {
            get
            {
                return this._Spent;
            }
            set
            {
                this._Spent = value;
                OnPropertyChanged("Spent");
                OnPropertyChanged("ButtonSpentEnabled");
            }
        }

        public bool ButtonSpentEnabled
        {
            get
            {
                return !this.Spent;
            }
        }
        #endregion

        public VacationDetailViewModel(SpentVacations item = null)
        {
            Title = "Vacations Details";
            this.Vacations = new ObservableCollection<SpentVacations>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());
            TapCommand_DeletePeriod = new Command(DeleteVacationPeriodCommand);

            if (item != null)
            {
                this.Vacation = new SpentVacations();
                this.Vacation = item;
            }
            else
            {
                Vacation = new SpentVacations();
            }
        }

        async void DeleteVacationPeriodCommand(object VacPeriodData)
        {
            var periodToDelete = VacPeriodData as SpentVacations;
            if (periodToDelete == null)
                return;

            var confirmationPopup = new ConfirmationPopup("Delete this vacation period (" + periodToDelete.TotalDays.ToString() + " days) from " + periodToDelete.RefYear.ToString() + "?");
            await PopupNavigation.Instance.PushAsync(confirmationPopup);
            var result = await confirmationPopup.GetResult();
            

            if(result.Equals("true"))
            {
                this.Vacations.Remove(periodToDelete);
                await App.Database._spentVacations.DeleteSpentVacationsAsync(periodToDelete);
                await this.ExecuteLoadItemsCommand();
            }
        }

        #region Methods
        public async Task ExecuteLoadItemsCommand()
        {
            //if (IsBusy)
            //    return;

            IsBusy = true;

            try
            {
                //Vacations.Clear();
                //this.Vacation.TotalDays = 0;
                //var items = await App.Database._spentVacations.GetSpentVacationsPeriodsAsync(Vacation.IDEmployee, Vacation.RefYear);

                //foreach (var item in items)
                //{
                //    Vacations.Add(item);
                //    this.TDays += item.TotalDays;
                //}

                //this.Vacation.TotalDays = this.TDays;
                this.UpdateActivity();
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
                //IsBusy = false;
            }
        }

        public async void UpdateActivity()
        {
            this.Vacations.Clear();
            this.Vacation.TotalDays = 0;
            this.TDays = 0;
            var items = await App.Database._spentVacations.GetSpentVacationsPeriodsAsync(Vacation.IDEmployee, Vacation.RefYear);

            foreach (var item in items)
            {
                Vacations.Add(item);
                this.TDays += item.TotalDays;
            }

            this.Vacation.TotalDays = this.TDays;
        }
        #endregion
    }
}
