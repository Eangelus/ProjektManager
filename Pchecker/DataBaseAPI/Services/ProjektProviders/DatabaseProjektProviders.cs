using Pchecker.Models;
using Pchecker;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ProjektManager.DataBaseAPI.ProjektProviders
{
    class DatabaseProjektProviders : IProjektproviders
    {
        private readonly ProjektDBContextFactory _dbContextFactory;

        public DatabaseProjektProviders(ProjektDBContextFactory projektDBContextFactory)
        {
            _dbContextFactory = projektDBContextFactory;
        }

        public async Task<IEnumerable<Projekt>> GetAllProjekts()
        {
            using (ProjektDBContext context = _dbContextFactory.CreateDbContext())
            {
                IEnumerable<Projekt> projekteDTO = await context.Projekte.ToListAsync();

                return projekteDTO.Select(p => new Projekt());

            }
        }
    }
}
