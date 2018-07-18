using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;
using GeoCoordinatePortable;
using ITSRunningDbRepository;
using ITSRunningDbRepository.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.FileSystemGlobbing;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Queue;
using StackExchange.Redis;
using WebAppRunning.Models;
using WebAppRunning.Models.ActivityViewModels;
using WebAppRunning.Models.RaceModels;

namespace WebAppRunning.Controllers
{
    public class MapController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IActivityRepository _activityRepository;
        private readonly IRaceRepository _raceRepository;
        private readonly IRunnerRepository _runnerRepository;
        private readonly ITelemetryRepository _telemetryRepository;
        private int _idRunner;

        public MapController(UserManager<ApplicationUser> userManager,
            IActivityRepository activityRepository,
            IRaceRepository raceRepository,
            IRunnerRepository runnerRepository,
            ITelemetryRepository telemetryRepository,
            IConfiguration configuration)
        {
            _configuration = configuration;
            _userManager = userManager;
            _activityRepository = activityRepository;
            _raceRepository = raceRepository;
            _runnerRepository = runnerRepository;
            _telemetryRepository = telemetryRepository;
            _idRunner = 0;
        }

        protected int RunnerId
        {
            get
            {
                if (_idRunner == 0)
                {
                    _idRunner = _runnerRepository.GetIdByUsername(_userManager.GetUserName(User));
                }
                return _idRunner;
            }
        }

        [HttpGet]
        public IActionResult Start()
        {
            if (_userManager.GetUserId(User) != null)
            {
                var races = _raceRepository.GetRaceEnabled();

                var list = races.Select(t => new RaceModel(t));

                var model = new ActivityModel();
                model.Races = list.ToArray();

                return View(model);
            }
            else
            {
                return RedirectToAction("../Account/Login");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Map(ActivityModel model)
        {
            //if type of activity is training set to null id race value
            if (model.Type == 1)
            {
                model.IdRace = null;
            }

            var activity = new Activity
            {
                CreationDate = DateTime.Now,
                Location = "Location fodeong",
                Type = model.Type,
                IdRunner = RunnerId,
                IdRace = model.IdRace
            };

            int idActivity = _activityRepository.InsertAndReturnId(activity);

            model.Id = idActivity;

            return View(model);
        }

        [HttpGet]
        public IActionResult ActivityDetails(int id)
        {
            //int id = Int32.Parse(Request.Query["id"]);

            var activity = _activityRepository.Get(id);
            var list = _telemetryRepository.GetFirstAndLastTelemetry(id);

            TimeSpan time = list.Last().Instant.Subtract(list.First().Instant);

            var runner = _runnerRepository.Get(activity.IdRunner);

            var details = new ActivityDetailsViewModel
            {
                LatitudeStart = list.First().Latitude,
                LongitudeStart = list.First().Longitude,
                FirstName = runner.FirstName,
                LastName = runner.LastName,
                Time = time,
            };

            return View(details);
        }

        public IActionResult Index()
        {
            if (_userManager.GetUserId(User) != null)
            {
                var activities = _activityRepository.GetListByIdRunner(RunnerId);

                var list = activities.Select(t => new ActivityViewModel(t));

                return View(list);
            }
            else
            {
                return RedirectToAction("../Account/Login");
            }
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            if (_userManager.GetUserId(User) != null)
            {
                var activity = _activityRepository.Get(id);

                var model = new ActivityViewModel
                {
                    CreationDate = activity.CreationDate,
                    Type = activity.Type,
                    IdRace = activity.IdRace
                };

                return View(model);
            }
            else
            {
                return RedirectToAction("../Account/Login");
            }
        }

        [HttpPost]
        public IActionResult Delete([FromRoute]int id, ActivityViewModel model)
        { 
            var activity = _activityRepository.Get(id);
            if (activity == null)
                return NotFound();

            _telemetryRepository.Delete(id);

            _activityRepository.Delete(id);

            return RedirectToAction("/Index");
        }
    }
}