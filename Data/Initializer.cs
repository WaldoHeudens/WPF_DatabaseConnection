using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF_DatabaseConnection.Models;

namespace WPF_DatabaseConnection.Data
{
    internal static class Initializer
    {
        internal static void DbSetInitializer(MyDbContext context)
        {

            if (!context.Categorieen.Any())
            {
                context.Add(new Categorie { Naam = "-", Omschrijving= "-" });
                context.Add(new Categorie { Naam = "Elektronica", Omschrijving = "Diverse elektronische toestellen" });
                context.Add(new Categorie { Naam = "Huishoudtoestellen", Omschrijving = "Diverse (elektrische) huishoudtoestellen" });
                context.Add(new Categorie { Naam = "IT", Omschrijving = "IT-toestellen" });
                context.SaveChanges();
            }
        }
    }
}
