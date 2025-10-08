namespace QueueSystem.Implement.EntityModels
{
    public class QueueTicket
    {
        public int Id { get; set; }
        public int TicketNumber { get; set; }         // 1,2,3... (per day per counter)
        public DateTime TicketDate { get; set; } = DateTime.UtcNow.Date;
        public int CounterId { get; set; }
        public Counters? Counter { get; set; }

        public int PatronId { get; set; }
        public Patron? Patron { get; set; }

        public string Status { get; set; } = "Pending";
        public bool IsActive { get; set; } = true;
        public bool IsDelete { get; set; } = false;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}
