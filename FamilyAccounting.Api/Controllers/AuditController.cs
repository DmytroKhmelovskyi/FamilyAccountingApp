using FamilyAccounting.BL.DTO;
using FamilyAccounting.BL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace FamilyAccounting.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuditController : ControllerBase
    {
        private readonly IAuditService auditService;

        public AuditController(IAuditService auditService)
        {
            this.auditService = auditService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<AuditActionDTO>> IndexActions(/*int Id, */)
        {
            var auditActions = auditService.GetActions();
            return auditActions.ToList();
        }

        [HttpGet]
        public ActionResult<IEnumerable<AuditWalletDTO>> IndexWallets(/*int Id, */)
        {
            var auditWallets = auditService.GetWallets();
            return auditWallets.ToList();
        }

        [HttpGet]
        public ActionResult<IEnumerable<AuditPersonDTO>> IndexPersons(/*int Id, */)
        {
            var auditPersons = auditService.GetPersons();
            return auditPersons.ToList();
        }
    }
}
