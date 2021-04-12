using FamilyAccounting.BL.DTO;
using FamilyAccounting.BL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FamilyAccounting.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuditsController : ControllerBase
    {
        private readonly IAuditService auditService;

        public AuditsController(IAuditService auditService)
        {
            this.auditService = auditService;
        }

        [HttpGet]
        public Task<IEnumerable<AuditActionDTO>> GetActions()
        {
            return auditService.GetActionsAsync();
        }

        [HttpGet]
        public Task<IEnumerable<AuditWalletDTO>> GetWallets()
        {
            return auditService.GetWalletsAsync();
        }

        [HttpGet]
        public Task<IEnumerable<AuditPersonDTO>> GetPersons()
        {
            return auditService.GetPersonsAsync();
        }
    }
}
