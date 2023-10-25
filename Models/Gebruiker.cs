using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.RightsManagement;
using System.Text;
using System.Threading.Tasks;

namespace WPF_DatabaseConnection.Models
{
    class Gebruiker
    {
        public int Id { get; set; }
        public string Naam { get; set; }
        public string Gebruikersnaam { get; set; }
        public string Wachtwoord { get; set;  }
    }
}
