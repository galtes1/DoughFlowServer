using AccountManagementServer.Core.Models;

namespace AccountManagementServer.Application.Interface
{
    public interface IMonthService
    {
        public Task<Month> CreateMonthAsync(Month month);
        public Task<Month?> GetMonthByIdAsync(int monthId);
        public Task<List<Month>> GetAllMonthsAsync();
        public Task<Month?> UpdateMonthAsync(Month month);
        public Task<bool> DeleteMonthAsync(int monthId);
        public Task<Month?> GetMonthByDateAndUserAsync(int userId, DateTime monthDate);
        public Task<Month?> GetPieDataByMonthAsync(int userId, int monthId);

        public Task<int?> GetMonthIdByDateAndUserAsync(int userId, int month, int year);
        public Task<int?> GetPreviousMonthIdAsync(int userId, int currentMonthId); // FOR GPT
        public  Task<List<Month>> GetMonthsByUserAsync(int userId);// FOR GPT
    }
}
