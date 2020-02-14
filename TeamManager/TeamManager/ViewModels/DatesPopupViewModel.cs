using System;
using System.Windows.Input;

using Xamarin.Forms;

namespace TeamManager.ViewModels
{
    public class DatesPopupViewModel : BaseViewModel
    {
        #region Properties
        public int IDEmployee { get; set; }

        private DateTime _StartDate;
        public DateTime StartDate
        {
            get
            {
                return this._StartDate;
            }
            set
            {
                this._StartDate = value;
                OnPropertyChanged("StartDate");
            }
        }
        private DateTime _EndDate;
        public DateTime EndDate
        {
            get
            {
                return this._EndDate;
            }
            set
            {
                this._EndDate = value;
                OnPropertyChanged("EndDate");
            }
        }
        #endregion
        public DatesPopupViewModel(int IDEmployee)
        {
            this.IDEmployee = IDEmployee;
        }
    }
}