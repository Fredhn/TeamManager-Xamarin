using SQLite;
using System;
using System.Collections.Generic;

namespace TeamManager.Models
{
    public class ExpiredVacations
    {
        public int IDEmployee { get; set; }
        public string EmployeeName { get; set; }
        public List<DateTime> ExpiringDates { get; set; }

        public ExpiredVacations()
        {
            this.ExpiringDates = new List<DateTime>();
        }

    }
}