using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;


namespace AccountManagementServer.Core.Models
{

    public class Expense
    {
        [Key]
        public int ExpenseId { get; set; }

        [Required]
        public double Amount { get; set; }

        [Required]
        public DateTime Date { get; set; } 

        [Required]
        [ForeignKey("ExpenseCategory")]
        public int ExpenseCategoryId { get; set; }

        [Required]
        public string expenseName { get; set; }

        [Required]
        [ForeignKey("User")]
        public int UserId { get; set; }

        [Required]
        [ForeignKey("Month")]
        public int MonthId { get; set; }

        public string? Description { get; set; }

    }
}
