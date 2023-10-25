using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using WPF_DatabaseConnection.Models;

namespace WPF_DatabaseConnection
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    /// 


    public partial class App : Application
    {
        internal static MyDbContext context = null;
        internal static MainWindow mainWindow = null;
        internal static Gebruiker gebruiker = null;
    }
}
