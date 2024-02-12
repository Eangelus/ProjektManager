using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjektManager.DTOs
{
    public class MitarbeiterDTO : Entity
    {
        public MitarbeiterDTO()
        {
            
        }
        public string Name { get; set; }

        public string Nachname { get; set; }

        private Dictionary<string, int> StundenImProjekt { get; set; } // String : ProjektNummern  ---  int : StundenImProjekt


    }
}

