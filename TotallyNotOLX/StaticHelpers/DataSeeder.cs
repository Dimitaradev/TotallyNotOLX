using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TotallyNotOLX.Data;
using TotallyNotOLX.Models;

namespace TotallyNotOLX.StaticHelpers
{
    public static class DataSeeder
    {
        //creates Moderator and Administrator roles if they dont exist in the database yet
        public static void SeedRoles(RoleManager<IdentityRole> roleManager)
        {
            if (!roleManager.RoleExistsAsync("Administrator").Result)
            {
                IdentityRole role = new IdentityRole();
                role.Name = "Administrator";
                IdentityResult roleResult = roleManager.CreateAsync(role).Result;
            }
            if (!roleManager.RoleExistsAsync("Moderator").Result)
            {
                IdentityRole role = new IdentityRole();
                role.Name = "Moderator";
                IdentityResult roleResult = roleManager.CreateAsync(role).Result;
            }
        }

        //creates the categories given if they dont exist in the database yet
        private static List<string> Categories = new List<string>()
        {
            "Cars", "Home", "Office", "Sport", "Gadgets", "Technologies", "Books", "Clothes", "Other"
        };
        public static void SeedCategories(ApplicationDbContext db)
        {
            foreach (var category in Categories)
            {
                if (!db.Categories.Where(x=>x.Name == category).Any())
                {
                    Category newCategory = new Category();
                    newCategory.Name = category;
                    db.Categories.Add(newCategory);
                }
            }
            db.SaveChanges();
        }
    }
}
