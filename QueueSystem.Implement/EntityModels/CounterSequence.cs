using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueueSystem.Implement.EntityModels
{
    public class CounterSequence
    {
        public int Id { get; set; }
        public int CounterId { get; set; }
        public DateTime SequenceDate { get; set; } = DateTime.UtcNow.Date;
        public int LastNumber { get; set; } = 0;
    }
}
