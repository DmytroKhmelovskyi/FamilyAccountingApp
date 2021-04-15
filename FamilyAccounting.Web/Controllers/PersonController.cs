using FamilyAccounting.Web.Interfaces;
using FamilyAccounting.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
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

        public async Task<IActionResult> Index(int? page)
        {
            var pageNumber = page ?? 1;
            var person = await personsWebService.Get();
            var onePageOfPersons = person.ToPagedList(pageNumber, 8);
            ViewBag.OnePageOfPersons = onePageOfPersons;
            return View(person);
        }

        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Add()
        {
            return View();
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<IActionResult> Add(PersonViewModel person)
        {
            if (ModelState.IsValid)
            {
                await personsWebService.Add(person);
                return RedirectToAction("Index");
            }
            return Content("Invalid inputs");
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var updatedPerson = await personsWebService.Get(id);
            return View(updatedPerson);
        }

        [HttpPost]
        public async Task<IActionResult> Update(int id, PersonViewModel person)
        {
            if (ModelState.IsValid)
            {
                await personsWebService.Update(id, person);
            }
            return RedirectToAction("Details", "Person", new { id });
        }
        public async Task<IActionResult> Details(int Id, int? page)
        {
            var pageNumber = page ?? 1;
            var person = await personsWebService.Get(Id);
            person.Wallets = await personsWebService.GetWallets(Id);
            var onePageOfWallets = person.Wallets.ToPagedList(pageNumber, 4);
            ViewBag.OnePageOfWallets = onePageOfWallets;
            return View(person);
        }

        [HttpGet]
        public async Task<ViewResult> Delete(int? id)
        {
            var person = await personsWebService.Get((int)id);
            return View(person);
        }

        [ActionName("Delete")]
        [HttpPost]
        public async Task<IActionResult> DeletePerson(int? id)
        {
            await personsWebService.Delete((int)id);
            return RedirectToAction("Index");
        }
    }
}
