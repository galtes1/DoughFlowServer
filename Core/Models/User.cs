using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using System.Xml.Linq;

namespace AccountManagementServer.Core.Models
{
    public class User
    {
        [JsonPropertyName("_id")]
        public int Id { get; set; } 
        public string Name { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        [JsonIgnore]
        public bool IsAdmin { get; set; } = false;
        public List<MonthlyFormEntry> Entries { get; set; } = new List<MonthlyFormEntry>();
    }
}
