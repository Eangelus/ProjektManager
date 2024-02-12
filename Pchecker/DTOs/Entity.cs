
using System.ComponentModel.DataAnnotations;

namespace ProjektManager.DTOs
{
    public abstract class Entity
    {
        [Key]
        public int Id { get; set; }


    }
}
