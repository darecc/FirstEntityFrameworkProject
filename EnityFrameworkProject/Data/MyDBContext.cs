using EnityFrameworkProject.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EnityFrameworkProject.Data;
using System.Runtime.CompilerServices;

/// <summary>Projekt prostej bazy Produkt-Klient-Zamówienie</summary>
namespace EnityFrameworkProject.Data
{
    /// <summary>
    /// Klasa kontekstu bazodanowego sklepu (dziedziczy po DbContext)
    /// </summary>
    public class MyDBContext : DbContext /// <summary>dbcontext</summary>
    {
        /// <summary>
        /// Zbiór (zawartość tabeli produktów)
        /// </summary>
        public DbSet<Product> Products { get; set; }
        /// <summary>
        /// Zbiór (zawartość tabeli zamówień (Orders)
        /// </summary>
        public DbSet<Order> Orders { get; set; }
        /// <summary>
        /// Zbiór (zawartość tabeli klientów (Customers)
        /// </summary>
        public DbSet<Customer> Customers { get; set; }
        /// <summary>
        /// Zbiór (zawartość tabeli szczegółów zamówień (DetailOrders)
        /// </summary>
        public DbSet<OrderDetail> OrderDetails { get; set; }
        /// <summary>
        /// Metoda konfigurująca parametry pliku z bazą danych MS SQL
        /// </summary>
        /// <param name="optionsBuilder"></param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\dariu\OneDrive\Dokumenty\baza.mdf;Integrated Security=True;Connect Timeout=30;MultipleActiveResultSets=True");
        }
    }
}
