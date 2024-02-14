using ProjektManager.Models;


namespace ProjektManager.Models
{
    public class Abteilung
    {
        

        public string AbBezeichung { get; set; }

        public string AbLeiter { get; set; }
        public List<Mitarbeiter> Mitarbeiter { get; set; }

        public List<Projekt> Projekte { get; set; }
        public List<Problem> Probleme { get; set; }

        public Abteilung() { }
        public Abteilung(string abBezeichung, string abLeiter, List<Mitarbeiter> mitarbeiter, List<Projekt> projekte, List<Problem> probleme)
        {
            AbBezeichung = abBezeichung;
            AbLeiter = abLeiter;
            Mitarbeiter = mitarbeiter;
            Projekte = projekte;
            Probleme = probleme;
        }
    }
}
