using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace AccountManagementServer.Core.Models
{

    public class Expense
    {
        [Key]
        public int ExpenseId { get; set; }

        [Required]
        public decimal Amount { get; set; }

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
