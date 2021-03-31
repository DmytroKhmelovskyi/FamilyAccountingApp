using FamilyAccounting.Web.Interfaces;
using Microsoft.AspNetCore.Mvc;
using X.PagedList;

namespace FamilyAccounting.Web.Controllers
{
    public class AuditController : Controller
    {
        private readonly IAuditWebService auditWebService;

        public AuditController(IAuditWebService auditWebService)
        {
            this.auditWebService = auditWebService;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult IndexActions(/*int Id, */int? page)
        {
            var pageNumber = page ?? 1;
            var auditActions = auditWebService.GetActions();
            var onePageOfAuditActions = auditActions.ToPagedList(pageNumber, 20);
            ViewBag.onePageOfAuditActions = onePageOfAuditActions;
            return View(auditActions);
        }
        public IActionResult IndexWallets(/*int Id, */int? page)
        {
            var pageNumber = page ?? 1;
            var auditWallets = auditWebService.GetWallets();
            var onePageOfAuditWallets = auditWallets.ToPagedList(pageNumber, 20);
            ViewBag.onePageOfAuditWallets = onePageOfAuditWallets;
            return View(auditWallets);
        }
        public IActionResult IndexPersons(/*int Id, */int? page)
        {
            var pageNumber = page ?? 1;
            var auditPersons = auditWebService.GetPersons();
            var onePageOfAuditPersons = auditPersons.ToPagedList(pageNumber, 20);
            ViewBag.onePageOfAuditPersons = onePageOfAuditPersons;
            return View(auditPersons);
        }
    }
}
