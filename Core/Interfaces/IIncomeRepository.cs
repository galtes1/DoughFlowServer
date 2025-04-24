using AccountManagementServer.Core.Models;

namespace AccountManagementServer.Core.Interfaces
{
    public interface IIncomeRepository
    {

        public Task<List<Income>?> CreateIncomesAsync(List<Income> incomes);
        public Task<List<IncomeCategory>> GetAllIncomeCategoriesAsync();

        public Task<Income> CreateIncomeAsync(Income income);
        public Task<IEnumerable<Income>> GetIncomesByUserAndMonthAsync(int userId, int monthId);

        public Task UpdateIncomeAsync(Income income);
        public Task DeleteIncomeAsync(int incomeId);
    }
}
