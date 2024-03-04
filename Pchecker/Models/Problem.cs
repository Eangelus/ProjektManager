
using ProjektManager.DTOs;
using ProjektManager.Models;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;


namespace ProjektManager.Models
{
    public class Problem

    {
        public int? Id { get; set; }
        public string Bezug { get; set; }
        public DateTime AuftrittsDatum { get; set; }
        [NotMapped]
        public int KW
        {
            get
            {
                if(AuftrittsDatum == null)
                {
                    return 0;
                }
                return CultureInfo.InvariantCulture.Calendar.GetWeekOfYear(AuftrittsDatum, CalendarWeekRule.FirstDay, DayOfWeek.Monday);
            }
        }
        public string Abteilung { get; set; }
        public Mitarbeiter Verantwortlicher { get; set; }
        public string Initiator { get; set; }
        public string Kategorie { get; set; }
        public string Thema { get; set; }
        public string Maßnahme { get; set; }

        public string Bewertung { get; set; }

        public DateTime? Termin { get; set; }
        public DateTime ReTermin { get; set; }
        public string ProzessStatus { get; set; }


        public TimeSpan LengthOfTheProblem => ReTermin.Subtract(AuftrittsDatum);




        public Problem( )
        {            
        }

        public Problem(int? id, string bezug, DateTime auftrittsDatum, string abteilung, Mitarbeiter verantwortlicher, string initiator, string kategorie, string thema, string maßnahme, string bewertung, DateTime? termin, DateTime reTermin, string prozessStatus)
        {
            Id = id;
            Bezug = bezug;
            AuftrittsDatum = auftrittsDatum;
            Abteilung = abteilung;
            Verantwortlicher = verantwortlicher;
            Initiator = initiator;
            Kategorie = kategorie;
            Thema = thema;
            Maßnahme = maßnahme;
            Bewertung = bewertung;
            Termin = termin;
            ReTermin = reTermin;
            ProzessStatus = prozessStatus;
        }
    }
}
