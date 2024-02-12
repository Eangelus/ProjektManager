using Microsoft.EntityFrameworkCore;
using ProjektManager.View;
using ProjektManager.ViewModel;
using ProjektManager.DataBaseAPI;
using ProjektManager.Services;
using ProjektManager.Stores;
using System.Windows;
using ProjektManager.Logic;

namespace ProjektManager
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        public static string CONSTRING = "server=localhost; database=projekte; uid=root; Password=123456789";
        public readonly ProjektDBContextFactory _projektDBContextFactory;
        private readonly NaviStore _naviStore;

   
        public App()
        {

            _naviStore = new NaviStore();

            _projektDBContextFactory = new ProjektDBContextFactory(CONSTRING);


            //ExcelConnection.ReadAllExcelFiles();
        }

        protected override void OnStartup(StartupEventArgs e)
        {


            //_naviStore.CurrentViewModel = new ViewModelMainWindow(_naviStore);
            MainWindow = new MainWindow()
            {

                DataContext = new ViewModelMainWindow()
            
            };
            MainWindow.Show();


            var serverVersion = new MySqlServerVersion(new Version(8, 3, 0));
            DbContextOptions options = new DbContextOptionsBuilder().UseMySql(CONSTRING, serverVersion).Options;
    
            using (ProjektDBContext dbContext = _projektDBContextFactory.CreateDbContext()) {

                
                dbContext.Database.Migrate();
            }
            base.OnStartup(e);
        }

        private ViewModelCreateProjekt CreateViewModelCreateProjekt()
        {
            return new ViewModelCreateProjekt(new NaviService (_naviStore, createViewModelProjektList ));
        }

        private ViewModelProjektList createViewModelProjektList()
        {
            return new ViewModelProjektList( new NaviService(  _naviStore, CreateViewModelCreateProjekt));
        }
    }

}
