using Microsoft.EntityFrameworkCore;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using WPF_DatabaseConnection.Data;
using WPF_DatabaseConnection.Models;

namespace WPF_DatabaseConnection
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Categorie> categorieen = null;
        Categorie selectedCategorie = null;
        MyDbContext context = new MyDbContext();

        public MainWindow()
        {
            Initializer.DbSetInitializer(context);

            InitializeComponent();

            categorieen = context.Categorieen.Where(c => c.Naam != "-").ToList();
            lbCategorieen.ItemsSource = categorieen;
        }

        private void lbCategorieen_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedCategorie = categorieen[lbCategorieen.SelectedIndex];
            tbCategorie.Text = selectedCategorie.Naam;
        }

        private void tbCategorie_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                Categorie cat = context.Categorieen.First(c => c.Naam == tbCategorie.Text);
            }
            catch
            {
                spOmschrijving.Visibility = Visibility.Visible;
            }
        }

        private void tbOmschrijving_LostFocus(object sender, RoutedEventArgs e)
        {
            if (tbOmschrijving.Text != "")
            {
                spOmschrijving.Visibility = Visibility.Hidden;
                context.Categorieen.Add(new Categorie { Naam = tbCategorie.Text, Omschrijving = tbOmschrijving.Text });
                context.SaveChanges();
                categorieen = context.Categorieen.Where(c => c.Naam != "-").ToList();
                lbCategorieen.ItemsSource = categorieen;
            }
        }
    }
}
