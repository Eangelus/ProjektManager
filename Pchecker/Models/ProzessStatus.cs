using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjektManager.Models
{
    public static class ProzessStatus
    {
        //        ◔ - Problem erkannt
        //◑ - Umsetzung eingeleitet 
        //◕ - Umsetzung in Arbeit
        // ● - Umsetzung beendet

        public const string Problem_Erkannt = "Problem erkannt";
        public const string Umsetzung_Eingeleitet = "Umsetzung eingeleitet";
        public const string Umsetzung_Laufend = "Umsetzung Laufend";
        public const string Vorgang_Abgeschlossen = "Vorgang Abgeschlossen";
        public const string Umsetzung_Beendet = "Umsetzung Beendet";
        public const string Entscheidung = "Entscheidung";
        public const string Info = "Informativ";

        //public static string Problem_Erkannt = "Problem erkannt";
        //public static string Umsetzung_Eingeleitet = "Umsetzung eingeleitet";
        //public static string Umsetzung_Laufend = "Umsetzung Laufend";
        //public static string Vorgang_Abgeschlossen = "Vorgang Abgeschlossen";
        //public static string Umsetzung_Beendet = "Umsetzung Beendet";
        //public static string Entscheidung = "Entscheidung";
        //public static string Info = "Inforamtiv";
        public static ObservableCollection<string> ListOfState = new ObservableCollection<string>() { 
    Problem_Erkannt, Umsetzung_Eingeleitet, Umsetzung_Laufend,Vorgang_Abgeschlossen ,
    Umsetzung_Beendet, Entscheidung, Info};

    }
}
