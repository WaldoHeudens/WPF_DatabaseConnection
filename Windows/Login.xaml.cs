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
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();
        }

        private void btLogin_Click(object sender, RoutedEventArgs e)
        {
            Gebruiker? gebruiker = null;
            try
            {
                gebruiker = App.context.Gebruikers.First(g => g.Gebruikersnaam == tbGebruiker.Text && g.Wachtwoord == pwWachtwoord.Password);
            }
            catch
            {
                tbMededeling.Text = "Foutieve aanmeldingspoging";
                tbMededeling.Visibility = Visibility.Visible;
            }
            if (gebruiker != null)
            {
                App.gebruiker = gebruiker;
                App.mainWindow.miAangemeld.Visibility = Visibility.Visible;
                App.mainWindow.miAangemeld.Header = "Welkom " + gebruiker.Naam;
                App.mainWindow.miAfmelden.Visibility = Visibility.Visible;
                App.mainWindow.miRegistreren.Visibility = Visibility.Collapsed;
                App.mainWindow.miLogin.Visibility = Visibility.Collapsed;
                this.Close();
            }
        }
    }
}
