using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
    }
}
