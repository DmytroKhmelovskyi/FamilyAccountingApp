using FamilyAccounting.BL.DTO;
using FamilyAccounting.BL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

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
        public async Task<ActionResult> GetAll()
        {
            return new OkObjectResult(await personsService.Get());
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<ActionResult> Add(PersonDTO person)
        {
            if (ModelState.IsValid)
            {
                return new OkObjectResult(await personsService.Add(person));
            }
            return new BadRequestResult();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, PersonDTO person)
        {
            if (ModelState.IsValid)
            {
                return new OkObjectResult(await personsService.Update(id, person));
            }
            return new BadRequestResult();
        }
        [HttpGet("{id}")]
        public async Task<ActionResult> Details(int Id)
        {
            var person = await personsService.Get(Id);
            person.Wallets = await personsService.GetWallets(Id);
            return new OkObjectResult(person);
        }

        [ActionName("Delete")]
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeletePerson(int id)
        {
            await personsService.Delete(id);
            return new OkResult();
        }
    }
}
