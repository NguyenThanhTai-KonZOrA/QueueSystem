namespace QueueSystem.Implement.EntityModels
{
    public class Patron
    {
        public int Id { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string Phone { get; set; }
        public string Email { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public ICollection<QueueTicket>? Tickets { get; set; }
    }
}
