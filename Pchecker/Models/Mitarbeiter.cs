using ProjektManager.Models;
using System.ComponentModel.DataAnnotations.Schema;


namespace Pchecker.Models
{
    public class Mitarbeiter : Entity
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




        public void addProjektStunde(int stunden)
        {

            this._projektStunden = this._projektStunden + stunden;

        }

    }
}
