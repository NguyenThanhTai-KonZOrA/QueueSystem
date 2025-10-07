using Implement.Repositories;
using QueueSystem.Implement.ApplicationDbContext;
using QueueSystem.Implement.EntityModels;
using QueueSystem.Implement.Repositories.Interface;

namespace QueueSystem.Implement.Repositories
{
    public class TicketArchiveRepository : GenericRepository<TicketArchive>, ITicketArchiveRepository
    {
        public TicketArchiveRepository(QueueSystemDbContext context) : base(context)
        {
        }
    }
}
