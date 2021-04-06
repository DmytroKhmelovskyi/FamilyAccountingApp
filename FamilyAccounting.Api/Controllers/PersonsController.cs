using FamilyAccounting.BL.DTO;
using FamilyAccounting.BL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace FamilyAccounting.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PersonsController : ControllerBase
    {
        private readonly IPersonService personsService;
        public PersonsController(IPersonService personsService)
        {
            this.personsService = personsService;
        }
        [HttpGet]
        public ActionResult<IEnumerable<PersonDTO>> Index()
        {
            var person = personsService.Get();
            return person.ToList();
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public ActionResult Add(PersonDTO person)
        {
            if (ModelState.IsValid)
            {
                personsService.Add(person);
                return Ok();
            }
            return Content("Invalid inputs");
        }

        [HttpGet("{id}")]
        public ActionResult<PersonDTO> Update(int id)
        {
            var updatedPerson = personsService.Get(id);
            return updatedPerson;
        }

        [HttpPut("{id}")]
        public ActionResult Update(int id, PersonDTO person)
        {
            if (ModelState.IsValid)
            {
                personsService.Update(id, person);
                return Ok();
            }
            return Content("Invalid inputs");
        }
        [HttpGet("{id}")]
        public ActionResult<PersonDTO> Details(int Id)
        {
            var person = personsService.Get(Id);
            person.Wallets = personsService.GetWallets(Id);
            return person;
        }


        //[HttpGet]
        //public ViewResult Delete(int? id)
        //{
        //    var person = personsWebService.Get((int)id);
        //    return View(person);
        //}

        [ActionName("Delete")]
        [HttpDelete("{id}")]
        public IActionResult DeletePerson(int? id)
        {
            personsService.Delete((int)id);
            return Ok();
        }
    }
}
