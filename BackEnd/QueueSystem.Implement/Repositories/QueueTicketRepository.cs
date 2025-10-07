using Implement.Repositories;
using Microsoft.EntityFrameworkCore;
using QueueSystem.Implement.ApplicationDbContext;
using QueueSystem.Implement.EntityModels;
using QueueSystem.Implement.Repositories.Interface;

namespace QueueSystem.Implement.Repositories
{
    public class QueueTicketRepository : GenericRepository<QueueTicket>, IQueueTicketRepository
    {
        private readonly QueueSystemDbContext _context;
        public QueueTicketRepository(QueueSystemDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<QueueTicket>> GetAllTicketsWithUserAsync()
        {
            return await _context.QueueTickets
              .Include(q => q.Patron)
              .Include(q => q.Counter)
              .OrderByDescending(q => q.CreatedAt)
              .ToListAsync();
        }

        public async Task<List<QueueTicket>> GetTicketsOlder(DateTime date)
        {
            return await _context.QueueTickets
                .Where(t => t.TicketDate < date)
                .ToListAsync();
        }
    }
}
