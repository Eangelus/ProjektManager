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

        public MitarbeiterDTO(int? id, string name, string vorname, string email,  Dictionary<string, int> stundenImProjekt)
        {
            Id = id;
            Name = name;
            Vorname = vorname;
            StundenImProjekt = stundenImProjekt;
            Email = email;

        }

        [Key]
        public int? Id { get; set; }

        public string Name { get; set; } = String.Empty;

        public string Vorname { get; set; } = String.Empty;

        public string Email { get; set; } = String.Empty;
 

        private Dictionary<string, int> StundenImProjekt { get; set; } // String : ProjektNummern  ---  int : StundenImProjekt

        public static MitarbeiterDTO? ToMitarbeiterDTO(Mitarbeiter? mitarbeiter)
        {
            if (mitarbeiter == null)
            {
                return null;
            }
            return new MitarbeiterDTO(null, mitarbeiter.Name, mitarbeiter.Vorname, mitarbeiter.Email, new Dictionary<string, int>());
        }

        public static Mitarbeiter? FromMitarbeiterDTO(MitarbeiterDTO mitarbeiterDTO)
        {
            if (mitarbeiterDTO == null)
            {
                return null;
            }
            return new Mitarbeiter(mitarbeiterDTO.Name, mitarbeiterDTO.Vorname, new Dictionary<string, int>(), mitarbeiterDTO.Email);

        }

    }
}

