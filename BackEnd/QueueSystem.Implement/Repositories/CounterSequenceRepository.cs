using Implement.Repositories;
using Microsoft.EntityFrameworkCore;
using QueueSystem.Implement.ApplicationDbContext;
using QueueSystem.Implement.EntityModels;
using QueueSystem.Implement.Repositories.Interface;

namespace QueueSystem.Implement.Repositories
{
    public class CounterSequenceRepository : GenericRepository<CounterSequence>, ICounterSequenceRepository
    {
        public CounterSequenceRepository(QueueSystemDbContext context) : base(context)
        {
        }

        public async Task<CounterSequence?> GetCounterSequencesAsync(int counterId, DateTime dateTime)
        {
            return await _context.CounterSequences
                .FirstOrDefaultAsync(s => s.CounterId == counterId && s.SequenceDate == dateTime);
        }
    }
}
