using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace AccountManagementServer.Core.Models
{
    public class IncomeCategory
    {
        [Key]
        public int IncomeCategoryId { get; set; }
        public string Name { get; set; }
    }
}
