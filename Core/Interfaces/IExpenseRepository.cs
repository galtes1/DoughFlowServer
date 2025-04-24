using AccountManagementServer.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace AccountManagementServer.Core.Interfaces
{
    public interface IExpenseRepository
    {
        public Task<List<Expense>?> CreateExpensesAsync(List<Expense> expenses);
        public Task<List<ExpenseCategory>> GetAllExpenseCategoriesAsync();

        public Task<Expense> CreateExpenseAsync(Expense expense);

        public Task<IEnumerable<Expense>> GetExpensesByUserAndMonthAsync(int userId, int monthId);
        public Task UpdateExpenseAsync(Expense expense);
        public Task DeleteExpenseAsync(int expenseId);
    }
}
