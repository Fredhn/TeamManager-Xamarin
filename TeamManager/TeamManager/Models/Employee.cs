using SQLite;
using System;

namespace TeamManager.Models
{
    public class Employee
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public double Salary { get; set; }
        public DateTime AdmissionDate { get; set; }
        public DateTime AcquisitivePeriod { get; set; }

        public bool HasExpiredVacations
        {
            get
            {
                return true;
            }
        }
    }
}