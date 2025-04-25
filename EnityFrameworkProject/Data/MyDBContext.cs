using EnityFrameworkProject.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EnityFrameworkProject.Data;

namespace EnityFrameworkProject.Data
{
    public class MyDBContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\dariu\OneDrive\Dokumenty\baza.mdf;Integrated Security=True;Connect Timeout=30;MultipleActiveResultSets=True");
        }
    }
}
