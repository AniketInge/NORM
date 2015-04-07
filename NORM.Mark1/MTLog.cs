using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NORM.Mark1
{
    public class MTLog
    {
        public int MTLogId { get; set; }
        public int UserId { get; set; }
        public string Sender { get; set; }
        
        public DateTime ProcessingTime { get; set; }
    }
}
