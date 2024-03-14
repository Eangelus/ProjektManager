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

        public double Stunden
        {
            get
            {
                return (this.EndTime.Ticks - this.StartTime.Ticks) / 1000 * 60 * 60;
            }
        }
        public string Details { get; set; }
        public ProjektDTO Projekt { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }



        public static StundenbuchungDTO? ToStundenbuchungDTO(Stundenbuchung? stundenbuchung)
        {
            if (stundenbuchung == null)
            {
                return null;
            }
            return new StundenbuchungDTO(stundenbuchung.Id, stundenbuchung.BuchungsDatum, MitarbeiterDTO.ToMitarbeiterDTO(stundenbuchung.Mitarbeiter), stundenbuchung.Details, ProjektDTO.ToProjektDTO(stundenbuchung.Projekt), stundenbuchung.StartTime, stundenbuchung.EndTime);
        }

        public static Stundenbuchung? FromStundenbuchungDTO(StundenbuchungDTO stundenbuchungDTO)
        {
            if (stundenbuchungDTO == null)
            {
                return null;
            }
            return new Stundenbuchung(null, stundenbuchungDTO.BuchungsDatum, MitarbeiterDTO.FromMitarbeiterDTO(stundenbuchungDTO.Mitarbeiter),  stundenbuchungDTO.Details, ProjektDTO.FromProjektDTO(stundenbuchungDTO.Projekt), stundenbuchungDTO.StartTime, stundenbuchungDTO.EndTime);

        }

        public StundenbuchungDTO()
        {

        }

        public StundenbuchungDTO(int? Id, DateTime TagDerBuchung, MitarbeiterDTO Mitarbeiter, string Details, ProjektDTO Projekt, DateTime StartTime, DateTime EndTime)
        {
            this.Id = Id;
            this.BuchungsDatum = TagDerBuchung;
            this.Mitarbeiter = Mitarbeiter;
            this.Details = Details;
            this.Projekt = Projekt;
            this.StartTime = StartTime;
            this.EndTime = EndTime;
        }
    }
}
