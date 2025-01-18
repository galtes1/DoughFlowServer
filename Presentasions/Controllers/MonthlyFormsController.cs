using Microsoft.AspNetCore.Mvc;
using AccountManagementServer.Core.Models;
using Microsoft.EntityFrameworkCore;
using AccountManagementServer.Infrastructure.Data;

namespace AccountManagementServer.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MonthlyFormsController : ControllerBase
    {
        private readonly AccountManagementDbContext _context;

        public MonthlyFormsController(AccountManagementDbContext context)
        {
            _context = context;
        }

        // יצירת טופס חדש
        [HttpPost("create")]
        public async Task<IActionResult> CreateMonthlyForm(int userId, DateTime monthYear, List<int> categoryIds)
        {
            var categories = await _context.Categories
                .Where(c => categoryIds.Contains(c.Id))
                .ToListAsync();

            if (!categories.Any())
                return BadRequest("לא נמצאו קטגוריות.");

            var monthlyForm = new MonthlyForm
            {
                UserId = userId,
                MonthYear = monthYear,
                Entries = categories.Select(c => new MonthlyFormEntry
                {
                    CategoryId = c.Id,
                    Amount = 0 // ערך ברירת מחדל
                }).ToList()
            };

            _context.MonthlyForms.Add(monthlyForm);
            await _context.SaveChangesAsync();

            return Ok(monthlyForm);
        }

        // עדכון סכום בטופס
        [HttpPost("update")]
        public async Task<IActionResult> UpdateEntry(int formId, int entryId, decimal amount)
        {
            var form = await _context.MonthlyForms
                .Include(f => f.Entries)
                .FirstOrDefaultAsync(f => f.Id == formId);

            if (form == null || form.IsLocked)
                return BadRequest("הטופס לא נמצא או נעול לעריכה.");

            var entry = form.Entries.FirstOrDefault(e => e.Id == entryId);
            if (entry == null)
                return NotFound("הרשומה לא נמצאה.");

            entry.Amount = amount;
            await _context.SaveChangesAsync();

            return Ok(entry);
        }

        // נעלת טופס
        [HttpPost("lock")]
        public async Task<IActionResult> LockForm(int formId)
        {
            var form = await _context.MonthlyForms.FirstOrDefaultAsync(f => f.Id == formId);
            if (form == null)
                return NotFound("הטופס לא נמצא.");

            form.IsLocked = true;
            await _context.SaveChangesAsync();

            return Ok("הטופס ננעל לעריכה.");
        }
    }
}
