using AccountManagementServer.Core.Models;

namespace AccountManagementServer.Application.Interface
{
    public interface IIncomeService
    {


        public Task<Income> CreateIncomeAndAttachToMonthAsync(Income income);
        public Task<List<Income>> CreateIncomeAndAttachToMonthAsync(List<Income> income);

        public Task<List<IncomeCategory>> GetAllIncomeCategoriesAsync();

        public Task<IEnumerable<Income>> GetIncomesAsync(int userId, int monthId);
        public Task UpdateIncomeAsync(Income income);
        public Task DeleteIncomeAsync(int incomeId);
    }
}
