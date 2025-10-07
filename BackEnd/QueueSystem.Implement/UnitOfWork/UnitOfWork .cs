using Implement.Repositories;
using QueueSystem.Common.Repository;
using QueueSystem.Implement.ApplicationDbContext;
using QueueSystem.Implement.EntityModels;

namespace QueueSystem.Implement.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly QueueSystemDbContext _context;
        public UnitOfWork(QueueSystemDbContext context)
        {
            _context = context;
            Patron = new GenericRepository<Patron>(_context);
            TicketArchive = new GenericRepository<TicketArchive>(_context);
            QueueTicket = new GenericRepository<QueueTicket>(_context);
            Counters = new GenericRepository<Counters>(_context);
            CounterSequence = new GenericRepository<CounterSequence>(_context);
        }
        public IGenericRepository<Patron> Patron { get; }

        public IGenericRepository<TicketArchive> TicketArchive { get; }

        public IGenericRepository<QueueTicket> QueueTicket { get; }

        public IGenericRepository<Counters> Counters { get; }

        public IGenericRepository<CounterSequence> CounterSequence { get; }


        public async Task<int> CompleteAsync() => await _context.SaveChangesAsync();
        public void Update() => _context.Update(this);
        public void UpdateRange() => _context.UpdateRange(this);
        public void Dispose() => _context.Dispose();
    }
}
