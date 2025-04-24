using AccountManagementServer.Core.Interfaces;
using AccountManagementServer.Core.Models;
using AccountManagementServer.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace AccountManagementServer.Core.Services
{
    public class ExpenseRepository : IExpenseRepository
    {
        private readonly AccountManagementDbContext _context;
        public ExpenseRepository(AccountManagementDbContext context)
        {
            _context = context;
        }
        public async Task<List<Expense>?> CreateExpensesAsync(List<Expense> expenses)
        {
            try
            {
                await _context.Expenses.AddRangeAsync(expenses);
                await _context.SaveChangesAsync();
                return expenses;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error while creating Expenses: {e.Message}");
                return null;
            }
        }


        public async Task<List<ExpenseCategory>> GetAllExpenseCategoriesAsync()
        {
            try
            {
                return await _context.ExpenseCategories.ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving expense categories: {ex.Message}");
                return new List<ExpenseCategory>(); 
            }
        }

        public async Task<Expense> CreateExpenseAsync(Expense expense)
        {
            _context.Expenses.Add(expense);
            await _context.SaveChangesAsync();
            return expense;
        }

        public async Task<IEnumerable<Expense>> GetExpensesByUserAndMonthAsync(int userId, int monthId)
        {
            return await _context.Expenses
                .Where(i => i.UserId == userId && i.MonthId == monthId)
                .ToListAsync();
        }

        public async Task UpdateExpenseAsync(Expense expense)
        {
            _context.Expenses.Update(expense);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteExpenseAsync(int expenseId)
        {
            var expense = await _context.Expenses.FindAsync(expenseId);
            if (expense != null)
            {
                _context.Expenses.Remove(expense);
                await _context.SaveChangesAsync();
            }
        }
    }
}
