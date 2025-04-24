using System.ComponentModel.DataAnnotations;

namespace AccountManagementServer.Core.Models
{
    public class UpdateUserRequest
    {
        [Required]
        public string Name { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        public string? Password { get; set; }
        public string? CurrentPassword { get; set; }
        [Required]
        public bool IsBusiness { get; set; }
    }
}
