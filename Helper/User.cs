using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Helper
{
    [Table("tbluser")]
    public class User
    {
        public int Id { get; set; }
        [Required]
        public string Username { get; set; }

        public int RoleId { get; set; }

        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }

        public string FullName => FirstName + " " + LastName;

        [Required]
        public string Gender { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MM-yyyy}")]
        public DateTime? DateofBirth { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? DateofJoining { get; set; }

        [Required]
        public string Role { get; set; }

        public string UniqueEmpCode { get; set; }

        //[Required(ErrorMessage = "Email Id Mandatory")]

        [CustomUsernameValidation]
        public string EmailOfficial { get; set; }

        [Required]
        public string EmailPersonal { get; set; }

        [Required(ErrorMessage = "Password Mandatory")]
        public string Password { get; set; }

        public string NewPass { get; set; }

        public string PhoneNoOfficial { get; set; }

        public string PhoneNoPersonal { get; set; }

        public string PanNumber { get; set; }

        public string PanDetailsId { get; set; }


        public string AadharNumber { get; set; }

        public string AadharDetailsId { get; set; }

        public string Qualification { get; set; }


        [Required]
        public int Status { get; set; }

        public bool? IsPasswordReset { get; set; }

        public DateTime? TimeStamp { get; set; }
    }

    public class CustomUsernameValidation : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                string email = value.ToString();
                if (value.ToString().Length < 5)
                {
                    return new ValidationResult("Username cannot be less than 5");
                }
                else
                {
                    return ValidationResult.Success;
                }
                //if (Regex.IsMatch(email, @"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", RegexOptions.IgnoreCase))
                //{
                //    return ValidationResult.Success;
                //}
                //else
                //{
                //    return new ValidationResult("Please Enter a Valid Email.");
                //}
            }
            else
            {
                return new ValidationResult("Username cannot be empty");
            }
        }
    }
}
