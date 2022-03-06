﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TotallyNotOLX.Data;
using TotallyNotOLX.Models;
using TotallyNotOLX.StaticHelpers;
using TotallyNotOLX.ViewModels.Products;
namespace TotallyNotOLX.Controllers
{
    public class ProductController : Controller
    {
        //Dependency injection
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _db;
        public ProductController(ApplicationDbContext db, UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
            _db = db;
        }
        [HttpGet]
        public IActionResult Index(int? page, string search, string category)
        {
            int pageNumber;
            List<Product> products;
            string searchType = "";
            //searches for products whose name or description contain x if given
            if (string.IsNullOrEmpty(search))
            {
                products = _db.Products.ToList();
            }
            else
            {
                products = _db.Products.Where(
                    product => product.Name.ToLower().Contains(search.ToLower()) ||
                    product.Description.ToLower().Contains(search.ToLower()))
                    .ToList();
                searchType = searchType + search;
            }

            //chooses items of category x if given
            if (!string.IsNullOrEmpty(category)) { 
                products = products.Where(
                    x => x.Category == Categories.CategoryNames.Where(
                        x => x.Value == category)
                    .First().Key)
                    .ToList();
                if (string.IsNullOrEmpty(searchType))
                {
                    searchType = category;
                }
                else
                {
                    searchType = searchType + $" in category {category}";
                }
            }

            //orders items by newest
            products = products.OrderByDescending(x=>x.DatePosted).ToList();

            //chooses how many elements*50 to skip
            if (page.HasValue)
            {
                pageNumber = page.Value;
            }
            else
            {
                pageNumber = 1;
            }

            //gets max 50 items that satisfy the needs given
            //extract 1 to let the pages start from 1, not 0 at the UI
            products = products.Skip((pageNumber-1)*50).Take(50).ToList();

            ProductIndexViewModel data = new ProductIndexViewModel()
            {
                Products = products,
                Page = pageNumber,
                SearchType = searchType
            };
            return View(data);
        }

        [HttpGet]
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Product product)
        {
            product.DatePosted = DateTime.Today.ToShortTimeString();
            product.SellerId = _userManager.GetUserId(User);
            product.Sold = false;
            try
            {
                _db.Products.Add(product);
                _db.SaveChanges();
                return RedirectToAction("index");
            }
            catch
            {
                return View(product);
            }
            
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            Product product = _db.Products.Where(prod => prod.Id == id).FirstOrDefault();
            product.Seller = _db.Users.Where(x=>x.Id==product.SellerId).FirstOrDefault();
            return View(product);
        }
    }
}