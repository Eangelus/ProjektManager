using ProjektManager.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjektManager.DTOs
{
    public class AbteilungDTO
    {
        public AbteilungDTO(int id, string name, List<string> jobs, List<MitarbeiterDTO> mitarbeiters)
        {
            Id = id;
            Name = name;
            Jobs = jobs;
            Mitarbeiters = mitarbeiters;
        }
        public AbteilungDTO()
        {
           
        }
        

        [Key]
        public int Id { get; set; }
        public string Name { get; set; }

        public List<string> Jobs { get; set; }
        
        public List<MitarbeiterDTO> Mitarbeiters { get; set; }


        public static AbteilungDTO? ToAbteilungDTO(Abteilung abteilung)
        {
            List<MitarbeiterDTO> mitarbeiterDTO = new List<MitarbeiterDTO>();
            foreach (Mitarbeiter mitarbeiter in abteilung.Mitarbeiters)
            {
                mitarbeiterDTO.Add(MitarbeiterDTO.ToMitarbeiterDTO(mitarbeiter));
            };
            //List<string> Jobs = new List<string>();
            //foreach(string job in abteilung.Jobs)
            //{
            //    Jobs.Add(job);
            //}

            return new AbteilungDTO()
            {
                Name = abteilung.Name,
                Jobs = abteilung.Jobs,

                Mitarbeiters = mitarbeiterDTO,
               
            };
        }

        public static Abteilung FromAbteilungDTO(AbteilungDTO abteilungDTO)
        {
            ObservableCollection<Mitarbeiter> mitarbeiter = new ObservableCollection<Mitarbeiter>();
            if (abteilungDTO == null)
            {
                return null;
            }

            foreach(var m in abteilungDTO.Mitarbeiters)
            {
                mitarbeiter.Add(MitarbeiterDTO.FromMitarbeiterDTO(m));
            }

            return new Abteilung(){
                Id = abteilungDTO.Id,
                Name =abteilungDTO.Name,
                Jobs = abteilungDTO.Jobs,
                Mitarbeiters = mitarbeiter};

        }
    }
}
