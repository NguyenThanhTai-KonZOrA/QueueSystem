using Implement.Repositories;
using Microsoft.EntityFrameworkCore;
using QueueSystem.Implement.ApplicationDbContext;
using QueueSystem.Implement.EntityModels;
using QueueSystem.Implement.Repositories.Interface;

namespace QueueSystem.Implement.Repositories
{
    public class PatronRepository : GenericRepository<Patron>, IPatronRepository
    {
        public PatronRepository(QueueSystemDbContext context) : base(context)
        {
        }

        public async Task<Patron?> GetUserByPhoneOrEmailAsync(string? phone, string? email)
        {
            return await _context.Patrons
               .FirstOrDefaultAsync(u =>
                   (!string.IsNullOrEmpty(phone) && u.Phone == phone) ||
                   (!string.IsNullOrEmpty(email) && u.Email == email));
        }
    }
}
