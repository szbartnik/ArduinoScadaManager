using System;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.IO;
using System.Windows;
using ArduinoScadaManager.Gui.ViewModels;
using ArduinoScadaManager.Gui.Views;

namespace ArduinoScadaManager.Gui
{
    public partial class App : Application
    {
        private Bootstrapper _bootstrapper;

        protected override void OnStartup(StartupEventArgs e)
        {
            LoadBootstrapper();
            base.OnStartup(e);
        }

        private void LoadBootstrapper()
        {
            try
            {
                Console.WriteLine("Initialize bootstrapper.");
                _bootstrapper = new Bootstrapper();
                Console.WriteLine("Bootstrapper initialized.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Fatal error during bootstrapper initialization.");
                MessageBox.Show(ex.ToString());
                Environment.Exit(1);
            }
        }
    }

    public class Bootstrapper
    {
        private const string ModulesDirectory = "Modules";

        public AggregateCatalog AggregateCatalog { get; private set; }

        [Export]
        public CompositionContainer Container { get; private set; }

        public Bootstrapper()
        {
            Console.WriteLine("Loading MEF libraries.");
            ComposeContainer();

            Console.WriteLine("Initialize MainWindow view.");
            InitializeMainWindow();
        }

        private void InitializeMainWindow()
        {
            new MainWindowView(new MainWindowViewModel(Container)).Show();
        }

        private void ComposeContainer()
        {
            AggregateCatalog = new AggregateCatalog();
            AggregateCatalog.Catalogs.Add(new AssemblyCatalog(typeof(Bootstrapper).Assembly));

            if (Directory.Exists(ModulesDirectory))
                AggregateCatalog.Catalogs.Add(new DirectoryCatalog(ModulesDirectory));

            Container = new CompositionContainer(AggregateCatalog);
        }
    }
}
