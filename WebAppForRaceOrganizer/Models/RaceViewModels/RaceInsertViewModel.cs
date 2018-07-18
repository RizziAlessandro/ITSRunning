using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppForRaceOrganizer.Models.RaceViewModels
{
    public class RaceInsertViewModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int RaceOrganizerId { get; set; }
        public bool IsEnabled { get; set; }
    }
}
