using System.Text;
using System.Text.Json;
using AccountManagementServer.Application.Interface;
using Microsoft.AspNetCore.Authorization;


namespace AccountManagementServer.Application.Service
{
    public class GptService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;
        private readonly IExpenseService _expenseService;
        private readonly IIncomeService _incomeService;
        private readonly IMonthService _monthService;

        public GptService(HttpClient httpClient, IConfiguration configuration, IExpenseService expenseService, IIncomeService incomeService, IMonthService monthService)
        {
            _httpClient = httpClient;
            _apiKey = configuration["myChatGPT:ApiKey"] ?? throw new ArgumentNullException("Missing OpenAI API Key in configuration");
            _expenseService = expenseService;
            _incomeService = incomeService;
            _monthService = monthService;
        }
        public async Task<string> AnalyzeUserBudgetAsync(int userId)
        {
            var now = DateTime.Now;
            var prevMonth = now.AddMonths(-1);
            var currentMonthId = await _monthService.GetMonthIdByDateAndUserAsync(userId, now.Month, now.Year);
            var prevMonthId = currentMonthId.HasValue
                ? await _monthService.GetPreviousMonthIdAsync(userId, currentMonthId.Value)
                : null;
          

            if (currentMonthId == null && prevMonthId == null)
                return "לא נמצאו נתונים עבור המשתמש בחודשים הנוכחי או הקודם.";

            var currentExpenses = currentMonthId.HasValue
                ? await _expenseService.GetExpensesAsync(userId, currentMonthId.Value)
                : new List<Core.Models.Expense>();

            var currentIncomes = currentMonthId.HasValue
                ? await _incomeService.GetIncomesAsync(userId, currentMonthId.Value)
                : new List<Core.Models.Income>();

            var prevExpenses = prevMonthId.HasValue
                ? await _expenseService.GetExpensesAsync(userId, prevMonthId.Value)
                : new List<Core.Models.Expense>();

            var prevIncomes = prevMonthId.HasValue
                ? await _incomeService.GetIncomesAsync(userId, prevMonthId.Value)
                : new List<Core.Models.Income>();

            var jsonInput = JsonSerializer.Serialize(new
            {
                question = "על סמך הנתונים של החודש הקודם והחודש הנוכחי, מתי היו לי יותר הוצאות ועל מה. תן לי טיפ לחיסכון תענה בתשובה קצרה ומתומצתת",
                currentMonthExpenses = currentExpenses,
                previousMonthExpenses = prevExpenses,
                currentMonthIncomes = currentIncomes,
                previousMonthIncomes = prevIncomes
            });

            var requestBody = new
            {
                model = "gpt-3.5-turbo",
                messages = new[]
                {
                    new { role = "system", content = "על סמך הנתונים של החודש הקודם והחודש הנוכחי, מתי היו לי יותר הוצאות ועל מה. תן לי טיפ לחיסכון תענה בתשובה קצרה ומתומצתת" },
                    new { role = "user", content = jsonInput }
                }
            };

            var requestContent = new StringContent(JsonSerializer.Serialize(requestBody), Encoding.UTF8, "application/json");
            var request = new HttpRequestMessage(HttpMethod.Post, "https://api.openai.com/v1/chat/completions");
            request.Headers.Add("Authorization", $"Bearer {_apiKey}");
            request.Content = requestContent;

            var response = await _httpClient.SendAsync(request);
            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                throw new ApplicationException($"OpenAI Error: {error}");
            }

            var responseContent = await response.Content.ReadAsStringAsync();
            using var jsonDoc = JsonDocument.Parse(responseContent);
            var result = jsonDoc.RootElement
                                 .GetProperty("choices")[0]
                                 .GetProperty("message")
                                 .GetProperty("content")
                                 .GetString();

            return result?.Trim();
        }
    }
}
