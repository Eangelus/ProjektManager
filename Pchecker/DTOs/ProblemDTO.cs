using ProjektManager.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjektManager.DTOs
{
    public class ProblemDTO 
    {

        [Key]
        public int? Id { get; set; }

        public ProblemDTO()
        {
            
        }

        public ProblemDTO(int? id, string bezug, DateTime auftrittsDatum, int kW, AbteilungDTO? abteilung, string name, string initiator, string kategorie, string thema, string maßnahme, string bewertung, DateTime? termin, DateTime reTermin, string prozessStatus, string projektNr)
        {
            Id = id;
            Bezug = bezug;
            AuftrittsDatum = auftrittsDatum;
            KW = kW;
            Abteilung = abteilung;
            Name = name;
            Initiator = initiator;
            Kategorie = kategorie;
            Thema = thema;
            Maßnahme = maßnahme;
            Bewertung = bewertung;
            Termin = termin;
            ReTermin = reTermin;
            ProzessStatus = prozessStatus;
        }

        public string Bezug { set; get; }


        public DateTime AuftrittsDatum { set; get; }


        public int KW { set; get; }

        public AbteilungDTO? Abteilung  { set; get; }


        public string Name { set; get; }

        public string Initiator { set; get; }

        public string Kategorie { set; get; }

        public string Thema { set; get; }

        public string Maßnahme { set; get; }

        public string Bewertung { set; get; }

        public DateTime? Termin { set; get; }

        public DateTime ReTermin { set; get; }

        public string ProzessStatus { set; get; }

        public static ProblemDTO ToProblemDTO(Problem problem) => new ProblemDTO()
        {
            Id = problem.Id,
            Bezug = problem.Bezug,
            AuftrittsDatum = problem.AuftrittsDatum,
            KW = problem.KW,
            Abteilung = AbteilungDTO.ToAbteilungDTO(problem.Abteilung),
            Name = problem.Name,
            Initiator = problem.Initiator,
            Kategorie = problem.Kategorie,
            Thema = problem.Thema,
            Maßnahme = problem.Maßnahme,
            Bewertung = problem.Bewertung,
            Termin = problem.Termin,
            ReTermin = problem.ReTermin,
            ProzessStatus = problem.ProzessStatus
        };

        public static Problem FromProblemDTO(ProblemDTO problemDTO)
        {
            return new Problem(problemDTO.Id, problemDTO.Bezug, problemDTO.AuftrittsDatum, problemDTO.Abteilung == null ? null : AbteilungDTO.FromAbteilungDTO(problemDTO.Abteilung), problemDTO.Name, problemDTO.Initiator, problemDTO.Kategorie, problemDTO.Thema, problemDTO.Maßnahme, problemDTO.Bewertung,
                                problemDTO.Termin, problemDTO.ReTermin, problemDTO.ProzessStatus);
           
        }

    }

}
