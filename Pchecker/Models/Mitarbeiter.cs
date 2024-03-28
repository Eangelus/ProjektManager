using ProjektManager.Models;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web;


namespace ProjektManager.Models
{
    public class Mitarbeiter 
    {

        public Mitarbeiter() { }

        public Mitarbeiter(int? id,string name, string vorname, string email, string? inAbteilung, ObservableCollection<DateTime> urlaubsTage,int anzDerUrlaubsTage)
        {
            this.Id = id;
            Name = name;
            Vorname = vorname;
            Email = email;  // $"{mitarbeiter.Vorname[0]}.{mitarbeiter.Name}@jp-industrieanlagen.de"
            InAbteilung = inAbteilung;
            UrlaubsTage = urlaubsTage;
            AnzDerUrlaubsTage = anzDerUrlaubsTage;

        }

        public int? Id { get; set; }
        public string Name { get; set; } = String.Empty;
        public string Vorname { get; set; } = String.Empty;
        public string Email { get; set; } = String.Empty;

        public string InAbteilung {  get; set; } = String.Empty;

        public ObservableCollection<DateTime>? UrlaubsTage {  get; set; } = new ObservableCollection<DateTime>();
        public int AnzDerUrlaubsTage { get; set; } = 30;


    }
}
