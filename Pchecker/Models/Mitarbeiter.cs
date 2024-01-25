using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pchecker.Models
{
    public class Mitarbeiter
    {
        private string _name;
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        private string _nachName;
        public string NachName
        {
            get { return _nachName; }
            set { _nachName = value; }
        }

        private int _projektStunden = 0;
        public int ProjektStunden
        {
            get { return _projektStunden; }
            set { _projektStunden = value; }
        }

        private int _mitarbeiterID = 0;
        public int MitarbeiterID
        {
            get { return _mitarbeiterID; }
            set { _mitarbeiterID = value; }
        }



        public Mitarbeiter(string name, string nachName, int mitarbeiterID) { 
        
            this._name = name;
            this._nachName = nachName;
            this._mitarbeiterID = mitarbeiterID;
            
        
        }

        public void addProjektStunde(int stunden)
        {

            this._projektStunden = this._projektStunden + stunden;

        }

    }
}
