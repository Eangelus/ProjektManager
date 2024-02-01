using Microsoft.EntityFrameworkCore;
using ProjektManager.DataBaseAPI;
using System.Configuration;
using System.Data;
using System.Windows;

namespace Pchecker
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        private const string CONSTRING = "server=localhost; database=projekte; uid=root; Password=123456789";



        protected override void OnStartup(StartupEventArgs e)
        {
            var serverVersion = new MySqlServerVersion(new Version(8, 3, 0));
            DbContextOptions options = new DbContextOptionsBuilder().UseMySql(CONSTRING, serverVersion).Options;
            using (DBContext dbContext = new DBContext(options)) {

                
                dbContext.Database.Migrate();
            }
            base.OnStartup(e);
        }


    }

}
