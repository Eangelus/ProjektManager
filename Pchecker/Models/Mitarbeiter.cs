using ProjektManager.Models;
using System.ComponentModel.DataAnnotations.Schema;


namespace ProjektManager.Models
{
    public class Mitarbeiter 
    { 

        public string Name { get; set; }
        public string NachName { get; set; }
        public int ProjektStunden { get; set; }

        public Abteilung Abteilung { get; set; }



    }
}
