using QueueSystem.Common.Repository;
using QueueSystem.Implement.EntityModels;

namespace QueueSystem.Implement.Repositories.Interface
{
    public interface ICounterSequenceRepository : IGenericRepository<CounterSequence>
    {
        Task<CounterSequence?> GetCounterSequencesAsync(int counterId, DateTime dateTime);
    }
}
