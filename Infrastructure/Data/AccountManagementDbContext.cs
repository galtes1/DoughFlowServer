using Microsoft.EntityFrameworkCore;
using AccountManagementServer.Core.Models;

namespace AccountManagementServer.Infrastructure.Data
{
    public class AccountManagementDbContext : DbContext
    {              
        public DbSet<User> Users { get; set; }
        public DbSet<Expense> Expenses { get; set; }
        public DbSet<Income> Incomes { get; set; }
        public DbSet<Month> Months { get; set; }
        public DbSet<ExpenseCategory> ExpenseCategories { get; set; }
        public DbSet<IncomeCategory> IncomeCategories { get; set; }

        public AccountManagementDbContext(DbContextOptions<AccountManagementDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
         {
             base.OnModelCreating(modelBuilder);
         }

            

        }
    }


      
