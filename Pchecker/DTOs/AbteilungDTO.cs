using ProjektManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjektManager.DTOs
{
    public class AbteilungDTO : Entity
    {
        public AbteilungDTO()
        {
            
        }
        public string AbBezeichung { set; get; }
        public string AbLeiter { set; get; }

        public List<MitarbeiterDTO>? _mitarbeiterList { get; set; }
        
    }
}
