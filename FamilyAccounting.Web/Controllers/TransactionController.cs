using FamilyAccounting.BL.Interfaces;
using FamilyAccounting.Web.Models;
using FamilyAccounting.Web.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FamilyAccounting.Web.Controllers
{
    public class TransactionController : Controller
    {
        private readonly ITransactionService transactionService;

        public TransactionController(ITransactionService transactionService)
        {
            this.transactionService = transactionService;
        }

        [HttpPost]
        public IActionResult MakeExpense(TransactionViewModel transaction)
        {
            if (ModelState.IsValid)
            {
                transactionService.MakeExpense(MapperService.TransactionMap(transaction));
                return RedirectToAction("Index", "Person");
            }
            else
            {
                return Content("Invalid inputs");
            }          
        }
    }
}
