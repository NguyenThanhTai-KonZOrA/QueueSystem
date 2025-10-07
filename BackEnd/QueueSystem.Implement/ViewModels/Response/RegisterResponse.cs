namespace QueueSystem.Implement.ViewModels.Response
{
    public class RegisterResponse
    {
        public int TicketId { get; set; }
        public int PatronId { get; set; }
        public int CounterId { get; set; }
        public int TicketNumber { get; set; }
        public string TicketDate { get; set; } = string.Empty;
        public string Status { get; set; } = "Pending";
    }
}
