using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WPF_DatabaseConnection.Models;

namespace WPF_DatabaseConnection
{
    /// <summary>
    /// Interaction logic for Registratie.xaml
    /// </summary>
    public partial class Registratie : Window
    {
        public Registratie()
        {
            InitializeComponent();
        }

        private void btRegistreren_Click(object sender, RoutedEventArgs e)
        {
            if (pwWachtwoord.Password=="" || tbNaam.Text=="" || tbGebruiker.Text=="")
            {
                tbMededeling.Text = "Alle velden moeten ingevuld zijn";
            }
            else if (pwWachtwoord.Password != pwHerhaling.Password)
            {
                tbMededeling.Text = "Geef 2x hetzelfde wachtwoord in";
            }
            else
            {
                Gebruiker? gebruiker = null;
                try
                {
                    gebruiker = App.context.Gebruikers.First(g => g.Gebruikersnaam == tbGebruiker.Text);
                }
                catch
                {
                    gebruiker = new Gebruiker { Naam = tbNaam.Text, Gebruikersnaam = tbGebruiker.Text, Wachtwoord = pwWachtwoord.Password };
                    App.context.Add(gebruiker);
                    App.context.SaveChanges();
                    this.Close();
                }
                tbMededeling.Text = "Deze gebruiker bestaat al.  Geen een andere gebruikersnaam in";
            }
            tbMededeling.Visibility = Visibility.Visible;
        }
    }
}
