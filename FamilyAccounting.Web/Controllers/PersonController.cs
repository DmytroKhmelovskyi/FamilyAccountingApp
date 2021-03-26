using FamilyAccounting.Web.Interfaces;
using FamilyAccounting.Web.Models;
using Microsoft.AspNetCore.Mvc;
using X.PagedList;

namespace FamilyAccounting.Web.Controllers
{
    public class PersonController : Controller
    {
        private readonly IPersonWebService personsWebService;
        public PersonController(IPersonWebService personsWebService)
        {
            this.personsWebService = personsWebService;
        }

        public IActionResult Index(int? page)
        {
            var pageNumber = page ?? 1;
            var person = personsWebService.Get();
            var onePageOfPersons = person.ToPagedList(pageNumber, 8);
            ViewBag.OnePageOfPersons = onePageOfPersons;
            return View(person);
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
                personsWebService.Add(person);
                return RedirectToAction("Index");
            }
            return Content("Invalid inputs");
        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            var updatedPerson = personsWebService.Get(id);
            return View(updatedPerson);
        }

        [HttpPost]
        public IActionResult Update(int id, PersonViewModel person)
        {
            if (ModelState.IsValid)
            {
                personsWebService.Update(id, person);
            }
            return RedirectToAction("Index");
        }
        public IActionResult Details(int Id, int? page)
        {
            var pageNumber = page ?? 1;
            var person = personsWebService.Get(Id);
            person.Wallets = personsWebService.GetWallets(Id);
            var onePageOfWallets = person.Wallets.ToPagedList(pageNumber, 4);
            ViewBag.OnePageOfWallets = onePageOfWallets;
            return View(person);
        }

        [HttpGet]
        public ViewResult Delete(int? id)
        {
            var person = personsWebService.Get((int)id);
            return View(person);
        }

        [ActionName("Delete")]
        [HttpPost]
        public IActionResult DeletePerson(int? id)
        {
            personsWebService.Delete((int)id);
            return RedirectToAction("Index");
        }
    }
}
