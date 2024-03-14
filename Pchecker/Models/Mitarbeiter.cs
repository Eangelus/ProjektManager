using ProjektManager.Models;
using System.ComponentModel.DataAnnotations.Schema;


namespace ProjektManager.Models
{
    public class Mitarbeiter 
    {

        public Mitarbeiter() { }

        public Mitarbeiter(int? id,string name, string vorname, string email)
        {
            this.Id = id;
            Name = name;
            Vorname = vorname;
            Email = email;  // $"{mitarbeiter.Vorname[0]}.{mitarbeiter.Name}@jp-industrieanlagen.de"
        }

        public int? Id { get; set; }
        public string Name { get; set; } = String.Empty;
        public string Vorname { get; set; } = String.Empty;
        public string Email { get; set; } = String.Empty;

        public Abteilung Abteilung { get; set; }



    }
}
