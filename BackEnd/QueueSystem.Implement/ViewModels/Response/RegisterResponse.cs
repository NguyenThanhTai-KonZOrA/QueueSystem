namespace QueueSystem.Implement.ViewModels.Response
{
    public class RegisterResponse
    {
        public int TicketId { get; set; }
        public int UserId { get; set; }
        public int CounterId { get; set; }
        public string TicketNumber { get; set; } = string.Empty;
        public string TicketDate { get; set; } = string.Empty;
        public string Status { get; set; } = "Pending";
    }
}
