using FamilyAccounting.BL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FamilyAccounting.Web.Controllers
{
    public class CardController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private ICardService cardService;

        public CardController(ILogger<HomeController> logger, ICardService cardService)
        {
            _logger = logger;
            this.cardService = cardService;
        }
    }
}
