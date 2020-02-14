using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using TeamManager.Models;
using Xamarin.Forms;

namespace TeamManager.Services
{
    public static class Service_Calendar
    {
        private static List<ExpiredVacations> AllExpiredVacations;
        public async static Task<List<ExpiredVacations>> GetAllExpiredVacations()
        {
            AllExpiredVacations = new List<ExpiredVacations>();
            var employees = await App.Database._employee.GetEmployeesAsync();

            foreach(var e in employees)
            {
                var dates = await CalculateExpiredVacations(e);
                AllExpiredVacations.Add(new ExpiredVacations() { IDEmployee = e.ID, ExpiringDates = dates, EmployeeName = e.FirstName + " " + e.LastName });
            }

            return AllExpiredVacations;
        }
        private async static Task<List<DateTime>> CalculateExpiredVacations(Employee emp)
        {
            List<DateTime> expiredVacations = new List<DateTime>();
            TimeSpan vacQty = new TimeSpan();
            if (emp.AcquisitivePeriod < emp.AdmissionDate)
            {
                vacQty = DateTime.Today - emp.AdmissionDate;
                emp.AcquisitivePeriod = emp.AdmissionDate;
            }
            else
            {
                vacQty = DateTime.Today - emp.AcquisitivePeriod;
            }
            DateTime zeroTime = new DateTime(1, 1, 1);
            int years = (zeroTime + vacQty).Year - 1;

            for (int i = 1; i <= years; i++)
            {
                var dt = await GetVacationDetails(i, emp);
                if (dt != DateTime.MinValue)
                    expiredVacations.Add(dt);
            }

            return expiredVacations;
        }

        private async static Task<DateTime> GetVacationDetails(int i, Employee emp)
        {
            DateTime dt = new DateTime();
            int days = 0;
            var items = await App.Database._spentVacations.GetSpentVacationsPeriodsAsync(emp.ID, (emp.AcquisitivePeriod.Year + i));
            foreach (var item in items)
            {
                days += item.TotalDays;
            }
            if (days < 30)
            {
                dt = new DateTime((emp.AcquisitivePeriod.Year + i), emp.AcquisitivePeriod.Month, emp.AcquisitivePeriod.Day);
            }
            return dt;
        }
    }
}