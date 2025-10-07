using QueueSystem.Common.Repository;
using QueueSystem.Implement.EntityModels;

namespace QueueSystem.Implement.Repositories.Interface
{
    public interface IQueueTicketRepository : IGenericRepository<QueueTicket>
    {
        Task<List<QueueTicket>> GetTicketsOlder(DateTime date);
        Task<List<QueueTicket>> GetAllTicketsWithUserAsync();
    }
}
