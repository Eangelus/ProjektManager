
using System.ComponentModel.DataAnnotations;

namespace ProjektManager.Models
{
    public abstract class Entity
    {
        [Key]
        public int Id { get; set; }


    }
}
