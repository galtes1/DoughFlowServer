using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace AccountManagementServer.Core.Models
{

    public class Income
    {
        [Key]
        public int IncomeId { get; set; }

        [Required]
        public decimal Amount { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        [ForeignKey("IncomeCategory")]
        public int IncomeCategoryId { get; set; }

        [Required]
        public string IncomeName { get; set; }

        [Required]
        [ForeignKey("User")]
        public int UserId { get; set; }

        [Required]
        [ForeignKey("Month")]
        public int MonthId { get; set; }

        public string? Description { get; set; }


    }
}
