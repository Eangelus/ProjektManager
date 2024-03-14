using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjektManager.Models
{
    public static class Jobs
    {
        public static string Programmier  "Programmieren";
                    
        public static ObservableCollection<string> ListOfState = new ObservableCollection<string>() {
    Problem_Erkannt, Umsetzung_Eingeleitet, Umsetzung_Laufend,Vorgang_Abgeschlossen ,
    Umsetzung_Beendet, Entscheidung, Info};
    }
}
