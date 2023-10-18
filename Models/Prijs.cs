using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_DatabaseConnection.Models
{
    internal class Prijs
    {
        public int Id { get; set; }

        [ForeignKey ("Product")]
        public int ProductId { get; set; }

        public double Bedrag { get; set; }

        [DataType(DataType.Date)]
        public DateTime Vanaf { get; set; } = DateTime.Now;

        public Product? Product { get; set; }
    }
}
