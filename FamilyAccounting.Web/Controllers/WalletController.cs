using AutoMapper;
using FamilyAccounting.BL.DTO;
using FamilyAccounting.BL.Interfaces;
using FamilyAccounting.Web.Models;
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

        public IActionResult Details(int Id)
        {
            try
            {
                WalletDTO wallet = walletService.Get(Id);
                var config = new MapperConfiguration(cfg => cfg.CreateMap<WalletDTO, WalletViewModel>());
                var mapper = new Mapper(config);
                var personVM = mapper.Map<WalletViewModel>(wallet);
                return View(personVM);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}
