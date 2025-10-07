namespace QueueSystem.Implement.ViewModels.Request
{
    public class RegisterRequest
    {
        public string FullName { get; set; } = string.Empty;
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public int CounterId { get; set; }
    }
}
