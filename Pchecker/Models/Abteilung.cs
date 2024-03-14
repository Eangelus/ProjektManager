using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjektManager.Models
{
    public class Abteilung
    {
        public Abteilung()
        {
                
        }
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
       
    }
}
