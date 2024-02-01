using Pchecker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjektManager.DataBaseAPI.Services.ProjektCreator
{
    class DatabaseProjektCreator : IProjektCreator
    {
        private readonly ProjektDBContextFactory _dbContextFactory;

        public DatabaseProjektCreator(ProjektDBContextFactory projektDBContextFactory)
        {
            _dbContextFactory = projektDBContextFactory;
        }
        public async Task CreateProjekt(Projekt projekt)
        {
            using(ProjektDBContext context = _dbContextFactory.CreateDbContext())
            {
                Projekt projektDTO = ToProjektDTO(projekt);

                context.Projekte.Add(projektDTO);
                await context.SaveChangesAsync();
            }
        }

        private Projekt ToProjektDTO(Projekt projekt)
        {
            return new Projekt()
            {

            };
        }
    }
}
