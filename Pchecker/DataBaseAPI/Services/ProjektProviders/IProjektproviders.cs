using Pchecker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjektManager.DataBaseAPI.ProjektProviders
{
    public interface IProjektproviders
    {
        Task<IEnumerable<Projekt>> GetAllProjekts();

    }
}
