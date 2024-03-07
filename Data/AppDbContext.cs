using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;
using taskApi.Models;

namespace taskApi.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        // Define your DbSet properties for each entity
        public DbSet<Item> Items { get; set; }
        public DbSet<SalesMaster> SalesMasters { get; set; }
        public DbSet<SalesDetails> SalesDetails { get; set; }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
            
        //    modelBuilder.Entity<SalesDetails>()
        // .HasOne<SalesMaster>()  // Specify the navigation property type (SalesMaster)
        // .WithMany(sm => sm.SalesDetails)
        // .HasForeignKey(sd => sd.InvoiceId);
        //}
    }
}

