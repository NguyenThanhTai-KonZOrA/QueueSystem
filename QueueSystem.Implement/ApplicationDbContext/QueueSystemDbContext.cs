using Microsoft.EntityFrameworkCore;
using QueueSystem.Implement.EntityModels;

namespace QueueSystem.Implement.ApplicationDbContext
{
    public class QueueSystemDbContext : DbContext
    {
        public QueueSystemDbContext(DbContextOptions<QueueSystemDbContext> options) : base(options)
        {
        }

        public DbSet<Patron> Patrons { get; set; }
        public DbSet<QueueTicket> QueueTickets { get; set; }
        public DbSet<Counters> Counters { get; set; }
        public DbSet<CounterSequence> CounterSequences { get; set; }
        public DbSet<TicketArchive> TicketArchives { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CounterSequence>()
                .HasIndex(c => new { c.CounterId, c.SequenceDate })
                .IsUnique();

            modelBuilder.Entity<QueueTicket>()
                .HasIndex(q => new { q.CounterId, q.TicketDate, q.TicketNumber })
                .IsUnique();

            modelBuilder.Entity<QueueTicket>()
                .Property(q => q.Status)
                .HasDefaultValue("Pending");
        }
    }
}
