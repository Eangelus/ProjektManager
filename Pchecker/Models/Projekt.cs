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


        public string Auftraggeber { get; }

        
        
        public string ProjektNr { get; }

        public DateTime Stand { get; }

        public DateTime Startpunkt { get; }
        public string ProjektLeiter { get; }
        public DateTime? DeadLine { get; }
        public List<Abteilung> Abteilungen { get; }


        public List<Problem> Probleme { get; }

        public DateTime DateOfTheEnd { get; }
       
        public Projekt(string Auftraggeber, string ProjektNr, DateTime Stand, string ProjektLeiter, DateTime? DeadLine, DateTime Startpunkt,  List<Problem> Probleme, DateTime DateOfTheEnd, IEnumerable<ISeries> ChartData)
        {
            this.Auftraggeber = Auftraggeber;
            this.ProjektNr = ProjektNr;
            this.Stand = Stand;
            this.Startpunkt = Startpunkt;
            this.DeadLine = DeadLine;
            this.ProjektLeiter = ProjektLeiter;
            this.Abteilungen = new List<Abteilung>();
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

        public IEnumerable<ISeries> ChartData { get; }

        public TimeSpan LengthOfTheProjekt => DateOfTheEnd.Subtract(Startpunkt);



    }


}
