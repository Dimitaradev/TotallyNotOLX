﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using TotallyNotOLX.Data;
using TotallyNotOLX.Models;

namespace TotallyNotOLX.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _db;

        public HomeController(ApplicationDbContext db, ILogger<HomeController> logger)
        {
            _logger = logger;
            _db = db;
        }

        public IActionResult Index()
        {
            
            List<Product> latestAds = _db.Products.ToList();
            return View(latestAds.OrderByDescending(x => x.DatePosted).Take(15));
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult About()
        {
            return View();
        }
        public IActionResult Developers()
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
