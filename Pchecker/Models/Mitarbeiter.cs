using ProjektManager.Models;
using System.ComponentModel.DataAnnotations.Schema;


namespace ProjektManager.Models
{
    public class Mitarbeiter 
    {

        public Mitarbeiter() { }

        public Mitarbeiter(string name, string vorname, Dictionary<string, int> stundenImProjekt, string email)
        {
            Name = name;
            Vorname = vorname;
            StundenImProjekt = stundenImProjekt;
            Email = email;
        }

        public string Name { get; set; } = String.Empty;
        public string Vorname { get; set; } = String.Empty;
        private Dictionary<string, int> StundenImProjekt { get; set; } // String : ProjektNummern  ---  int : StundenImProjekt
        public string Email { get; set; } = String.Empty;



    }
}
