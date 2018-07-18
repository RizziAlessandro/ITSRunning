using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ITSRunningDbRepository.Models;

namespace WebAppForRaceOrganizer.Models.RaceViewModels
{
    public class RaceViewModel
    {
        public RaceViewModel()
        {

        }

        public RaceViewModel(Race race)
        {
            this.Id = race.Id;
            this.Name = race.Name;
            this.Description = race.Description;
            this.RaceOrganizerId = race.RaceOrganizerId;
            this.IsEnabled = race.IsEnabled;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int RaceOrganizerId { get; set; }
        public bool IsEnabled { get; set; }
    }
    
}
