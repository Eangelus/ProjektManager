using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pchecker.Models
{
    public class Abteilungen
    {

        public Abteilungen(string abLeiter, int abID) { 
        
            this._abLeiter = abLeiter;
            this._abID = abID;

        }
        private int _abID = 0;
        public int AbID
        {
            get { return _abID; }
            set { _abID = value; }
        }

        private string _abBezeichung = "";
        public string AbBezeichung
        {
            get { return _abBezeichung; }
            set { _abBezeichung = value; }
        }

        private string _abLeiter = "";
        public string AbLeiter
        {
            get { return _abLeiter; }
            set { _abLeiter = value; }
        }

        private List<Mitarbeiter> _mitarbeiterList;
        public List<Mitarbeiter> MitarbeiterList
        {
            get { return _mitarbeiterList; }
            set { _mitarbeiterList = value; }
        }


        private void addMitarbeiter(Mitarbeiter mitarbeiter) {

            _mitarbeiterList.Add(mitarbeiter);

        }

        private void deleteMitarbeiter(Mitarbeiter mitarbeiter)
        {
            //foreach (Mitarbeiter a in mitarbeiterList) { 
                //if (mitarbeiter.mitarbeiterID = mitarbeiterList[a.mitarbeiterID]){
                    //mitarbeiterList.Remove(a);
                //}
            //}

        }



    }
}
