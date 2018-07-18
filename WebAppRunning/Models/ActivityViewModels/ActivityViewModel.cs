using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ITSRunningDbRepository.Models;

namespace WebAppRunning.Models.ActivityViewModels
{
    public class ActivityViewModel
    {
        public ActivityViewModel()
        {

        }

        public ActivityViewModel(Activity activity)
        {
            this.Id = activity.Id;
            //this.IdRunner = activity.IdRunner;
            this.CreationDate = activity.CreationDate;
            this.Location = activity.Location;
            this.Type = activity.Type;
            this.IdRace = activity.IdRace;
        }

        public int Id { get; set; }
        //public int IdRunner { get; set; }
        public DateTime CreationDate { get; set; }
        public string Location { get; set; }
        public int Type { get; set; }
        public int? IdRace { get; set; }
    }
}
