using AccountManagementServer.Application.Interface;
using AccountManagementServer.Core.Interfaces;
using AccountManagementServer.Core.Models;
using System.Text;

namespace AccountManagementServer.Application.Service
{
    public class MonthService : IMonthService
    {
        private readonly IMonthRepository _monthRepository;

        public MonthService(IMonthRepository monthRepository)
        {
            _monthRepository = monthRepository;
        }

        public async Task<Month?> GetPieDataByMonthAsync(int userId, int monthId)
        {
            try
            {
                var monthEntity = await _monthRepository.GetPieDataByMonthAsync(userId, monthId);

                if (monthEntity == null)
                {
                    var newMonth = new Month
                    {
                        UserId = userId,
                        Date = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1),
                        Expenses = new List<Expense>(),
                        Incomes = new List<Income>()
                    };

                    monthEntity = await _monthRepository.CreateMonthAsync(newMonth);
                }

                // שליפת נתוני ההוצאות וההכנסות של החודש שנמצא או נוצר
                return monthEntity;
            }
            catch
            {
                throw new ArgumentException("פורמט חודש לא תקין");
            }
        }

        public async Task<int?> GetMonthIdByDateAndUserAsync(int userId, int month, int year)
        {
            DateTime monthDate = new DateTime(year, month, 1);
            Month? monthEntity = await _monthRepository.GetMonthByDateAndUserAsync(userId, monthDate);

            return monthEntity?.MonthId;
        }

        public async Task<Month> CreateMonthAsync(Month month)
        {
            return await _monthRepository.CreateMonthAsync(month);
        }

        public async Task<Month?> GetMonthByIdAsync(int monthId)
        {
            return await _monthRepository.GetMonthByIdAsync(monthId);
        }

        public async Task<List<Month>> GetAllMonthsAsync()
        {
            return await _monthRepository.GetAllMonthsAsync();
        }

        public async Task<Month?> UpdateMonthAsync(Month month)
        {
            return await _monthRepository.UpdateMonthAsync(month);
        }

        public async Task<bool> DeleteMonthAsync(int monthId)
        {
            return await _monthRepository.DeleteMonthAsync(monthId);
        }

        public async Task<Month?> GetMonthByDateAndUserAsync(int userId, DateTime monthDate)
        {
            return await _monthRepository.GetMonthByDateAndUserAsync(userId, monthDate);
        }

        public async Task<int?> GetPreviousMonthIdAsync(int userId, int currentMonthId)
        {
            var allMonths = await _monthRepository.GetAllMonthsAsync();
            var userMonths = allMonths
                .Where(m => m.UserId == userId)
                .OrderBy(m => m.Date)
                .ToList();

            Console.WriteLine("🧾 All months for user:");
            foreach (var m in userMonths)
                Console.WriteLine($"ID: {m.MonthId}, Date: {m.Date:yyyy-MM-dd}");

            var currentIndex = userMonths.FindIndex(m => m.MonthId == currentMonthId);

            Console.WriteLine($"🔍 currentIndex: {currentIndex}");

            if (currentIndex > 0)
            {
                var prevId = userMonths[currentIndex - 1].MonthId;
                Console.WriteLine($"✅ previousMonthId: {prevId}");
                return prevId;
            }

            Console.WriteLine("❌ No previous month found.");
            return null;
        }

        public async Task<List<Month>> GetMonthsByUserAsync(int userId)
        {
            return await _monthRepository.GetMonthsByUserAsync(userId);
        }

        public Task<List<MonthTotalsDto>> GetYearSummaryAsync(int userId, int year)
        {
            return _monthRepository.GetYearSummaryAsync(userId, year);
        }

        public async Task<string> ExportMonthCsvAsync(int monthId, int userId)
        {
            var month = await _monthRepository.GetMonthWithDetailsAsync(monthId, userId);
            if (month == null) return string.Empty;

            var sb = new StringBuilder();
            sb.AppendLine("Type,Category,Amount,Date,Description");

            foreach (var income in month.Incomes)
            {
                sb.AppendLine($"Income,{income.IncomeName},{income.Amount},{income.Date:yyyy-MM-dd},{income.Description}");
            }

            foreach (var expense in month.Expenses)
            {
                sb.AppendLine($"Expense,{expense.expenseName},{expense.Amount},{expense.Date:yyyy-MM-dd},{expense.Description}");
            }

            return sb.ToString();
        }



    }
}
