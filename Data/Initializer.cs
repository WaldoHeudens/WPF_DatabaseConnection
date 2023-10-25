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
            Gebruiker dummy = null;
            if (!context.Gebruikers.Any())
            {
                dummy = new Gebruiker {Naam ="-", Gebruikersnaam = "-", Wachtwoord="!**Zever!" };
                context.Add(dummy);
                context.SaveChanges();
            }

            if (dummy == null)
                dummy = context.Gebruikers.First(g => g.Naam == "-");

            if (!context.Categorieen.Any())
            {
                context.Add(new Categorie { Naam = "-", Omschrijving = "-" });
                context.Add(new Categorie { Naam = "Elektronica", Omschrijving = "Diverse elektronische toestellen" });
                context.Add(new Categorie { Naam = "Huishoudtoestellen", Omschrijving = "Diverse (elektrische) huishoudtoestellen" });
                context.Add(new Categorie { Naam = "IT", Omschrijving = "IT-toestellen" });
                context.SaveChanges();
            }

            Categorie dummyCategorie = context.Categorieen.First(c => c.Naam == "-");
            if (!context.Producten.Any())
            {
                context.Add(new Product { Naam = "-", Omschrijving = "-", Categorie = dummyCategorie });
            }

            // Niet nodig:  Het dummy produkt is sowieso toegevoegd aan de context, zij het met een voorlopige Id
            // context.SaveChanges();   

            if (!context.Prijzen.Any())
            {
                // Zorg er wel voor dat alle eventueel al bestaande producten ook een prijs meekrijgen
                foreach (Product product in context.Producten)
                    context.Add(new Prijs { Bedrag = 0, Product = product });
            }

            context.SaveChanges();
        }
    }
}
