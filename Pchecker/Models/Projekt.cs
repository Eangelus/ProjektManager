using LiveChartsCore;
using ProjektManager.DataBaseAPI.ProjektProviders;
using ProjektManager.DataBaseAPI.Services.ProjektCreator;
using ProjektManager.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;



namespace ProjektManager.Models
{
    public class Projekt  
    {


        public string Auftraggeber { get; set; }

        
        
        public string ProjektNr { get; set; }

        public DateTime Stand { get; set; }

        public DateTime Startpunkt { get; set; }
        public string ProjektLeiter { get; set; }
        public DateTime? DeadLine { get; set; }
        public List<Abteilung> Abteilungen { get; set; }


        public List<Problem> Probleme { get; set; }

        public DateTime DateOfTheEnd { get; set; }
       
        public Projekt(string Auftraggeber, string ProjektNr, DateTime Stand, string ProjektLeiter, DateTime? DeadLine, DateTime Startpunkt,  List<Problem> Probleme, DateTime DateOfTheEnd, IEnumerable<ISeries> ChartData)
        {
            this.Auftraggeber = Auftraggeber;
            this.ProjektNr = ProjektNr;
            this.Stand = Stand;
            this.Startpunkt = Startpunkt;
            this.DeadLine = DeadLine;
            this.ProjektLeiter = ProjektLeiter;
            this.ChartData = ChartData;
            this.Probleme = Probleme;
            this.DateOfTheEnd = DateOfTheEnd;
        }

        public Projekt()
        {
            

        }


        public bool Conflicts(Projekt projekt)
        {
            if (projekt.ProjektNr == ProjektNr)
            {
                return false;
            }

            return true;
            
        }

        public IEnumerable<ISeries> ChartData { get; set; }

        public TimeSpan LengthOfTheProjekt => DateOfTheEnd.Subtract(Startpunkt);



    }


}
