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

        public Abteilung(int id, string name, List<string> jobs, ObservableCollection<Mitarbeiter> mitarbeiters)
        {
            Id = id;
            Name = name;
            Jobs = jobs;
            Mitarbeiters = mitarbeiters;
        }

        public int Id { get; set; }
        public string Name { get; set; }
       
        public List<string> Jobs { get; set; }

        public ObservableCollection<Mitarbeiter> Mitarbeiters { get; set; }

    }
}
