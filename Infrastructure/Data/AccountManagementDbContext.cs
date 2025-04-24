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
             base.OnModelCreating(modelBuilder);

             // שמירת enum של קטגוריות כהמרה למחרוזת בבסיס הנתונים
             modelBuilder.Entity<Expense>()
                 .Property(e => e.Category)
                 .HasConversion<string>();


            base.OnModelCreating(modelBuilder);
        }

    }
}
