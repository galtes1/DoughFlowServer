using AccountManagementServer.Application.Interface;
using AccountManagementServer.Core.Interfaces;
using AccountManagementServer.Core.Models;


namespace AccountManagementServer.Application.Service
{
    public class ExpenseService : IExpenseService
    {
        private readonly IExpenseRepository _expenseRepository;
        private readonly IMonthService _monthService;

        public ExpenseService(IExpenseRepository expenseRepository, IMonthService monthService)
        {
            _expenseRepository = expenseRepository;
            _monthService = monthService;
        }


        public async Task<Expense> CreateExpenseAndAttachToMonthAsync(Expense expense)
        {
            if (expense.MonthId != 0)
            {
                await _expenseRepository.CreateExpenseAsync(expense); 
                return expense;
            }

            DateTime monthDate = new DateTime(expense.Date.Year, expense.Date.Month, 1);
            var existingMonth = await _monthService.GetMonthByDateAndUserAsync(expense.UserId, monthDate);

            if (existingMonth == null)
            {
                var newMonth = new Month
                {
                    Date = monthDate,
                    UserId = expense.UserId
                };

                newMonth.Expenses.Add(expense);
                await _monthService.CreateMonthAsync(newMonth);
            }
            else
            {
                existingMonth.Expenses.Add(expense);
                await _monthService.UpdateMonthAsync(existingMonth);
            }

            return expense;
        }


        public async Task<List<Expense>> CreateExpensesAndAttachToMonthAsync(List<Expense> expenses)
        {
            List<Expense> createdExpenses = new List<Expense>();
            foreach (Expense expense in expenses)
            {
                Expense createdExpense = await CreateExpenseAndAttachToMonthAsync(expense);
                createdExpenses.Add(createdExpense);
            }
            return createdExpenses;
        }

        public async Task<List<ExpenseCategory>> GetAllExpenseCategoriesAsync()
        {
            return await _expenseRepository.GetAllExpenseCategoriesAsync();
        }

        public async Task<IEnumerable<Expense>> GetExpensesAsync(int userId, int monthId)
        {
            return await _expenseRepository.GetExpensesByUserAndMonthAsync(userId, monthId);
        }

        public async Task UpdateExpenseAsync(Expense expense)
        {
            await _expenseRepository.UpdateExpenseAsync(expense);
        }

        public async Task DeleteExpenseAsync(int expenseId)
        {
            await _expenseRepository.DeleteExpenseAsync(expenseId);
        }
    }
}
