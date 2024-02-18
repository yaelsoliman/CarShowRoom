using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SparkAuto.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SparkAuto.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<ServiceType> serviceTypes { get; set; }
        public DbSet<ApplicationUser> ApplicationUser { get; set; }
        public DbSet<Car> Cars { get; set; }

        public DbSet<Service> Services { get; set; }
        public DbSet<ServiceDetails> ServiceDetails { get; set; }
        public DbSet<ServiceShoppingCart> ServiceShoppingCarts { get; set; }

    }
}
