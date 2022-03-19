using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using TotallyNotOLX.Data;
using TotallyNotOLX.Models;
using TotallyNotOLX.StaticHelpers;
using TotallyNotOLX.ViewModels.Home;

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

        /// <summary>
        /// Directs us to the home page of the website where we can see the latest products.
        /// </summary>
        public IActionResult Index()
        {
            HomePageViewModel data = new HomePageViewModel();
            data.Products = _db.Products.ToList().OrderByDescending(x => x.DatePosted).Take(15).ToList();
            data.ProductsCount = _db.Products.Count();
            data.Categories = _db.Categories.ToList();
            data.PopularSearches = SearchesLogger.GetTopSearches(5);
            return View(data);
        }

        /// <summary>
        /// Redirects us to the privacy page of our website.
        /// </summary>
        public IActionResult Privacy()
        {
            return View();
        }

        /// <summary>
        /// Redirects us to the About page of our website.
        /// </summary>
        public IActionResult About()
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
