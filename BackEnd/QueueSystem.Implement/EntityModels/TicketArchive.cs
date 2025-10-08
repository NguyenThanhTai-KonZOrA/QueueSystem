namespace QueueSystem.Implement.EntityModels
{
    public class TicketArchive
    {
        public int Id { get; set; }
        public int OriginalTicketId { get; set; }
        public int TicketNumber { get; set; }
        public DateTime TicketDate { get; set; }
        public int PatronId { get; set; }
        public int CounterId { get; set; }
        public string Status { get; set; } = null!;
        public string? Remarks { get; set; }
        public bool IsActive { get; set; } = true;
        public bool IsDelete { get; set; } = false;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}
