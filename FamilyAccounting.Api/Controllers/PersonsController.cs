using FamilyAccounting.BL.DTO;
using FamilyAccounting.BL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
        public ActionResult Index()
        {
            var person = personsService.Get();
            return new OkObjectResult(person.ToList());
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public ActionResult Add(PersonDTO person)
        {
            if (ModelState.IsValid)
            {
                personsService.Add(person);
                return new OkResult();
            }
            return new BadRequestResult();
        }

        [HttpGet("{id}")]
        public ActionResult Update(int id)
        {
            var updatedPerson = personsService.Get(id);
            return new OkObjectResult(updatedPerson);
        }

        [HttpPut("{id}")]
        public ActionResult Update(int id, PersonDTO person)
        {
            if (ModelState.IsValid)
            {
                personsService.Update(id, person);
                return Ok();
            }
            return new BadRequestResult();
        }
        [HttpGet("{id}")]
        public ActionResult Details(int Id)
        {
            var person = personsService.Get(Id);
            person.Wallets = personsService.GetWallets(Id);
            return new OkObjectResult(person);
        }

        [HttpGet]
        public ActionResult Delete(int? id)
        {
            var person = personsService.Get((int)id);
            return new OkObjectResult(person);
        }

        [ActionName("Delete")]
        [HttpDelete("{id}")]
        public ActionResult DeletePerson(int? id)
        {
            personsService.Delete((int)id);
            return new OkResult();
        }
    }
}
