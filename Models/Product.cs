using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF_DatabaseConnection;

namespace WPF_DatabaseConnection.Models
{
    internal class Product
    {

        public int Id { get; set; }

        [StringLength (maximumLength:50)]
        [Required]
        public string Naam { get; set; }


        [Required]
        public string Omschrijving { get; set; }

        [Required]
        [DataType (DataType.Date)]
        public DateTime Gecreeerd { get; set; } = DateTime.Now;

        public Categorie? Categorie { get; set; }
        [ForeignKey ("Categorieen")]
        public int CategorieId { get; set; }


        public List<Prijs>? Prijzen { get; set; }
    }
}
