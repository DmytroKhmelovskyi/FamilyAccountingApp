using FamilyAccounting.BL.DTO;
using FamilyAccounting.BL.Services;
using FamilyAccounting.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace FamilyAccounting.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private PersonsService personsService;

        public HomeController(ILogger<HomeController> logger, PersonsService personsService)
        {
            _logger = logger;
            this.personsService = personsService;
        }

        public IActionResult Index()
        {
            IEnumerable<PersonDTO> personDTOs = personsService.GetListOfPersons();
            PersonViewModel bvm = new PersonViewModel { Persons = personDTOs };
            return View(bvm);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
