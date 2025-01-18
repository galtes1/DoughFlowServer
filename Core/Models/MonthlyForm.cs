using System.Text.Json.Serialization;

namespace AccountManagementServer.Core.Models
{
    public class MonthlyForm
    {
        [JsonPropertyName("_id")]
        public int Id { get; set; } 
        public int UserId { get; set; } // מזהה המשתמש
        public DateTime MonthYear { get; set; } // חודש ושנה
        public bool IsLocked { get; set; } = false; // האם הטופס נעול לעריכה
        [JsonIgnore]
        public List<MonthlyFormEntry> Entries { get; set; } = new List<MonthlyFormEntry>(); // רשומות הטופס
    }
}
