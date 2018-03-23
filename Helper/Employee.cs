using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Helper
{
    [Table("tblemployee")]
    public class Employee
    {
        public int EmployeeId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Qualification { get; set; }
        public DateTime? DateofBirth { get; set; }
        public string PanNumber { get; set; }
        public string AadharNo { get; set; }
        public string Gender { get; set; }
        public string City { get; set; }

    }
}