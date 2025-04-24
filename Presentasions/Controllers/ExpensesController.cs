using System.Security.Claims;
using AccountManagementServer.Application.Interface;
using AccountManagementServer.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace AccountManagementServer.Presentasions.Controllers
{
    
    [Route("api/expenses")]
    [ApiController]
    public class ExpensesController : ControllerBase
    {

        private readonly IExpenseService _expenseService;

        public ExpensesController(IExpenseService expenseService)
        {
            _expenseService = expenseService;
        }


        [HttpPost("Create")]
        public async Task<IActionResult> CreateExpenses([FromBody] List<Expense> expenses)
        {

  
            if (expenses == null || !expenses.Any())
            {
                return BadRequest("Expenses list cannot be empty.");
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

                foreach (Expense expense in expenses)
                {
                    expense.UserId = userId;
                    Console.WriteLine($"📦 Server received expense with MonthId: {expense.MonthId}");

                }

                var createdExpenses = await _expenseService.CreateExpensesAndAttachToMonthAsync(expenses);
                return Ok(createdExpenses);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error creating Month: {ex.InnerException?.Message ?? ex.Message}");
            }

        }


        [HttpGet("GetAllCategories")]
        public async Task<IActionResult> GetAllCategories()
        {
            List<ExpenseCategory> categories = await _expenseService.GetAllExpenseCategoriesAsync();
            return Ok(categories);
        }

        [HttpGet]
        public async Task<IActionResult> GetExpenses([FromQuery] int userId, [FromQuery] int monthId)
        {
            var expenses = await _expenseService.GetExpensesAsync(userId, monthId);
            return Ok(expenses ?? new List<Expense>());
        }

        [HttpPut("{expenseId}")]
        public async Task<IActionResult> UpdateExpense(int expenseId, [FromBody] Expense expense)
        {
            if (expenseId != expense.ExpenseId)
                return BadRequest("expense ID mismatch");

            await _expenseService.UpdateExpenseAsync(expense);
            return NoContent();
        }

        [HttpDelete("{expenseId}")]
        public async Task<IActionResult> DeleteExpense(int expenseId)
        {
            await _expenseService.DeleteExpenseAsync(expenseId);
            return NoContent();
        }

    }
}
