using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ITSRunningDbRepository.Models;

namespace WebAppRunning.Models.RaceModels
{
    public class RaceModel
    {
        public RaceModel()
        {

        }

        public RaceModel(Race race)
        {
            this.Id = race.Id;
            this.Name = race.Name;
            this.Description = race.Description;
            this.RaceOrganizerId = race.RaceOrganizerId;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int RaceOrganizerId { get; set; }
    }
}
