using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdvancedApp.Models
{
    public class Employee
    {
        private decimal databaseSalary;
        public long Id { get; set; }
        public string SSN { get; set; }
        public string FirstName { get; set; }
        public string FamilyName { get; set; }
        //public decimal Salary
        //{
        //    get => databaseSalary * 2;
        //    set => databaseSalary = Math.Max(0, value);
        //}
        public decimal Salary
        {
            get => databaseSalary;
            //set
            //{
            //    if (value % 2 == 0)
            //    {
            //        databaseSalary = value;
            //    }
            //}
            set => databaseSalary = value;
        }
        public SecondaryIdentity OtherIdentity { get; set; }
        public bool SoftDeleted { get; set; } = false;
        public DateTime LastUpdated { get; set; }
        public byte[] RowVersion { get; set; }
        public string GeneratedValue { get; set; }
    }
}
