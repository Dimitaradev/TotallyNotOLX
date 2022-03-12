using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TotallyNotOLX.Data;
using TotallyNotOLX.Models;
using TotallyNotOLX.ViewModels.Administration;

namespace TotallyNotOLX.Controllers
{
    [Authorize(Roles = "Administrator, Moderator")]
    public class AdministrationController : Controller
    {
        //Dependency injection
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _db;
        public AdministrationController(ApplicationDbContext db, UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
            _db = db;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            AdministrationControllerIndexViewModel data = new AdministrationControllerIndexViewModel();

            //adds users to delete
            data.Users = new List<ControlledUser>();
            foreach (var user in _userManager.Users.ToList())
            {
                if (!await _userManager.IsInRoleAsync(user, "Moderator"))
                {
                    if (!await _userManager.IsInRoleAsync(user, "Administrator"))
                    {
                        var userSimplifiedData = new ControlledUser();
                        userSimplifiedData.UserId = user.Id;
                        userSimplifiedData.UserName = user.UserName;
                        userSimplifiedData.IsSelected = false;
                        data.Users.Add(userSimplifiedData);
                    } 
                }
            }
            //adds products to delete
            data.Products = new List<ControlledProduct>();
            foreach (var product in _db.Products.ToList())
            {
                var productSimplifiedData = new ControlledProduct();
                productSimplifiedData.ProductName = product.Name;
                productSimplifiedData.ProductId = product.Id;
                productSimplifiedData.IsSelected = false;
                data.Products.Add(productSimplifiedData);
            }
            return View(data);
        }
        [HttpGet]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> ManageModerators()
        {
            ManageModeratorsViewModel data = new ManageModeratorsViewModel();
            data.Users = new List<ControlledUser>();
            //adds users to control
            foreach (var user in _userManager.Users.ToList())
            {
                var userRole = new ControlledUser
                {
                    UserId = user.Id,
                    UserName = user.UserName
                };

                if (await _userManager.IsInRoleAsync(user, "Moderator"))
                {
                    userRole.IsSelected = true;
                }
                else
                {
                    userRole.IsSelected = false;
                }

                data.Users.Add(userRole);
            }
            return View(data);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteUsers(AdministrationControllerIndexViewModel model)
        {
            if (ModelState.IsValid)
            {
                foreach (var user in model.Users)
                {
                    if (user.IsSelected)
                    {
                        var userToDelete = await _userManager.FindByIdAsync(user.UserId);
                        if (userToDelete != null)
                        {
                            await _userManager.DeleteAsync(userToDelete);
                        }
                    }
                }
                return RedirectToAction("index");
            }
            return View(model);
        }

        [HttpPost]
        public IActionResult DeleteProducts(AdministrationControllerIndexViewModel model)
        {
            if (ModelState.IsValid)
            {
                foreach (var product in model.Products)
                {
                    if (product.IsSelected)
                    {
                        var productToDelete = _db.Products.Where(prod => prod.Id == product.ProductId).FirstOrDefault();
                        if (productToDelete != null)
                        {
                           _db.Products.Remove(productToDelete);
                        }
                    }
                }
                _db.SaveChanges();
                return RedirectToAction("index");
            }
            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> ManageModerators(ManageModeratorsViewModel model)
        {
            for (int i = 0; i < model.Users.Count; i++)
            {
                var user = await _userManager.FindByIdAsync(model.Users[i].UserId);

                IdentityResult result = null;

                if (model.Users[i].IsSelected && !(await _userManager.IsInRoleAsync(user, "Moderator")))
                {
                    result = await _userManager.AddToRoleAsync(user, "Moderator");
                }
                else if (!model.Users[i].IsSelected && await _userManager.IsInRoleAsync(user, "Moderator"))
                {
                    result = await _userManager.RemoveFromRoleAsync(user, "Moderator");
                }
                else
                {
                    continue;
                }

                if (result.Succeeded)
                {
                    if (i < (model.Users.Count - 1))
                        continue;
                    else
                        return RedirectToAction("managemoderators");
                }
            }

            return RedirectToAction("index");
        }
    }
}
