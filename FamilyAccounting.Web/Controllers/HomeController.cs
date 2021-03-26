﻿using FamilyAccounting.BL.Interfaces;
using FamilyAccounting.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Diagnostics;

namespace FamilyAccounting.Web.Controllers
{

    public class HomeController : Controller
    {
        private readonly IPersonService personsService;

        public HomeController(IPersonService personsService)
        {
            this.personsService = personsService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
