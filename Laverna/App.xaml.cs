using Laverna.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Laverna
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static MainWindow MainWindow;
        public static LavernaAppEntities1 db = new LavernaAppEntities1();
    }
}
