using System;
using System.Collections.Generic;
using System.Text;

namespace ITSRunningDbRepository.Models
{
    public class Activity
    {
        public int Id { get; set; }
        public int IdRunner { get; set; }
        public DateTime CreationDate { get; set; }
        public string Location { get; set; }
        public int Type { get; set; }
        public int? IdRace { get; set; }
    }
}
