using Microsoft.AspNetCore.Mvc;
using QueueSystem.Implement.Services;
using QueueSystem.Implement.ViewModels;

namespace QueueSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class QueueController : ControllerBase
    {
        private readonly IQueueService _queueService;

        public QueueController(IQueueService queueService)
        {
            _queueService = queueService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto request)
        {
            var result = await _queueService.RegisterAsync(request);
            return Ok(result);
        }

        [HttpGet("tickets")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _queueService.GetAllTicketsAsync();
            return Ok(result);
        }
    }
}
