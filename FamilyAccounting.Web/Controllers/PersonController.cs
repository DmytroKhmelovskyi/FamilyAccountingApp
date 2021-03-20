using FamilyAccounting.BL.Interfaces;
using FamilyAccounting.Web.Models;
using FamilyAccounting.Web.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using X.PagedList;

namespace FamilyAccounting.Web.Controllers
{
    public class PersonController : Controller
    {
        private readonly IPersonService personsService;
        public PersonController(IPersonService personsService)
        {
            this.personsService = personsService;
        }

        public IActionResult Index(int? page)
        {
            try
            {
                var pageNumber = page ?? 1;
                var person = personsService.Get();
                var IndexVM = MapperService.PersonMap(person);
                var onePageOfPersons = IndexVM.Persons.ToPagedList(pageNumber, 8);
                ViewBag.OnePageOfPersons = onePageOfPersons;
                    //return View(IndexVM);
                return View();
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
                personsService.Add(MapperService.PersonMap(person));
                return RedirectToAction("Index");
            }

            return Content("Invalid inputs");
        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            try
            {
                var updatedPerson = personsService.Get(id);
                return View(MapperService.PersonMap(updatedPerson));
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPost]
        public IActionResult Update(int id, PersonViewModel person)
        {
            if (ModelState.IsValid)
            {
                personsService.Update(id, MapperService.PersonMap(person));
            }
            return RedirectToAction("Index");
        }
        public IActionResult Details(int Id)
        {
            try
            {
                var person = personsService.Get(Id);
                person.Wallets = personsService.GetWallets(Id);
                return View(MapperService.PersonMap(person, person.Wallets));
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
