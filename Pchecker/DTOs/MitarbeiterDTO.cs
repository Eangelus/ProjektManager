using ProjektManager.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjektManager.DTOs
{
    public class MitarbeiterDTO 
    {
        public MitarbeiterDTO()
        {
            
        }

        public MitarbeiterDTO(int? id, string name, string vorname, string email, string Inabteilung, ObservableCollection<DateTime>? urlaubsTage, int anzDerUrlaubsTage)
        {
            Id = id;
            Name = name;
            Vorname = vorname;
            Email = email;
            InAbteilung = Inabteilung;
            UrlaubsTage = urlaubsTage;
            AnzDerUrlaubsTage = anzDerUrlaubsTage;
        }

        [Key]
        public int? Id { get; set; }

        public string Name { get; set; } = String.Empty;

        public string Vorname { get; set; } = String.Empty;

        public string Email { get; set; } = String.Empty;

        public string InAbteilung { get; set; } = String.Empty;
        public ObservableCollection<DateTime> UrlaubsTage { get; set; } = new ObservableCollection<DateTime>();

        public int AnzDerUrlaubsTage { get; set; } = 30;

        public static MitarbeiterDTO? ToMitarbeiterDTO(Mitarbeiter? mitarbeiter) 
        {
            if (mitarbeiter == null)
            {
                return null;
            }
            return new MitarbeiterDTO(mitarbeiter.Id, mitarbeiter.Name, mitarbeiter.Vorname, mitarbeiter.Email, mitarbeiter.InAbteilung, mitarbeiter.UrlaubsTage, mitarbeiter.AnzDerUrlaubsTage);
        }

        public static Mitarbeiter? FromMitarbeiterDTO(MitarbeiterDTO mitarbeiterDTO)
        {
            if (mitarbeiterDTO == null)
            {
                return null;
            }
            return new Mitarbeiter(mitarbeiterDTO.Id, mitarbeiterDTO.Name, mitarbeiterDTO.Vorname, mitarbeiterDTO.Email, mitarbeiterDTO.InAbteilung, mitarbeiterDTO.UrlaubsTage, mitarbeiterDTO.AnzDerUrlaubsTage);

        }



    }
}

