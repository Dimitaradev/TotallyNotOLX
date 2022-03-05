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
            base.OnModelCreating(modelBuilder);
        }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductImage> ProductsImages { get; set; }
    }
}
