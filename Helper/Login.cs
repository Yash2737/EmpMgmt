using System.ComponentModel.DataAnnotations;

namespace Helper
{
    public class Login
    {
        public int Id { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
