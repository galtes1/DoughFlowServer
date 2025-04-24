using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AccountManagementServer.Core.Models
{
    public class Month
    {
        [Key]
        public int MonthId { get; set; }

        public DateTime Date { get; set; } 

        [Required]
        [ForeignKey("User")]
        public int UserId { get; set; }

        public ICollection<Expense> Expenses { get; set; } = new List<Expense>(); 

        public ICollection<Income> Incomes { get; set; } = new List<Income>();

    }
}
