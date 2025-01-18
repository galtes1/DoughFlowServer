using Microsoft.AspNetCore.Mvc;
using AccountManagementServer.Infrastructure.Data;
using AccountManagementServer.Core.Models.AccountManagementServer.Core.Models;

namespace AccountManagementServer.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriesController : ControllerBase
    {
        private readonly AccountMaagmentDbContext _context;

        public CategoriesController(AccountMaagmentDbContext context)
        {
            _context = context;
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddCategory(string name, string type)
        {
            if (string.IsNullOrWhiteSpace(name) || (type != "Revenue" && type != "Expense"))
                return BadRequest("שם וסוג קטגוריה שגויים.");

            var category = new Category { Name = name, Type = type };
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();

            return Ok(category);
        }
    }
}
