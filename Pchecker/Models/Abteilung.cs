using DocumentFormat.OpenXml.Presentation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjektManager.Models
{
    public class Abteilung
    {
        public Abteilung()
        {

        }

        public Abteilung(int id, string bezeichnung, List<string> jobs )
        {
            Id = id;
            Bezeichung = bezeichnung;
            Jobs = jobs;
        }

        public int Id { get; set; }
        public string Bezeichung { get; set; }
       
        public List<string> Jobs { get; set; }

        public ObservableCollection<Mitarbeiter> Mitarbeiters { get; set; } = new ObservableCollection<Mitarbeiter>();

        public static ObservableCollection<Abteilung> CreateAllAbteilungen() {
            ObservableCollection<Abteilung>  Abteilungen = new ObservableCollection<Abteilung> {
                            new Abteilung { Id = 01, Bezeichung = "Projektleitung", Jobs = new List<string> { "Projektleitung" } },
                            new Abteilung { Id = 02, Bezeichung = "Konstruktion", Jobs = new List<string> { "Konstruktion", "Dokumentation", "Besprechung (K)", "Beschaffung (K)" } },
                            new Abteilung { Id = 03, Bezeichung = "Elektrotechnik", Jobs = new List<string> { "Programmierung", "E-Plan", "Dokumenation", "Besprechung (E)", "Beschaffung (E)" } },
                            new Abteilung { Id = 04, Bezeichung = "Monatge", Jobs = new List<string> { "Montage mechanisch", "Montage elektronisch", "Montage mechanisch extern", "Monatge elektronisch extern"} }
                            };
            return Abteilungen;
        }
  

        public void AddMitarbeiter(Mitarbeiter mitarbeiter)
        {
            mitarbeiter.InAbteilung = Bezeichung;
            Mitarbeiters.Add(mitarbeiter);  
            
        }

        public void DropMitarbeiter(Mitarbeiter mitarbeiter, int id)
        {
            foreach(Mitarbeiter m in Mitarbeiters)
            {
                if(m.Id == id)
                {
                    Mitarbeiters.Remove(mitarbeiter);
                }
            }
        }

    }
}
