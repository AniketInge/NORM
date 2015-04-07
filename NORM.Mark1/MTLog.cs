using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NORM.Attributes;

namespace NORM.Mark1
{
    [NormEntity(Connection = "NotConnection")]
    public class MTLog
    {
        public int MTLogId { get; set; }
        public int UserId { get; set; }
        public string Sender { get; set; }
        
        [Norm(ColumnName = "ProcessingTime")]
        public DateTime ProcessTime { get; set; }
    }
}
