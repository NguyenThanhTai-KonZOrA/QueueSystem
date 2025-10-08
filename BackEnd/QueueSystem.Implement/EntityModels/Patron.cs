namespace QueueSystem.Implement.EntityModels
{
    public class Patron
    {
        public int Id { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string Phone { get; set; }
        public string Email { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public bool IsActive { get; set; } = true;
        public bool IsDelete { get; set; } = false;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
        public ICollection<QueueTicket>? Tickets { get; set; }
    }
}
