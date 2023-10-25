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
using WPF_DatabaseConnection.Migrations;
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
        Product selectedProduct = null;

        MyDbContext context = new MyDbContext();

        public MainWindow()
        {
            Initializer.DbSetInitializer(context);

            InitializeComponent();

            categorieen = context.Categorieen.Where(c => c.Naam != "-").ToList();
            lbCategorieen.ItemsSource = categorieen;

            App.mainWindow = this;
            App.context = context;
        }

        private void lbCategorieen_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ClearMessage();
            selectedCategorie = categorieen[lbCategorieen.SelectedIndex];
            tbCategorie.Text = selectedCategorie.Naam;

            // Haal de producten op die horen bij deze categorie
            producten = context.Producten.Where(p => p.CategorieId == selectedCategorie.Id).ToList();
            lbProducten.ItemsSource = producten;
            btAddProduct.Visibility = Visibility.Visible;
        }

        private void tbCategorie_LostFocus(object sender, RoutedEventArgs e)
        {
            ClearMessage();
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
            ClearMessage();
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
            // Maak een nieuw product aan.  Zorg er voor dat er geen bestaand product "geselecteerd" is

            selectedProduct = null;
            spProduct.Visibility = Visibility.Visible;
            ClearMessage();
        }

        private void btBewaarProduct_Click(object sender, RoutedEventArgs e)
        {
            /* Deze event handler wordt zowel gebruikt voor het toevoegen van een nieuw product, 
             * als voor het bewaren van de wijzigingen aan een bestaand product */

            ClearMessage();
            try
            {
                double bedrag = Convert.ToDouble(tbPrijs.Text);

                if (selectedProduct == null)        // voeg product toe
                {
                    Product product = new Product
                    {
                        Naam = tbProductNaam.Text,
                        Omschrijving = tbProductOmschrijving.Text,
                        Categorie = selectedCategorie
                    };
                    context.Producten.Add(product);
                    context.Prijzen.Add(new Prijs { Bedrag = bedrag, Product = product });
                }
                else    // wijzig bestaand product
                {
                    selectedProduct.Naam = tbProductNaam.Text;
                    selectedProduct.Omschrijving = tbProductOmschrijving.Text;
                    context.Update(selectedProduct);
                    if (context.Prijzen.OrderBy(prijs => prijs.Vanaf).Last(prijs => prijs.ProductId == selectedProduct.Id).Bedrag != bedrag)
                    {
                        context.Prijzen.Add(new Prijs { Bedrag = bedrag, Product = selectedProduct });
                    }
                }
                context.SaveChanges();
                spProduct.Visibility = Visibility.Hidden;
                producten = context.Producten.Where(p => p.CategorieId == selectedCategorie.Id).ToList();
                lbProducten.ItemsSource = producten;
            }
            catch
            {
                ShowMessage("Er is een probleem met je prijs.  Los dat op !!!");
            }
        }

        public void lbProducten_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // onthoud het gekozen product in "selectedProduct" en update de productvelden in de Xaml

            selectedProduct = (Product)lbProducten.SelectedItem;
            spProduct.Visibility = Visibility.Visible;
            tbProductNaam.Text = selectedProduct.Naam;
            tbProductOmschrijving.Text = selectedProduct.Omschrijving;
            tbPrijs.Text = context.Prijzen.OrderBy(prijs => prijs.Vanaf).Last(prijs => prijs.ProductId == selectedProduct.Id).Bedrag.ToString();
            ClearMessage();
        }

        public void tbPrijs_MouseEnter(object sender, MouseEventArgs e)
        {
            ShowMessage("Geef een getal in", false);
        }

        public void VerplichtVeld(object sender, MouseEventArgs e)
        {
            ShowMessage("Dit is een verplicht veld", false);
        }

        public void btAddProduct_MouseEnter(object sender, MouseEventArgs e)
        {
            ShowMessage("Voeg een nieuw product toe aan de geselecteerde productcategorie", false);
        }

        public void btBewaarProduct_MouseEnter(object sender, MouseEventArgs e)
        {
            ShowMessage("Bewaar alle wijzigingen die je hebt aangebracht", false);
        }

        public void DeleteMessage(object sender, MouseEventArgs e)
        {
            ClearMessage();
        }

        private void ClearMessage()
        {
            // toon een foutboodschap

            tbMessage.Visibility = Visibility.Hidden;
        }

        private void ShowMessage(string message, bool serious = true)
        {
            tbMessage.Text = message;
            tbMessage.Background = serious ? new SolidColorBrush(Colors.Red) : new SolidColorBrush(Colors.Green);
            tbMessage.Height = 30;
            tbMessage.FontWeight = FontWeights.Bold;
            tbMessage.FontSize = 13;
            tbMessage.VerticalAlignment = VerticalAlignment.Center;
            tbMessage.Visibility = Visibility.Visible;
        }

        private void btShowCase_Click(object sender, RoutedEventArgs e)
        {
            if (lbShowCase.Visibility == Visibility.Visible)
            {
                lbShowCase.Visibility = Visibility.Hidden;
                lbLinq.Visibility = Visibility.Hidden;
            }
            else
            {

                DateTime startTijd = DateTime.Now.AddMonths(-1);
                List<Categorie> categorien = context.Categorieen
                                                    .Where(c => c.Naam != "-")
                                                    .Include(c => c.Producten)
                                                    .ThenInclude(p => p.Prijzen
                                                        .Where(prijs => prijs.Vanaf > startTijd))
                                                    .ToList();
                List<Categorie> categorien2 = categorien.Where(c => c.Producten.Any()).ToList();

                lbShowCase.ItemsSource = categorien2;
                lbShowCase.Visibility = Visibility.Visible;


                // Met Linq om alleen de gebruikte velden op the halen

                var categorieQuerry = from categorie in context.Categorieen
                                      from product in context.Producten
                                      from prijs in context.Prijzen
                                      where categorie.Naam != "-"
                                         && product.Naam != "-"
                                         && product.CategorieId == categorie.Id
                                         && prijs.ProductId == product.Id
                                         && prijs.Vanaf > startTijd
                                      select new
                                      {
                                          Naam = categorie.Naam,
                                          ProductNaam = product.Naam,
                                          Bedrag = prijs.Bedrag,
                                          Vanaf = prijs.Vanaf
                                      };
                lbLinq.ItemsSource = categorieQuerry.ToList();
                lbLinq.Visibility = Visibility.Visible;

            }
        }

 
        private void TabItem_GotFocus(object sender, RoutedEventArgs e)
        {
            DateTime startTijd = DateTime.Now.AddMonths(-1);
            List<Categorie> categorien = context.Categorieen
                                                .Where(c => c.Naam != "-")
                                                .Include(c => c.Producten)
                                                .ThenInclude(p => p.Prijzen
                                                    .Where(prijs => prijs.Vanaf > startTijd))
                                                .ToList();
            List<Categorie> categorien2 = categorien.Where(c => c.Producten.Any()).ToList();

            lbShowCase.ItemsSource = categorien2;

        }

        private void TabItem_GotFocus_1(object sender, RoutedEventArgs e)
        {
            // Met Linq om alleen de gebruikte velden op the halen

            DateTime startTijd = DateTime.Now.AddMonths(-1);
            var categorieQuerry = from categorie in context.Categorieen
                                  from product in context.Producten
                                  from prijs in context.Prijzen
                                  where categorie.Naam != "-"
                                     && product.Naam != "-"
                                     && product.CategorieId == categorie.Id
                                     && prijs.ProductId == product.Id
                                     && prijs.Vanaf > startTijd
                                  select new
                                  {
                                      Naam = categorie.Naam,
                                      ProductNaam = product.Naam,
                                      Bedrag = prijs.Bedrag,
                                      Vanaf = prijs.Vanaf
                                  };
            lbLinq.ItemsSource = categorieQuerry.ToList();
            lbLinq.Visibility = Visibility.Visible;

        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            new Registratie().Show();
        }

        private void miAfmelden_Click(object sender, RoutedEventArgs e)
        {
            App.gebruiker = null;
            miAangemeld.Visibility = Visibility.Collapsed;
            miAfmelden.Visibility = Visibility.Collapsed;
            miRegistreren.Visibility = Visibility.Visible;
            miLogin.Visibility = Visibility.Visible;
        }

        private void miLogin_Click(object sender, RoutedEventArgs e)
        {
            new Login().Show();
        }
    }
}
