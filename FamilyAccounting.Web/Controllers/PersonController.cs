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
            try
            {
                var personDTOs = personsService.Get();
                var config = new MapperConfiguration(cfg => cfg.CreateMap<PersonDTO, PersonViewModel>());
                var mapper = new Mapper(config);
                var personVM = mapper.Map<IEnumerable<PersonViewModel>>(personDTOs);
                var indexVM = new IndexPersonViewModel
                {
                    Persons = personVM
                };
                return View(indexVM);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(PersonViewModel person)
        {
            if (ModelState.IsValid)
            {
                var config = new MapperConfiguration(cfg => cfg.CreateMap<PersonViewModel, PersonDTO>());
                var mapper = new Mapper(config);
                var createdPerson = personsService.Add(mapper.Map<PersonDTO>(person));
                return RedirectToAction("Index");
            }

            return Content("Invalid inputs");
        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<PersonDTO, PersonViewModel>());
            var mapper = new Mapper(config);
            var updatedPerson = personsService.Get(id);
            mapper.Map<PersonViewModel>(updatedPerson);
            return View(mapper.Map<PersonViewModel>(updatedPerson));
        }

        [HttpPost]
        public IActionResult Update(int id, PersonViewModel person)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<PersonViewModel, PersonDTO>());
            var mapper = new Mapper(config);
            var updatedPerson = personsService.Update(id, mapper.Map<PersonDTO>(person));
            return RedirectToAction("Index");
        }
        public IActionResult Details(int Id)
        {
            try
            {
                var personDTOs = personsService.Get(Id);
                var config = new MapperConfiguration(cfg => cfg.CreateMap<PersonDTO, PersonViewModel>());
                var mapper = new Mapper(config);
                var personVM = mapper.Map<IEnumerable<PersonViewModel>>(personDTOs);
                return View(personVM);
            }
            catch(Exception)
            {
                return BadRequest();
            }
        }
    }
}
