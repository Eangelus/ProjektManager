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
