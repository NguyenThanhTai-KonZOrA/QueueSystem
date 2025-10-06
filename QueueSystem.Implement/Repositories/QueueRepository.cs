using Microsoft.EntityFrameworkCore;
using QueueSystem.Implement.ApplicationDbContext;
using QueueSystem.Implement.EntityModels;
using QueueSystem.Implement.Repositories;

namespace QueueSystem.Repositories
{
    public class QueueRepository : IQueueRepository
    {
        private readonly QueueSystemDbContext _context;

        public QueueRepository(QueueSystemDbContext context)
        {
            _context = context;
        }

        public async Task<Patron?> GetUserByPhoneOrEmailAsync(string? phone, string? email)
        {
            return await _context.Patrons
                .FirstOrDefaultAsync(u =>
                    (!string.IsNullOrEmpty(phone) && u.Phone == phone) ||
                    (!string.IsNullOrEmpty(email) && u.Email == email));
        }

        public async Task<List<QueueTicket>> GetAllTicketsWithUserAsync()
        {
            return await _context.QueueTickets
                .Include(q => q.Patron)
                .Include(q => q.Counter)
                .OrderByDescending(q => q.CreatedAt)
                .ToListAsync();
        }

        public async Task<QueueTicket> RegisterTicketTransactionAsync(string fullName, string? phone, string? email, int counterId)
        {
            using var transaction = await _context.Database.BeginTransactionAsync(System.Data.IsolationLevel.Serializable);

            var patron = await GetUserByPhoneOrEmailAsync(phone, email);
            if (patron == null)
            {
                patron = new Patron
                {
                    FullName = fullName,
                    Phone = phone,
                    Email = email,
                    CreatedAt = DateTime.UtcNow
                };
                _context.Patrons.Add(patron);
                await _context.SaveChangesAsync();
            }

            var today = DateTime.UtcNow.Date;
            var sequence = await _context.CounterSequences
                .FirstOrDefaultAsync(s => s.CounterId == counterId && s.SequenceDate == today);

            if (sequence == null)
            {
                sequence = new CounterSequence
                {
                    CounterId = counterId,
                    SequenceDate = today,
                    LastNumber = 1
                };
                _context.CounterSequences.Add(sequence);
            }
            else
            {
                sequence.LastNumber += 1;
                _context.CounterSequences.Update(sequence);
            }

            await _context.SaveChangesAsync();

            var ticket = new QueueTicket
            {
                TicketNumber = sequence.LastNumber,
                TicketDate = today,
                CounterId = counterId,
                PatronId = patron.Id,
                Status = "Pending",
                CreatedAt = DateTime.UtcNow
            };

            _context.QueueTickets.Add(ticket);
            await _context.SaveChangesAsync();

            await transaction.CommitAsync();
            return ticket;
        }

        public async Task ArchiveTicketsOlderThanAsync(DateTime cutoffDate)
        {
            var oldTickets = await _context.QueueTickets
                .Where(t => t.TicketDate < cutoffDate)
                .ToListAsync();

            if (oldTickets.Count == 0)
                return;

            var archives = oldTickets.Select(t => new TicketArchive
            {
                OriginalTicketId = t.Id,
                TicketNumber = t.TicketNumber,
                TicketDate = t.TicketDate,
                CounterId = t.CounterId,
                PatronId = t.PatronId,
                Status = t.Status,
                CreatedAt = t.CreatedAt
            }).ToList();

            _context.TicketArchives.AddRange(archives);
            _context.QueueTickets.RemoveRange(oldTickets);
            await _context.SaveChangesAsync();
        }
    }
}
