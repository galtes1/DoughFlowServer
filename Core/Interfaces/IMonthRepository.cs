using AccountManagementServer.Core.Models;

namespace AccountManagementServer.Core.Interfaces
{
    public interface IMonthRepository
    {
        public Task<Month> CreateMonthAsync(Month month);
        public Task<Month?> GetMonthByIdAsync(int monthId);
        public Task<List<Month>> GetAllMonthsAsync();
        public Task<Month?> UpdateMonthAsync(Month month);
        public Task<bool> DeleteMonthAsync(int monthId);
        public Task<Month?> GetMonthByDateAndUserAsync(int userId, DateTime monthDate);
        public Task<Month?> GetPieDataByMonthAsync(int userId, int monthId);
        // FOR GPT
        public Task<List<Month>> GetMonthsByUserAsync(int userId);

        public Task<List<MonthTotalsDto>> GetYearSummaryAsync(int userId, int year);

        public Task<Month?> GetMonthWithDetailsAsync(int monthId, int userId);
    }
}
