using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helper
{
    public class Password
    {
        public int Id { get; set; }
        [Required]
        public string OldPass { get; set; }
        [Required]
        public string NewPass { get; set; }
    }
}
