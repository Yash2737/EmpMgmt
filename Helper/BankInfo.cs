using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeManagement.Models
{
    [Table("tblbankinfo")]
    public class BankInfo
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        [Required]
        public string BankName { get; set; }
        [Required]
        public string IfscCode { get; set; }
        [Required]
        public string AccountNo { get; set; }
        [Required]
        public string AccountType { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string State { get; set; }
        [Required]
        public string PinCode { get; set; }

        public string EmailId { get; set; }
        [Required]
        public string UPICode { get; set; }

        public int Status { get; set; }
        public DateTime TimeStamp { get; set; }

    }
}