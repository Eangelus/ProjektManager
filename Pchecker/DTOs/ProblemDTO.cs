using ProjektManager.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjektManager.DTOs
{
    public class ProblemDTO : Entity
    {


        public ProblemDTO()
        {
            
        }
        public string Bezug { set; get; }


        public DateTime AuftrittsDatum { set; get; }


        public int KW { set; get; }


        public string Abteilung  { set; get; }


        public string Name { set; get; }

        public string Initiator { set; get; }

        public string Kategorie { set; get; }

        public string Thema { set; get; }

        public string Maßnahme { set; get; }

        public string Bewertung { set; get; }

        public DateTime? Termin { set; get; }

        public DateTime ReTermin { set; get; }

        public string ProzessStatus { set; get; }

       
        public string ProjektNr { set; get; }

        public static ProblemDTO ToProblemDTO(Problem problem) => new ProblemDTO()
        {
            Bezug = problem.Bezug,
            AuftrittsDatum = problem.AuftrittsDatum,
            KW = problem.KW,
            Abteilung = problem.Abteilung,
            Name = problem.Name,
            Initiator = problem.Initiator,
            Kategorie = problem.Kategorie,
            Thema = problem.Thema,
            Maßnahme = problem.Maßnahme,
            Bewertung = problem.Bewertung,
            Termin = problem.Termin,
            ReTermin = problem.ReTermin,
            ProzessStatus = problem.ProzessStatus,
            ProjektNr = problem.ProjektNr,
        };

        public static Problem FromProblemDTO(ProblemDTO problemDTO)
        {
            return new Problem(problemDTO.Bezug, problemDTO.AuftrittsDatum, problemDTO.KW, problemDTO.Abteilung, problemDTO.Name, problemDTO.Initiator, problemDTO.Kategorie, problemDTO.Thema, problemDTO.Maßnahme, problemDTO.Bewertung,
                                problemDTO.Termin, problemDTO.ReTermin, problemDTO.ProzessStatus, problemDTO.ProjektNr);
           
        }

    }

}
