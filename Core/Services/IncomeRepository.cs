using AccountManagementServer.Core.Interfaces;
using AccountManagementServer.Core.Models;
using AccountManagementServer.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace AccountManagementServer.Core.Services
{
    public class IncomeRepository : IIncomeRepository
    {
        private readonly AccountManagementDbContext _context;
        public IncomeRepository(AccountManagementDbContext context)
        {
            _context = context;
        }
        public async Task<List<Income>?> CreateIncomesAsync(List<Income> incomes)
        {
            try
            {
                await _context.Incomes.AddRangeAsync(incomes);
                await _context.SaveChangesAsync();
                return incomes;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error while creating Incomes: {e.Message}");
                return null;
            }
        }


        public async Task<List<IncomeCategory>> GetAllIncomeCategoriesAsync()
        {
            try
            {
                return await _context.IncomeCategories.ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving Incomes categories: {ex.Message}");
                return new List<IncomeCategory>();
            }
        }

        public async Task<Income> CreateIncomeAsync(Income income)
        {
            _context.Incomes.Add(income);
            await _context.SaveChangesAsync();
            return income;
        }

        public async Task<IEnumerable<Income>> GetIncomesByUserAndMonthAsync(int userId, int monthId)
        {
            return await _context.Incomes
                .Where(i => i.UserId == userId && i.MonthId == monthId)
                .ToListAsync();
        }

        public async Task UpdateIncomeAsync(Income income)
        {
            _context.Incomes.Update(income);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteIncomeAsync(int incomeId)
        {
            var income = await _context.Incomes.FindAsync(incomeId);
            if (income != null)
            {
                _context.Incomes.Remove(income);
                await _context.SaveChangesAsync();
            }
        }
    }
}
