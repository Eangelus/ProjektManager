using ProjektManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProjektManager.DTOs;

namespace ProjektManager.DataBaseAPI.ProjektProviders
{
    class DatabaseProjektProviders : IProjektproviders
    {
        private readonly ProjektDBContextFactory _dbContextFactory;

        public DatabaseProjektProviders(ProjektDBContextFactory projektDBContextFactory)
        {
            _dbContextFactory = projektDBContextFactory;
        }

        public async Task<IEnumerable<ProjektDTO>> GetAllProjekts()
        {
            using (ProjektDBContext context = _dbContextFactory.CreateDbContext())
            {
                IEnumerable<ProjektDTO> projekteDTO = (IEnumerable<ProjektDTO>)await GetAllProjekts();

                return projekteDTO.Select(p => new ProjektDTO());

            }
        }

        Task<IEnumerable<Projekt>> IProjektproviders.GetAllProjekts()
        {
            throw new NotImplementedException();
        }
    }
}
