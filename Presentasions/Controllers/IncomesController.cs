using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using AccountManagementServer.Application.Interface;
using AccountManagementServer.Core.Models;


namespace AccountManagementServer.Presentasions.Controllers
{
    [Route("api/income")]
    [ApiController]
    public class IncomesController : ControllerBase
    {
        private readonly IIncomeService _incomeService;

        public IncomesController(IIncomeService incomeService)
        {
            _incomeService = incomeService;
        }

        [HttpPost("Create")]
        public async Task<IActionResult> CreateIncomes([FromBody] List<Income> incomes)
        {
            if (incomes == null || !incomes.Any())
            {
                return BadRequest("Incomes list cannot be empty.");
            }

            try
            {
                // שליפת ה-UserId מתוך ה-Claims
                Claim? userIdClaim = HttpContext.User.Claims.FirstOrDefault(e => e.Type == "UserId");

                if (userIdClaim == null)
                {
                    return Unauthorized("User ID is missing.");
                }

                int userId = Convert.ToInt32(userIdClaim.Value);

                foreach (Income income in incomes)
                {
                    income.UserId = userId; // עדכון ה-UserId בכל הכנסה
                }

                var createdIncomes = await _incomeService.CreateIncomeAndAttachToMonthAsync(incomes);
                return Ok(createdIncomes);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpGet("GetAllCategories")]
        public async Task<IActionResult> GetAllCategories()
        {
            List<IncomeCategory> categories = await _incomeService.GetAllIncomeCategoriesAsync();
            return Ok(categories);
        }

        [HttpGet]
        public async Task<IActionResult> GetIncomes([FromQuery] int userId, [FromQuery] int monthId)
        {
            var incomes = await _incomeService.GetIncomesAsync(userId, monthId);           
            return Ok(incomes ?? new List<Income>());
        }

        [HttpPut("{incomeId}")]
        public async Task<IActionResult> UpdateIncome(int incomeId, [FromBody] Income income)
        {
            if (incomeId != income.IncomeId)
                return BadRequest("Income ID mismatch");

            await _incomeService.UpdateIncomeAsync(income);
            return NoContent();
        }

        [HttpDelete("{incomeId}")]
        public async Task<IActionResult> DeleteIncome(int incomeId)
        {
            await _incomeService.DeleteIncomeAsync(incomeId);
            return NoContent();
        }

    }
}
