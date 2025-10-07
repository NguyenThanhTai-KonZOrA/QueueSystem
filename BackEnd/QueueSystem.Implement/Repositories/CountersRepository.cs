using Implement.Repositories;
using QueueSystem.Implement.ApplicationDbContext;
using QueueSystem.Implement.EntityModels;
using QueueSystem.Implement.Repositories.Interface;

namespace QueueSystem.Implement.Repositories
{
    public class CountersRepository : GenericRepository<Counters>, ICountersRepository
    {
        public CountersRepository(QueueSystemDbContext context) : base(context)
        {
        }
    }
}
