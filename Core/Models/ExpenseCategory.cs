

using System.ComponentModel.DataAnnotations;

namespace AccountManagementServer.Core.Models
{
    public class ExpenseCategory
    {
        [Key]
        public int ExpenseCategoryId { get; set; }
        public string Name { get; set; }
    }
}
