using FamilyAccounting.BL.DTO;
using FamilyAccounting.BL.Interfaces;
using FamilyAccounting.Web.Services;
using Microsoft.AspNetCore.Mvc;
using System;

namespace FamilyAccounting.Web.Controllers
{
    public class WalletController : Controller
    {
        private readonly IWalletService walletService;

        public WalletController(IWalletService walletService)
        {
            this.walletService = walletService;
        }
        public IActionResult Index()
        {
            try
            {
                var wallet = walletService.Get();
                var IndexVM = MapperService.WalletMap(wallet);
                return View(IndexVM);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        public IActionResult Details(int Id)
        {
            try
            {
                WalletDTO wallet = walletService.Get(Id);
                return View(MapperService.WalletMap(wallet));
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}
