using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using TeamManager.Services;
using Xamarin.Forms;

namespace TeamManager.ViewModels
{
    public class CalendarViewModel : BaseViewModel
    {

        #region Properties
        public Command LoadVacationsCalendarCommand { get; set; }
        #endregion

        public CalendarViewModel()
        {
            Title = "Calendar";

            LoadVacationsCalendarCommand = new Command(async () => await ExecuteLoadVacationsCalendar());
        }

        async Task ExecuteLoadVacationsCalendar()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                var lista = await Service_Calendar.GetAllExpiredVacations();
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
    }
}