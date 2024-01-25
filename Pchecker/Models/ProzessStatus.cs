using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pchecker.Models
{
    public static class ProzessStatus
    {
        //        ◔ - Problem erkannt
        //◑ - Umsetzung eingeleitet 
        //◕ - Umsetzung in Arbeit
        // ● - Umsetzung beendet

        public static string Problem_Erkannt = "Problem erkannt";
        public static string Umsetzung_Eingeleitet = "Umsetzung eingeleitet";
        public static string Umsetzung_Laufend = "Umsetzung Laufend";
        public static string Vorgang_Abgeschlossen = "Vorgang Abgeschlossen";
        public static string Umsetzung_Beendet = "Umsetzung Beendet";
        public static string Entscheidung = "Entscheidung";
        public static string Info = "Inforamtiv";

    }
}
