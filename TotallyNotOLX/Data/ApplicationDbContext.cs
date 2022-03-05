using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using TotallyNotOLX.Models;

namespace TotallyNotOLX.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().HasMany(c => c.Images).WithOne(e => e.Product);
            modelBuilder.Entity<ApplicationUser>().HasMany(c => c.Products).WithOne(e => e.Seller);

            modelBuilder.Entity<ApplicationUsers_SavedProducts>().HasKey(sp => new { sp.ApplicationUserId, sp.ProductID });
            modelBuilder.Entity<ApplicationUsers_SavedProducts>().HasOne(user => user.ApplicationUser).WithMany(userSaved => userSaved.Saved).HasForeignKey(userId => userId.ApplicationUserId).OnDelete(DeleteBehavior.Restrict); 
            modelBuilder.Entity<ApplicationUsers_SavedProducts>().HasOne(product => product.Product).WithMany(savedBy => savedBy.SavedBy).HasForeignKey(productId => productId.ProductID).OnDelete(DeleteBehavior.Restrict); 

            base.OnModelCreating(modelBuilder);
        }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductImage> ProductsImages { get; set; }
        public DbSet<ApplicationUsers_SavedProducts> ApplicationUsers_SavedProducts { get; set; }
    }
}
