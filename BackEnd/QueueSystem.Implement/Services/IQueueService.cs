using QueueSystem.Implement.ViewModels.Request;
using QueueSystem.Implement.ViewModels.Response;

namespace QueueSystem.Implement.Services
{
    public interface IQueueService
    {
        Task<RegisterResponse> RegisterAsync(RegisterRequest request);
        Task<List<TicketInforResponse>> GetAllTicketsAsync();
    }
}
