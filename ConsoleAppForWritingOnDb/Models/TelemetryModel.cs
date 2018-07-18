using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunctionAppForWritingOnDb.Models
{
    public class TelemetryModel
    {
        public int ActivityId { get; set; }
        public int RunnerId { get; set; }
        public float Latitude { get; set; }
        public float Longitude { get; set; }
        public DateTime Instant { get; set; }
    }
}
