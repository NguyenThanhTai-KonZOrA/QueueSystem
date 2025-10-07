using QueueSystem.Common.Repository;
using QueueSystem.Implement.EntityModels;

namespace QueueSystem.Implement.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<Patron> Patron { get; }
        IGenericRepository<TicketArchive> TicketArchive { get; }
        IGenericRepository<QueueTicket> QueueTicket { get; }
        IGenericRepository<Counters> Counters { get; }
        IGenericRepository<CounterSequence> CounterSequence { get; }
        Task<int> CompleteAsync();
        void Update();
        void UpdateRange();
    }
}
