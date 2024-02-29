using Microsoft.EntityFrameworkCore;
using ProjektManager.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjektManager.DTOs
{
    public class AbteilungDTO 
    {
        public AbteilungDTO()
        {
            
        }

        public AbteilungDTO(string abBezeichung, string abLeiter, List<MitarbeiterDTO> mitarbeiter)
        {
            AbBezeichung = abBezeichung;
            AbLeiter = abLeiter;
            Mitarbeiter = mitarbeiter;
        }

        [Key] 
        public string AbBezeichung { set; get; }
        public string AbLeiter { set; get; }

        public List<MitarbeiterDTO> Mitarbeiter { get; set; }

        public static AbteilungDTO ToAbteilungDTO(Abteilung abteilung)
        {
            if(abteilung == null)
            {
                return null;
            }
            List<MitarbeiterDTO> mitarbeiter = new List<MitarbeiterDTO>();
            if (abteilung.Mitarbeiter != null)
            {
                foreach (var m in abteilung.Mitarbeiter)
                {
                    mitarbeiter.Add(MitarbeiterDTO.ToMitarbeiterDTO(m));
                }
            }

            return new AbteilungDTO(abteilung.AbBezeichung, abteilung.AbLeiter, mitarbeiter);
        }

        public static Abteilung FromAbteilungDTO(AbteilungDTO abteilungDTO)
        {
            List<Mitarbeiter> mitarbeiter = new List<Mitarbeiter>();
            if (abteilungDTO.Mitarbeiter != null)
            {
                foreach (var m in abteilungDTO.Mitarbeiter)
                {
                    mitarbeiter.Add(MitarbeiterDTO.FromMitarbeiterDTO(m));
                }
            }
            List<Projekt> projekte = new List<Projekt>();
            // Erzeugt eine zirkulare Abhängigkeit
            //if (abteilungDTO.Projekte != null)
            //{
            //    foreach (var p in abteilungDTO.Projekte)
            //    {
            //        projekte.Add(ProjektDTO.FromProjektDTO(p));
            //    }
            //}
            List<Problem> probleme = new List<Problem>();   
            return new Abteilung(abteilungDTO.AbBezeichung, abteilungDTO.AbLeiter, mitarbeiter);

        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            return ((AbteilungDTO)obj).AbBezeichung.Equals(AbBezeichung);
        }

        public override int GetHashCode()
        {
            return AbBezeichung.GetHashCode();

        }

    }
}
