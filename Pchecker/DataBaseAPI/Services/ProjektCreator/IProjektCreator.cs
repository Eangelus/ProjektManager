using ProjektManager.Models;
using ProjektManager.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjektManager.DataBaseAPI.Services.ProjektCreator
{
    public interface IProjektCreator
    {
        Task CreateProjekt(ProjektDTO projekt);
    }
}
