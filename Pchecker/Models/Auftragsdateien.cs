using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjektManager.Models
{
    public class Auftragsdateien
    {

        public Auftragsdateien() { }

        public List<string> AblegeOrte {  get; set; }

        public string Auftraggeber { get; set; }
        public string Auftragsnummer { get; set; }
    }
}
