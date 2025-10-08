using Microsoft.AspNetCore.Mvc;
using QueueSystem.Implement.Services.Interface;
using QueueSystem.Implement.ViewModels.Request;

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
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            try
            {
                var result = await _queueService.RegisterAsync(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw new BadHttpRequestException(ex.Message);
            }

        }

        [HttpGet("tickets")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var result = await _queueService.GetAllTicketsAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw new BadHttpRequestException(ex.Message);
            }
        }
    }
}
