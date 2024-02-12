
using ProjektManager.Models;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace ProjektManager.Models
{
    public class Problem 

    {

        public string Bezug { get; }
        public DateTime AuftrittsDatum { get; }
        public int KW { get; }
        public string Abteilung { get; }
        public string Name { get; }
        public string Initiator { get; }
        public string Kategorie { get; }
        public string Thema { get; }
        public string Maßnahme { get; }

        public string Bewertung { get; }

        public DateTime? Termin { get; }
        public DateTime ReTermin { get; }
        public string ProzessStatus { get; }
        public string ProjektNr { get; }


        public TimeSpan LengthOfTheProblem => ReTermin.Subtract(AuftrittsDatum);

        public Problem( )
        {            
        }

        public Problem(string bezug, DateTime AuftrittsDatum, int kW, string abteilung, string name, string initiator, string kategorie, string thema, string maßnahme, string bewertung, DateTime? termin, DateTime reTermin, string prozessStatus, string projektNr)
        {
            Bezug = bezug;
            AuftrittsDatum = AuftrittsDatum;
            KW = kW;
            Abteilung = abteilung;
            Name = name;
            Initiator = initiator;
            Kategorie = kategorie;
            Thema = thema;
            Maßnahme = maßnahme;
            Bewertung = bewertung;
            Termin = termin;
            ReTermin = reTermin;
            ProzessStatus = prozessStatus;
            ProjektNr = projektNr;
        }
    }
}
