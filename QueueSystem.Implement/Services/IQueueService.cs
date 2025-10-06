using QueueSystem.Implement.ViewModels;

namespace QueueSystem.Implement.Services
{
    public interface IQueueService
    {
        Task<RegisterResultDto> RegisterAsync(RegisterRequestDto request);
        Task<List<TicketInfoDto>> GetAllTicketsAsync();
    }
}
