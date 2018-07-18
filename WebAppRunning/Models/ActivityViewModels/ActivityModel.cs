using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebAppRunning.Models.RaceModels;

namespace WebAppRunning.Models.ActivityViewModels
{
    public class ActivityModel
    {
        public int Id { get; set; }
        public int IdRunner { get; set; }
        public DateTime CreationDate { get; set; }
        public string Location { get; set; }
        public int Type { get; set; }

        [Required]
        public int? IdRace { get; set; }

        public IEnumerable<RaceModel> Races { get; set; }
    }
}
