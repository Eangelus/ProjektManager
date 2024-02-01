using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace ProjektManager.DataBaseAPI
{
    public class ProjektDesignDbFactory :IDesignTimeDbContextFactory<ProjektDBContext>
    {
        public ProjektDBContext CreateDbContext(string[] args) {

            var serverVersion = new MySqlServerVersion(new Version(8, 3, 0));
            DbContextOptions options = new DbContextOptionsBuilder().UseMySql("server=localhost; database=projekte; uid=root; Password=123456789", serverVersion).Options;

            return new ProjektDBContext(options); 
        }


    }
}
