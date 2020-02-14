using System;
using System.Windows.Input;

using Xamarin.Forms;

namespace TeamManager.ViewModels
{
    public static class ViewModelCollection
    {
        private static CalendarViewModel _CalendarViewModel;

        public static CalendarViewModel Calendar

        {
            get
            {
                if (_CalendarViewModel == null)
                    _CalendarViewModel = new CalendarViewModel();

                return _CalendarViewModel;
            }
            set
            {
                _CalendarViewModel = value;

            }

        }

        private static EmployeeDetailViewModel _EmployeeDetail;

        public static EmployeeDetailViewModel EmployeeDetails

        {
            get
            {
                if (_EmployeeDetail == null)
                    _EmployeeDetail = new EmployeeDetailViewModel();

                return _EmployeeDetail;
            }
            set
            {
                _EmployeeDetail = value;

            }

        }

        private static EmployeeEditViewModel _EmployeeEdit;

        public static EmployeeEditViewModel EmployeeEdit

        {
            get
            {
                if (_EmployeeEdit == null)
                    _EmployeeEdit = new EmployeeEditViewModel();

                return _EmployeeEdit;
            }
            set
            {
                _EmployeeEdit = value;

            }

        }

        private static EmployeesViewModel _Employees;

        public static EmployeesViewModel Employees

        {
            get
            {
                if (_Employees == null)
                    _Employees = new EmployeesViewModel();

                return _Employees;
            }
            set
            {
                _Employees = value;

            }

        }

        private static VacationDetailViewModel _VacationDetail;

        public static VacationDetailViewModel VacationDetails

        {
            get
            {
                if (_VacationDetail == null)
                    _VacationDetail = new VacationDetailViewModel();

                return _VacationDetail;
            }
            set
            {
                _VacationDetail = value;

            }

        }

        private static ConfirmationPopupViewModel _ConfirmationPopup;

        public static ConfirmationPopupViewModel ConfirmationPopup

        {
            get
            {
                if (_ConfirmationPopup == null)
                    _ConfirmationPopup = new ConfirmationPopupViewModel();

                return _ConfirmationPopup;
            }
            set
            {
                _ConfirmationPopup = value;

            }

        }
    }
}