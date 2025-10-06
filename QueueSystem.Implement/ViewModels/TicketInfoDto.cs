using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueueSystem.Implement.ViewModels
{
    public class TicketInfoDto
    {
        public int TicketId { get; set; }
        public string TicketNumber { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public string CounterName { get; set; } = string.Empty;
        public DateTime TicketDate { get; set; }
        public string Status { get; set; } = "Pending";
    }
}
