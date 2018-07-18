using System;
using System.Collections.Generic;
using System.Text;

namespace ITSRunningDbRepository.Models
{
    public class Telemetry
    {
        public int Id { get; set; }
        public float Latitude { get; set; }
        public float Longitude { get; set; }
        public DateTimeOffset Instant { get; set; }
        public int IdActivity { get; set; }
        public string SelfiUri { get; set; }
    }
}
