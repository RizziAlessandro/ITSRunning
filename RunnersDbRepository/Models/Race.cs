using System;
using System.Collections.Generic;
using System.Text;

namespace ITSRunningDbRepository.Models
{
    public class Race
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int RaceOrganizerId { get; set; }
        public bool IsEnabled { get; set; }
    }
}
