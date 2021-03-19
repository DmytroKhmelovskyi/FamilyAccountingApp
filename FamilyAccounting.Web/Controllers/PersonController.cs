using AutoMapper;
using FamilyAccounting.BL.DTO;
using FamilyAccounting.BL.Interfaces;
using FamilyAccounting.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FamilyAccounting.Web.Controllers
{
    public class PersonController : Controller
    {
        private IPersonService personsService;

        public PersonController(ILogger<HomeController> logger, IPersonService personsService)
        {
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
    }
}
