using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF_DatabaseConnection.Models;

namespace WPF_DatabaseConnection
{
    internal class MyDbContext:DbContext
    {
        public DbSet<Categorie> Categorieen { get; set; }
        public DbSet<Product> Producten { get; set; }
        public DbSet<Prijs> Prijzen { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Database=(localDb)\\WPF_Testen;Trusted_Connection=true;TrustServerCertificate=true;MultipleActiveResultSets=true");
        }
    }
}
