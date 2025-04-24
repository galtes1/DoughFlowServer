using Microsoft.AspNetCore.Mvc;
using AccountManagementServer.Application.Service;
using Microsoft.AspNetCore.Authorization;

namespace AccountManagementServer.Presentations.Controllers
{
    [ApiController]
    [Route("api/gpt")]
    public class GptController : ControllerBase
    {
        private readonly GptService _gptService;

        public GptController(GptService gptService)
        {
            _gptService = gptService;
        }
        [Authorize(Policy = "MustBeBusiness")]
        [HttpPost("analyze-user-budget/{userId}")]
        public async Task<IActionResult> AnalyzeUserBudget(int userId)
        {
            try
            {
                var result = await _gptService.AnalyzeUserBudgetAsync(userId);
                return Ok(new { response = result });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
    }
}
