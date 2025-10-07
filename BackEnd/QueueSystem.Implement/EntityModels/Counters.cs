using System.ComponentModel.DataAnnotations;

namespace QueueSystem.Implement.EntityModels
{
    public class Counters
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
    }
}
