using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppRunning.Models.ActivityViewModels
{
    public class ActivityDetailsViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public TimeSpan Time { get; set; }
        public float LatitudeStart { get; set; }
        public float LongitudeStart { get; set; }
    }
}
