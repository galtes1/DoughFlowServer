using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;


namespace AccountManagementServer.Core.Models
{
    public class User
    {

        [Key]
        public int UserId { get; set; }


        [Required]
        public string Name { get; set; }


        [Required]
        [EmailAddress]
        public string Email { get; set; }
        

        [Required]
        public string Password { get; set; }


        [JsonIgnore]
        public DateTime CreatedAt { get; set; } = DateTime.Now;


        [Required]
        public bool IsBusiness { get; set; }


        [JsonIgnore]
        public ICollection<Month> Months { get; set; } = new List<Month>();

        [NotMapped]
        [JsonIgnore]
        public string? CurrentPassword { get; set; }


    }
}
