
using ProjektManager.Models;

using System.ComponentModel.DataAnnotations;


namespace Pchecker.Models
{
    public class Problem : Entity

    {
        private int _pID = 0;


        public int PID
        {
            get { return _pID; }
            set { _pID = value; }
        }

        private string _bezug = "";
        public string Bezug
        {
            get { return _bezug; }
            set { _bezug = value; }
        }

        private DateTime? _auftritsDatum;
        public DateTime? AuftritsDatum
        {
            get { return _auftritsDatum; }
            set { _auftritsDatum = value; }
        }

        private int _kw = 1;
        public int KW
        {
            get { return _kw; }
            set { _kw = value; }
        }

        private string _abteilung = "";
        public string Abteilung
        {
            get { return _abteilung; }
            set { _abteilung = value; }
        }

        private string _name = "";
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        private string _initiator = "";
        public string Initiator
        {
            get { return _initiator; }
            set { _initiator = value; }
        }

        private string _kategorie = "";
        public string Kategorie
        {
            get { return _kategorie; }
            set { _kategorie = value; }
        }

        private string _thema = "";
        public string Thema
        {
            get { return _thema; }
            set { _thema = value; }
        }

        private string _maßnahme = "";
        public string Maßnahme
        {
            get { return _maßnahme; }
            set { _maßnahme = value; }
        }

        private string _bewertung = ""; // beschreib das problem
        public string Bewertung
        {
            get { return _bewertung; }
            set { _bewertung = value; }
        }

        private DateTime? _termin;
        public DateTime? Termin
        {
            get { return _termin; }
            set { _termin = value; }
        }

        private DateTime? _reTermin;
        public DateTime? ReTermin
        {
            get { return _reTermin; }
            set { _reTermin = value; }
        }

        private String _prozessStatus = "";
        public String ProzessStatus
        {
            get { return _prozessStatus; }
            set { _prozessStatus = value; }
        }




        public Problem(DateTime auftritsDatum, string initiator,string thema,  string bewertung )
        {
            this._auftritsDatum = DateTime.Now;
            this._initiator = initiator;
            this._bewertung = bewertung;
            this._thema = thema;


            
        }

        public Problem()
        {

        }




    }
}
