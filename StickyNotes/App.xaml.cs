using ControlzEx.Theming;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace StickyNotes
{
    /// <summary>
    /// Lógica de interacción para App.xaml
    /// </summary>
    public partial class App : Application
    {
        public Configuration Config { get; set; }

        private void ApplicationStartup(object sender, StartupEventArgs e)
        {
            // TODO: Load settings here
            Config = Configuration.ReadConfig();

            MainWindow mainWindow = new();
            mainWindow.Show();
        }
    }
}
