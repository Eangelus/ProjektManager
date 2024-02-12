using ProjektManager.Models;
using System.ComponentModel.DataAnnotations.Schema;


namespace ProjektManager.Models
{
    public class Mitarbeiter 
    { 

        public string Name { get; }
        public string NachName { get; }
        public int ProjektStunden { get; }



    }
}
