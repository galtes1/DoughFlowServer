using AccountManagementServer.Core.Models.AccountManagementServer.Core.Models;
using System.Text.Json.Serialization;

namespace AccountManagementServer.Core.Models
{
    public class MonthlyFormEntry
    {
        [JsonPropertyName("_id")]
        public int Id { get; set; }
        public int MonthlyFormId { get; set; } // מזהה טופס חודשי
        public MonthlyForm MonthlyForm { get; set; } // קשר לטופס החודשי
        public int CategoryId { get; set; } // מזהה קטגוריה
        public Category Category { get; set; } // קשר לקטגוריה
        public decimal Amount { get; set; } // הסכום שהוזן
    }
}
