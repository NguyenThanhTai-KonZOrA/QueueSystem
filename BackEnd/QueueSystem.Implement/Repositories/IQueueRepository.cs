using QueueSystem.Implement.EntityModels;

namespace QueueSystem.Implement.Repositories
{
    public interface IQueueRepository
    {
        Task<QueueTicket> RegisterTicketTransactionAsync(string fullName, string? phone, string? email, int counterId);
        Task<Patron?> GetUserByPhoneOrEmailAsync(string? phone, string? email);
        Task<List<QueueTicket>> GetAllTicketsWithUserAsync();
        Task ArchiveTicketsOlderThanAsync(DateTime cutoffDate);
    }
}
