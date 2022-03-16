using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TotallyNotOLX.Data;
using TotallyNotOLX.Models;
using TotallyNotOLX.ViewModels.Error;
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
        /// <summary>
        /// The Index method descripes what will be done with our index page of the website.
        /// </summary>
        /// <param name="category">the name of the category</param>
        /// <param name="page">The number of the page your are on</param>
        /// <param name="search">The input from the search box</param>
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
            if (!string.IsNullOrEmpty(category) && category != "all") {
                products = products
                    .Where(product => product.CategoryId == _db.Categories
                        .Where(cate => cate.Name == category)
                    .FirstOrDefault().Id)
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

            //adds categories for the searchbar
            List<string> categories = new List<string>();
            foreach (var categoryName in _db.Categories.ToList())
            {
                categories.Add(categoryName.Name);
            }

            ProductIndexViewModel data = new ProductIndexViewModel()
            {
                Products = products,
                Page = pageNumber,
                SearchType = searchType,
                Categories = categories,
                CategoryChosen = category,
                Searched = search
            };
            return View(data);
        }

        [HttpGet]
        [Authorize]
        ///<summary>
        ///Calls the Create method with ViewModel.
        /// </summary>
        public IActionResult Create()
        {
            CreateProductViewModel createData = new CreateProductViewModel();
            createData.Categories = _db.Categories.ToList();
            return View(createData);
        }
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        ///<summary>
        ///Uses the data from the ViewModel to create new product.
        ///</summary>
        public IActionResult Create(CreateProductViewModel productCreateData)
        {
            var product = productCreateData.NewProduct;
            product.DatePosted = DateTime.UtcNow.ToString("dd-MM-yyyy");
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
                return View(productCreateData);
            }
            
        }


        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        ///<summary>
        ///This method is used to delete product with specific id
        /// </summary>
        /// <param name="id">id of the product that will be deleted</param>
        public IActionResult Delete(int? id)
        {
            var obj = _db.Products.Find(id);
            if (obj == null)
            {
                return NotFound();
            }

            _db.Products.Remove(obj);
            _db.SaveChanges();
            return RedirectToAction("Index");

        }


        [HttpGet]
        ///<summary>
        ///This method returns every information for product with cpecific id. 
        /// </summary>
        /// <param name="id">The id of the product for which we will display information</param>
        public IActionResult Details(int? id)
        {
            if (!id.HasValue)
            {
                NotFoundErrorViewModel errorData = new NotFoundErrorViewModel()
                {
                    ErrorMessage = "Enter valid item id",
                    ReturnAction = "index",
                    ReturnController = "product"
                };
                return RedirectToAction("notfound", "error", errorData);
            }
            Product product = _db.Products.Where(prod => prod.Id == id.Value).FirstOrDefault();
            if (product==null)
            {
                NotFoundErrorViewModel errorData = new NotFoundErrorViewModel()
                {
                    ErrorMessage = $"Item with id {id.Value} could not be found",
                    ReturnAction = "index",
                    ReturnController = "product"
                };
                return RedirectToAction("notfound", "error", errorData);
            }
            product.Seller = _db.Users.Where(x=>x.Id==product.SellerId).FirstOrDefault();
            product.SavedByUser = false;
            if (User.Identity.IsAuthenticated)
            {
                if (_db.ApplicationUsers_SavedProducts
                    .Where(x=>x.ApplicationUserId==_userManager.GetUserId(User)&&x.ProductID==id.Value)
                    .Any())
                {
                    product.SavedByUser = true;
                }
                if(_userManager.GetUserId(User) == product.Seller.Id)
                {
                    product.CreatedByUser = true;
                }
            }

            return View(product);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        ///<summary>
        ///This method add a product with specific id to the leist of products saved by us.
        /// </summary>
        /// <param name="id">The id of the product which we will save</param>
        public IActionResult AddProductToSaved(int id)
        {
            Product product = _db.Products.Where(prod => prod.Id == id).FirstOrDefault();
            ApplicationUser user = _db.Users.Where(x => x.Id == product.SellerId).FirstOrDefault();
            ApplicationUsers_SavedProducts connection = new ApplicationUsers_SavedProducts() {
                ApplicationUser = user,
                Product = product
            };
            _db.ApplicationUsers_SavedProducts.Add(connection);
            _db.SaveChanges();
            return RedirectToAction("details", new {id=id });
        }
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        ///<summary>
        ///This method will remove certain prduct from our list of saved products.
        /// </summary>
        /// <param name="id"> The id of the product which we will removed from saved</param>
        public IActionResult RemoveProductFromSaved(int id)
        {
            Product product = _db.Products.Where(prod => prod.Id == id).FirstOrDefault();
            ApplicationUser user = _db.Users.Where(x => x.Id == product.SellerId).FirstOrDefault();
            ApplicationUsers_SavedProducts connection = new ApplicationUsers_SavedProducts()
            {
                ApplicationUser = user,
                Product = product
            };
            _db.ApplicationUsers_SavedProducts.Remove(connection);
            _db.SaveChanges();
            return RedirectToAction("details", new { id = id });
        }
        [HttpGet]
        [Authorize]
        ///<summary>
        ///This method will return the list of products that we have saved.
        /// </summary>
        public IActionResult Saved()
        {
            var userSaved = _db.ApplicationUsers_SavedProducts
            .Where(user_saved => user_saved.ApplicationUserId == _userManager
            .FindByNameAsync(User.Identity.Name).Result.Id)
            .Select(pair => pair.Product)
            .ToList();
            return View(userSaved);
        }
    }
}
