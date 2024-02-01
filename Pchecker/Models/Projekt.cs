using LiveChartsCore;
using ProjektManager.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;



namespace Pchecker.Models
{
    public class Projekt 
    {

        private string _auftraggeber;
        public string Auftraggeber
        {
            get { return _auftraggeber; }
            set { _auftraggeber = value; }
        }

        private string _projektNr;

        [Key]
        public string ProjektNr
        {
            get { return _projektNr; }
            set { _projektNr = value; }
        }

        private DateTime _stand;
        public DateTime Stand
        {
            get { return _stand; }
            set { _stand = value; }
        }

        private DateTime _startpunkt;
        public DateTime Startpunkt
        {
            get { return _startpunkt; }
            set { _startpunkt = value; }
        }

        private string _projektLeiter;
        public string ProjektLeiter
        {
            get { return _projektLeiter; }
            set { _projektLeiter = value; }
        }

        private DateTime _deadLine;
        public DateTime DeadLine
        {
            get { return _deadLine; }
            set { _deadLine = value; }
        }

        private List<Abteilung> _abteilungen;
        public List<Abteilung> Abteilungen
        {
            get { return _abteilungen; }
            set { _abteilungen = value; }
        }
        [NotMapped]
        private List<string> _kategorien = new List<string>() { "I", "FF", "LF", "SF", "BBÜ" };

        [NotMapped]
        public List<string> Kategorien
        {
            get { return _kategorien; }
            set { _kategorien = value; }
        }

        private List<Problem> _probleme = new List<Problem>();
        public List<Problem> Probleme
        {
            get { return _probleme; }
            set { _probleme = value; }
        }

        private int aStatus;

        public int Status
        {
            get { return aStatus; }
            set { this.aStatus = value; }
        }

        public Projekt(DateTime startpunkt, string projektLeiter, string auftraggeber, DateTime deadLine)
        {
            this._startpunkt = startpunkt;
            this._projektLeiter = projektLeiter;
            this._auftraggeber = auftraggeber;
            this._deadLine = deadLine;
            this._abteilungen = new List<Abteilung> { };



        }

        public Projekt()
        {
            this._abteilungen = new List<Abteilung>();
        }

        public void addAbteilungen()
        {
            throw new NotImplementedException();
        }

        [NotMapped]
        private IEnumerable<ISeries> problemStatus;

        [NotMapped]
        public IEnumerable<ISeries> ProblemStatus
        {
            get { return problemStatus; }
            set { problemStatus = value; }
        }



    }


}
