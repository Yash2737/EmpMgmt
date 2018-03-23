using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helper
{
    public class Edit
    {
        public int Id { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string EmailPersonal { get; set; }

        [Required]
        public DateTime? DateofBirth { get; set; }

        [Required]
        public DateTime? DateofJoining { get; set; }

        [Required]
        public string Gender { get; set; }

        [Required]
        public int Status { get; set; }

        [Required]
        public int RoleId { get; set; }

    }
}
