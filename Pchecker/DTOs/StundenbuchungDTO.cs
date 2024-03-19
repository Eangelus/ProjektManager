using ProjektManager.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjektManager.DTOs
{
    public class StundenbuchungDTO
    {

        [Key]
        public int? Id { get; set; }

        public MitarbeiterDTO Mitarbeiter { get; set; }

        public DateTime BuchungsDatum { get; set; }

        public int Stunden {  get; set; }

        public int Minuten { get; set; }
        public string Details { get; set; }
        public ProjektDTO Projekt { get; set; }
        public DateTime StartTime { get; set; }

        public StundenbuchungDTO()
        {
            
        }

        public static StundenbuchungDTO ToStundenbuchungDTO(Stundenbuchung stundenbuchung)
        {

            return new StundenbuchungDTO(stundenbuchung.Id, stundenbuchung.BuchungsDatum, MitarbeiterDTO.ToMitarbeiterDTO(stundenbuchung.Mitarbeiter), stundenbuchung.Details, ProjektDTO.ToProjektDTO(stundenbuchung.Projekt), stundenbuchung.StartTime, stundenbuchung.Stunden, stundenbuchung.Minuten);
        }

        public static Stundenbuchung FromStundenbuchungDTO(StundenbuchungDTO stundenbuchungDTO)
        {

            return new Stundenbuchung(stundenbuchungDTO.Id, stundenbuchungDTO.BuchungsDatum, MitarbeiterDTO.FromMitarbeiterDTO(stundenbuchungDTO.Mitarbeiter),  stundenbuchungDTO.Details, ProjektDTO.FromProjektDTO(stundenbuchungDTO.Projekt), stundenbuchungDTO.StartTime, stundenbuchungDTO.Stunden , stundenbuchungDTO.Minuten);

        }



        public StundenbuchungDTO(int? Id, DateTime TagDerBuchung, MitarbeiterDTO Mitarbeiter, string Details, ProjektDTO Projekt, DateTime StartTime, int Stunden , int minuten)
        {
            this.Id = Id;
            this.BuchungsDatum = TagDerBuchung;
            this.Mitarbeiter = Mitarbeiter;
            this.Details = Details;
            this.Projekt = Projekt;
            this.StartTime = StartTime;
            this.Stunden = Stunden;
            this.Minuten = minuten;
        }
    }
}
