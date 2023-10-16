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
        List<Product> producten = null;     // De lijst van alle opgehaalde en getoonde producten
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

            // Haal de producten op die horen bij deze categorie
            producten = context.Producten.Where(p => p.CategorieId == selectedCategorie.Id).ToList();
            lbProducten.ItemsSource = producten;
            btAddProduct.Visibility = Visibility.Visible;
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

        private void btAddProduct_Click(object sender, RoutedEventArgs e)
        {
            spProduct.Visibility = Visibility.Visible;
        }

        private void btBewaarProduct_Click(object sender, RoutedEventArgs e)
        {
            context.Producten.Add(new Product
            {   Naam = tbProductNaam.Text,
                Omschrijving = tbProductOmschrijving.Text,
                Categorie = selectedCategorie});
            context.SaveChanges();
            spProduct.Visibility = Visibility.Hidden;
            producten = context.Producten.Where(p => p.CategorieId == selectedCategorie.Id).ToList();
            lbProducten.ItemsSource = producten;
        }
    }
}
