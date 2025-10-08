namespace QueueSystem.Implement.EntityModels
{
    public class CounterSequence
    {
        public int Id { get; set; }
        public int CounterId { get; set; }
        public DateTime SequenceDate { get; set; } = DateTime.UtcNow.Date;
        public int LastNumber { get; set; } = 0;
        public bool IsActive { get; set; } = true;
        public bool IsDelete { get; set; } = false;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;

    }
}
