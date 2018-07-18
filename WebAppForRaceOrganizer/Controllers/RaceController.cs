using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ITSRunningDbRepository;
using ITSRunningDbRepository.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebAppForRaceOrganizer.Models;
using WebAppForRaceOrganizer.Models.RaceViewModels;

namespace WebAppForRaceOrganizer.Controllers
{
    public class RaceController : Controller
    {
        private readonly IRaceRepository _raceRepository;
        private readonly IRaceOrganizerRepository _raceOrganizerRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        private int _idRaceOrganizer;

        public RaceController(IRaceRepository raceRepository, UserManager<ApplicationUser> userManager, IRaceOrganizerRepository raceOrganizerRepository)
        {
            _raceRepository = raceRepository;
            _raceOrganizerRepository = raceOrganizerRepository;
            _userManager = userManager;
            _idRaceOrganizer = 0;
        }

        protected int RaceOrganizerId
        {
            get
            {
                if (_idRaceOrganizer == 0)
                {
                    _idRaceOrganizer = _raceOrganizerRepository.GetIdByUsername(_userManager.GetUserName(User));
                }
                return _idRaceOrganizer;
            }
        }

        public IActionResult Index()
        {
            if (_userManager.GetUserId(User) != null)
            {
                var races = _raceRepository.GetListById(RaceOrganizerId);

                var list = races.Select(t => new RaceViewModel(t));

                return View(list);
            }
            else
            {
                return RedirectToAction("../Account/Login");
            }
        }

        [HttpGet]
        public IActionResult Insert()
        {
            if (_userManager.GetUserId(User) != null)
            {
                var model = new RaceInsertViewModel();
                return View(model);
            }
            else
            {
                return RedirectToAction("../Account/Login");
            }
        }

        [HttpPost]
        public IActionResult Insert(RaceInsertViewModel model)
        {

            if (ModelState.IsValid)
            {
                var race = new Race()
                {
                    Name = model.Name,
                    Description = model.Description,
                    RaceOrganizerId = RaceOrganizerId

                };
                _raceRepository.Insert(race);

                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Errore generico");

            return View(model);
        }

        [HttpGet]
        public IActionResult EnableDisable(int id)
        {
            var race = _raceRepository.Get(id);

            var model = new EnableDisableRaceViewModel();
            model.IsEnabled = race.IsEnabled;

            return View(model);
        }

        [HttpPost]
        public IActionResult EnableDisable(EnableDisableRaceViewModel model)
        {
            if (ModelState.IsValid)
            {
                _raceRepository.EnableDisableRace(model.Id, model.IsEnabled);

                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Errore generico");

            return View(model);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            if (_userManager.GetUserId(User) != null)
            {
                var race = _raceRepository.Get(id);
                if (race == null)
                    return NotFound();

                var model = new RaceViewModel()
                {
                    Name = race.Name,
                    Description = race.Description,
                    IsEnabled = race.IsEnabled
                };

                return View(model);
            }
            else
            {
                return RedirectToAction("../Account/Login");
            }
        }

        [HttpPost]
        public IActionResult Delete([FromRoute]int id, RaceViewModel model)
        {
            if (ModelState.IsValid)
            {
                _raceRepository.Delete(id);

                return RedirectToAction("/Index");
            }

            ModelState.AddModelError("", "Errore generico");

            return View(model);
        }
    }
}