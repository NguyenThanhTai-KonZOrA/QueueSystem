using QueueSystem.Common.Repository;
using QueueSystem.Implement.EntityModels;

namespace QueueSystem.Implement.Repositories.Interface
{
    public interface IPatronRepository : IGenericRepository<Patron>
    {
        Task<Patron?> GetUserByPhoneOrEmailAsync(string? phone, string? email);
    }
}
