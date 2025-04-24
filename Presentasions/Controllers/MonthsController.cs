using System.Security.Claims;
using AccountManagementServer.Application.Interface;
using AccountManagementServer.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace AccountManagementServer.Presentasions.Controllers
{
    [Route("api/months")]
    [ApiController]
    public class MonthsController : ControllerBase
    {
        private readonly IMonthService _monthService;

        public MonthsController(IMonthService monthService)
        {
            _monthService = monthService;
        }

        [HttpGet("pie-data")]
        public async Task<IActionResult> GetPieDataByMonth([FromQuery] int userId, [FromQuery] int monthId)
        {
                  
            try

            {

                var monthData = await _monthService.GetPieDataByMonthAsync(userId, monthId);

                if (monthData == null)
                {
                    return NotFound(new { expenses = new List<Expense>(), incomes = new List<Income>() });
                }

                return Ok(new
                {
                    expenses = monthData.Expenses ?? new List<Expense>(),
                    incomes = monthData.Incomes ?? new List<Income>()
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }



        [HttpPost("Create")]
        public async Task<IActionResult> CreateMonth([FromBody] Month month)
        {
            Claim? userIdClaim = HttpContext.User.Claims.FirstOrDefault(e => e.Type == "UserId");
            if (userIdClaim == null)
            {
                return Unauthorized("User ID is missing.");
            }

            int userId = Convert.ToInt32(userIdClaim.Value);

            if (month == null)
            {
                return BadRequest("Month object cannot be null.");
            }
            try
            {
                Month createdMonth = await _monthService.CreateMonthAsync(month);
                return Ok(createdMonth);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error creating Month: {ex.InnerException?.Message ?? ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetMonthById(int id)
        {
            var month = await _monthService.GetMonthByIdAsync(id);
            if (month == null)
            {
                return NotFound($"Month with ID {id} not found.");
            }
            return Ok(month);
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllMonths()
        {
            var months = await _monthService.GetAllMonthsAsync();
            return Ok(months);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMonth(int id, [FromBody] Month month)
        {
            if (month == null || id != month.MonthId)
            {
                return BadRequest("Month ID mismatch or null Month object.");
            }
            var updatedMonth = await _monthService.UpdateMonthAsync(month);
            if (updatedMonth == null)
            {
                return NotFound($"Month with ID {id} not found.");
            }
            return Ok(updatedMonth);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMonth(int id)
        {
            var success = await _monthService.DeleteMonthAsync(id);
            if (!success)
            {
                return NotFound($"Month with ID {id} not found.");
            }
            return Ok($"Month with ID {id} was deleted successfully.");
        }


        

        [HttpGet("get-month-id")]
        public async Task<IActionResult> GetMonthId(int userId, int month, int year)
        {
            var monthId = await _monthService.GetMonthIdByDateAndUserAsync(userId, month, year);

            if (monthId == null)
            {
                var newMonth = new Month
                {
                    UserId = userId,
                    Date = new DateTime(year, month, 1) // יוצרים תאריך על בסיס שנה וחודש
                };

                var createdMonth = await _monthService.CreateMonthAsync(newMonth);
                return Ok(new { monthId = createdMonth.MonthId });
            }

            return Ok(new { monthId });
        }



    }
}
