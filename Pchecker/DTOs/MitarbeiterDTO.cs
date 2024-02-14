using ProjektManager.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjektManager.DTOs
{
    public class MitarbeiterDTO 
    {
        public MitarbeiterDTO()
        {
            
        }

        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public string Nachname { get; set; }

        private Dictionary<string, int> StundenImProjekt { get; set; } // String : ProjektNummern  ---  int : StundenImProjekt

        public static MitarbeiterDTO ToMitarbeiterDTO(Mitarbeiter mitarbeiter)
        {
            return new MitarbeiterDTO();
        }

        public static Mitarbeiter FromMitarbeiterDTO(MitarbeiterDTO mitarbeiterDTO)
        {
            return new Mitarbeiter();

        }

    }
}

