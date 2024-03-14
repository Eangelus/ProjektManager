using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjektManager.Models
{
    public class Stundenbuchung
    {
        public int? Id { get; set; }
        public DateTime BuchungsDatum { get; set; }

        public Mitarbeiter? Mitarbeiter { get; set; } = new Mitarbeiter();

        public double Stunden {  get; set; }

        public string Details { get; set; } = String.Empty;

        public Projekt? Projekt { get; set; } = new Projekt();

        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }


        public Stundenbuchung()
        {
            
        }

       public Stundenbuchung(int? Id, DateTime BuchungsDatum, Mitarbeiter? Mitarbeiter,  string Details, Projekt? Projekt, DateTime StartTime, double Stunden)
        {
            this.Id = Id;
            this.BuchungsDatum = BuchungsDatum;
            this.Mitarbeiter = Mitarbeiter;
            
            this.Details = Details;
            this.Projekt = Projekt;
            this.StartTime = StartTime;
            this.Stunden = Stunden;
        }
    }
}
