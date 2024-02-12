using ProjektManager.Models;
using ProjektManager.DTOs;
using ProjektManager.DataBaseAPI;

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
                ProjektDTO projektDTO = ProjektDTO.ToProjektDTO(projekt);

                context.Add(projektDTO);
                await context.SaveChangesAsync();
            }
        }

        public Task CreateProjekt(ProjektDTO projekt)
        {
            throw new NotImplementedException();
        }
    }
}
