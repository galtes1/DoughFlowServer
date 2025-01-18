using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using AccountManagementServer.Core.Models;
using AccountManagementServer.Core.Models.AccountManagementServer.Core.Models;
using AccountManagementServer.API.Controllers;

namespace AccountManagementServer.Infrastructure.Data
{
    public class AccountManagementDbContext : IdentityDbContext<IdentityUser>
    {
        public AccountManagementDbContext(DbContextOptions<AccountManagementDbContext> options) : base(options) { }
        public DbSet<Category> Categories { get; set; }
        public DbSet<MonthlyForm> MonthlyForms { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<MonthlyFormEntry> MonthlyFormEntries { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // קטגוריות ברירת מחדל
            modelBuilder.Entity<Category>().HasData(
        new Category { Id = 1, Name = "JunkFood", Type = "Expense" },
        new Category { Id = 2, Name = "Car", Type = "Expense" },
        new Category { Id = 3, Name = "Insurances", Type = "Expense" },
        new Category { Id = 4, Name = "HomeShopping", Type = "Expense" },
        new Category { Id = 5, Name = "InternetShopping", Type = "Expense" },
        new Category { Id = 6, Name = "Rent", Type = "Expense" },
        new Category { Id = 7, Name = "ApartmentBills", Type = "Expense" },
        new Category { Id = 8, Name = "Savings", Type = "Expense" },
        new Category { Id = 9, Name = "Kindergarten", Type = "Expense" },
        new Category { Id = 10, Name = "ClothesAndShoes", Type = "Expense" },
        new Category { Id = 11, Name = "VariousExternalServices", Type = "Expense" },
        new Category { Id = 12, Name = "Health", Type = "Expense" },
        new Category { Id = 13, Name = "Fun", Type = "Expense" },
        new Category { Id = 14, Name = "Cosmetics", Type = "Expense" },
        new Category { Id = 15, Name = "MonthlySalary", Type = "Revenue" },
        new Category { Id = 16, Name = "RentalFees", Type = "Revenue" },
        new Category { Id = 17, Name = "SideSalary", Type = "Revenue" },
        new Category { Id = 18, Name = "Allowance", Type = "Revenue" }
 );


            base.OnModelCreating(modelBuilder);
        }

        public static implicit operator AccountManagementDbContext(CategoriesController v)
        {
            throw new NotImplementedException();
        }
    }
}
