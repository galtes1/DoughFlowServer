using AccountManagementServer.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace AccountManagementServer.Application.Interface
{
    public interface IExpenseService
    {
       public Task<Expense> CreateExpenseAndAttachToMonthAsync(Expense expense);
       public Task<List<Expense>> CreateExpensesAndAttachToMonthAsync(List<Expense> expenses);

       public Task<List<ExpenseCategory>> GetAllExpenseCategoriesAsync();

       public Task<IEnumerable<Expense>> GetExpensesAsync(int userId, int monthId);
       public Task UpdateExpenseAsync(Expense expense);
       public Task DeleteExpenseAsync(int expenseId);
    }
}
