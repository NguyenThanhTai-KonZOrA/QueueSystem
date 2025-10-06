using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueueSystem.Implement.ViewModels
{
    public class RegisterResultDto
    {
        public int TicketId { get; set; }
        public int UserId { get; set; }
        public int CounterId { get; set; }
        public string TicketNumber { get; set; } = string.Empty;
        public string TicketDate { get; set; } = string.Empty;
        public string Status { get; set; } = "Pending";
    }
}
