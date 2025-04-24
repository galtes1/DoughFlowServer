using System.ComponentModel.DataAnnotations;

namespace AccountManagementServer.Core.Models
{
    public class LoginModel
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }

    }
}
