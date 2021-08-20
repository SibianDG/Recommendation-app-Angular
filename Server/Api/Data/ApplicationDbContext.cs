using System;
using Api.Data.Mappers;
using Api.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Api.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {

        public DbSet<Item> Items { get; set; }
        public DbSet<Recommendation> Recommendations { get; set; }
        public DbSet<Customer> Customers { get; set; }
        
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new ItemConfiguration());
            modelBuilder.ApplyConfiguration(new RecommendationConfiguration());
            modelBuilder.ApplyConfiguration(new CustomerConfiguration());
            
            modelBuilder.Entity<Book>().HasBaseType<Item>();
            modelBuilder.Entity<Movie>().HasBaseType<Item>();
            modelBuilder.Entity<Serie>().HasBaseType<Item>();
        }
    }
}