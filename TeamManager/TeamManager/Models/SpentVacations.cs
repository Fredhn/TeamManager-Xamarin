using SQLite;
using System;
using Xamarin.Forms;

namespace TeamManager.Models
{
    public class SpentVacations
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        [Indexed]
        public int IDEmployee { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }
        public int TotalDays { get; set; }
        public int RefYear { get; set; }
        public bool Spent 
        { 
            get
            {
                return (TotalDays >= 30 ? true : false);
            }
        }

        public Color StatusColor
        {
            get
            {
                return (Spent ? Color.FromHex("#eae2b7") : Color.IndianRed);
            }
        }
    }
}