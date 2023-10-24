using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_DatabaseConnection.Models
{
    internal class Categorie
    {
        public int Id { get; set; }
        public string Naam { get; set; } = "";
        public string Omschrijving { get; set; } = "";

        
        public List<Product>? Producten { get; set; }
    }
}
