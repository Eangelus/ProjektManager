using ProjektManager.Models;


namespace ProjektManager.Models
{
    public class Abteilung
    {
        

        public string AbBezeichung { get; set; }

        public string AbLeiter { get; set; }
        public List<Mitarbeiter> Mitarbeiter { get; set; }

        public Abteilung() { }
        public Abteilung(string abBezeichung, string abLeiter, List<Mitarbeiter> mitarbeiter)
        {
            AbBezeichung = abBezeichung;
            AbLeiter = abLeiter;
            Mitarbeiter = mitarbeiter;
        }
    }
}
