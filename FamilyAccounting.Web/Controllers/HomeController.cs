using AutoMapper;
using FamilyAccounting.BL.DTO;
using FamilyAccounting.BL.Interfaces;
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
        private IPersonsService personsService;

        public HomeController(ILogger<HomeController> logger, IPersonsService personsService)
        {
            _logger = logger;
            this.personsService = personsService;
        }

        public IActionResult Index()
        {
            IEnumerable<PersonDTO> personDTOs = personsService.Get();
            var config = new MapperConfiguration(cfg => cfg.CreateMap<PersonDTO, PersonViewModel>());
            var mapper = new Mapper(config);
            var personVM = mapper.Map<IEnumerable<PersonViewModel>>(personDTOs);
            return View(personVM);
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
