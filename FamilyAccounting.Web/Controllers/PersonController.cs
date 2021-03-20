using FamilyAccounting.BL.Interfaces;
using FamilyAccounting.Web.Models;
using FamilyAccounting.Web.Services;
using Microsoft.AspNetCore.Mvc;
using System;

namespace FamilyAccounting.Web.Controllers
{
    public class PersonController : Controller
    {
        private readonly IPersonService personsService;
        public PersonController(IPersonService personsService)
        {
            this.personsService = personsService;
        }

        public IActionResult Index()
        {
            try
            {
                var person = personsService.Get();
                var IndexVM = MapperService.PersonMap(person);
                return View(IndexVM);
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
                var createdPerson = personsService.Add(MapperService.PersonMap(person));
                return RedirectToAction("Index");
            }

            return Content("Invalid inputs");
        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            var updatedPerson = personsService.Get(id);
            return View(MapperService.PersonMap(updatedPerson));
        }

        [HttpPost]
        public IActionResult Update(int id, PersonViewModel person)
        {
            var updatedPerson = personsService.Update(id, MapperService.PersonMap(person));
            return RedirectToAction("Index");
        }
        public IActionResult Details(int Id)
        {
            try
            {
                var person = personsService.Get(Id);
                return View(MapperService.PersonMap(person));
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpGet]
        public ViewResult Delete(int? id)
        {
            var person = personsService.Get((int)id);
            return View(MapperService.PersonMap(person));
        }

        [ActionName("Delete")]
        [HttpPost]
        public IActionResult DeletePerson(int? id)
        {
            personsService.Delete((int)id);
            return RedirectToAction("Index");
        }
    }
}
