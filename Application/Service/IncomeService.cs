using AccountManagementServer.Application.Interface;
using AccountManagementServer.Core.Interfaces;
using AccountManagementServer.Core.Models;

namespace AccountManagementServer.Application.Service
{
    public class IncomeService : IIncomeService
    {
        private readonly IIncomeRepository _incomeRepository;
        private readonly IMonthService _monthService;

        public IncomeService(IIncomeRepository incomeRepository, IMonthService monthService)
        {
            _incomeRepository = incomeRepository;
            _monthService = monthService;
        }

        public async Task<Income> CreateIncomeAndAttachToMonthAsync(Income income)
        {
            if (income.MonthId != 0)
            {
                await _incomeRepository.CreateIncomeAsync(income);
                return income;
            }

            DateTime monthDate = new DateTime(income.Date.Year, income.Date.Month, 1);
            var existingMonth = await _monthService.GetMonthByDateAndUserAsync(income.UserId, monthDate);

            if (existingMonth == null)
            {
                var newMonth = new Month
                {
                    Date = monthDate,
                    UserId = income.UserId
                };

                newMonth.Incomes.Add(income);
                await _monthService.CreateMonthAsync(newMonth);
            }
            else
            {
                existingMonth.Incomes.Add(income);
                await _monthService.UpdateMonthAsync(existingMonth);
            }

            return income;
        }



        public async Task<List<Income>> CreateIncomeAndAttachToMonthAsync(List<Income> incomes)
        {
            List<Income> createdIncomes = new List<Income>();
            foreach (var income in incomes)
            {
                var createdIncome = await CreateIncomeAndAttachToMonthAsync(income);
                createdIncomes.Add(createdIncome);
            }
            return createdIncomes;
        }

        public async Task<List<IncomeCategory>> GetAllIncomeCategoriesAsync()
        {
            return await _incomeRepository.GetAllIncomeCategoriesAsync();
        }

        public async Task<IEnumerable<Income>> GetIncomesAsync(int userId, int monthId)
        {
            return await _incomeRepository.GetIncomesByUserAndMonthAsync(userId, monthId);
        }

        public async Task UpdateIncomeAsync(Income income)
        {
            await _incomeRepository.UpdateIncomeAsync(income);
        }

        public async Task DeleteIncomeAsync(int incomeId)
        {
            await _incomeRepository.DeleteIncomeAsync(incomeId);
        }
    }
}
