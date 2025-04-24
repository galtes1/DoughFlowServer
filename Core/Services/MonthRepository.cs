using AccountManagementServer.Core.Interfaces;
using AccountManagementServer.Core.Models;
using AccountManagementServer.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace AccountManagementServer.Core.Services
{
    public class MonthRepository : IMonthRepository
    {
        private readonly AccountManagementDbContext _context;

        public MonthRepository(AccountManagementDbContext context)
        {
            _context = context;
        }

        public async Task<Month?> GetPieDataByMonthAsync(int userId, int monthId)
        {
            return await _context.Months
                .Where(m => m.UserId == userId && m.MonthId == monthId) 
                .Include(m => m.Expenses)
                .Include(m => m.Incomes)
                .AsSplitQuery()
                .FirstOrDefaultAsync();
        }
        public async Task<Month?> GetMonthByDateAndUserAsync(int userId, DateTime monthDate)
        {
            return await _context.Months
                .Include(m => m.Expenses)
                .Include(m => m.Incomes)
                .FirstOrDefaultAsync(m => m.UserId == userId && m.Date == monthDate);                
        }
       

        public async Task<Month?> GetMonthByIdAsync(int monthId)
        {
            return await _context.Months
                .Include(m => m.Expenses)
                .Include(m => m.Incomes)
                .FirstOrDefaultAsync(m => m.MonthId == monthId);
        }

        public async Task<List<Month>> GetAllMonthsAsync()
        {
            return await _context.Months
                .Include(m => m.Expenses)
                .Include(m => m.Incomes)
                .ToListAsync();
        }

        public async Task<Month?> UpdateMonthAsync(Month month)
        {
            var existingMonth = await _context.Months.FindAsync(month.MonthId);
            if (existingMonth == null)
            {
                return null;
            }
            existingMonth.Date = month.Date;
            existingMonth.UserId = month.UserId;
            await _context.SaveChangesAsync();
            return existingMonth;
        }

        public async Task<bool> DeleteMonthAsync(int monthId)
        {
            var existingMonth = await _context.Months.FindAsync(monthId);
            if (existingMonth == null)
            {
                return false;
            }
            _context.Months.Remove(existingMonth);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Month> CreateMonthAsync(Month month)
        {
            var userExists = await _context.Users.AnyAsync(u => u.UserId == month.UserId);

            if (!userExists)
            {
                throw new Exception($"Cannot create Month. User with ID {month.UserId} does not exist.");
            }
            _context.Months.Add(month);
            await _context.SaveChangesAsync();
            return month;
        }
        // FOR GPT
       
        public async Task<List<Month>> GetMonthsByUserAsync(int userId)
        {
            return await _context.Months
                .Where(m => m.UserId == userId)
                .OrderBy(m => m.Date)
                .ToListAsync();
        }


    }
}
