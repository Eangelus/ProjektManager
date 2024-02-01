using Microsoft.EntityFrameworkCore;

namespace ProjektManager.DataBaseAPI
{
    class ProjektDBContextFactory
    {
        private readonly string _connString;

        public ProjektDBContextFactory(string connectionString)
        {
            _connString = connectionString;
        }

        public ProjektDBContext CreateDbContext()
        {

            var serverVersion = new MySqlServerVersion(new Version(8, 3, 0));
            DbContextOptions options = new DbContextOptionsBuilder().UseMySql(_connString, serverVersion).Options;

            return new ProjektDBContext(options);
        }

    }
}
