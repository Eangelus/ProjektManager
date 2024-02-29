using ProjektManager.Models;
using System.ComponentModel.DataAnnotations;
using LiveChartsCore.SkiaSharpView.Painting;
using LiveChartsCore.SkiaSharpView;
using SkiaSharp;
using Microsoft.EntityFrameworkCore;

namespace ProjektManager.DTOs
{
    public class ProjektDTO
    {

        public ProjektDTO()
        {

        }

        public ProjektDTO(string projektNr, string auftraggeber, DateTime stand, DateTime startpunkt, string projektLeiter, DateTime deadLine, List<ProblemDTO> probleme, int status, DateTime dateOfTheEnd)
        {
            ProjektNr = projektNr;
            Auftraggeber = auftraggeber;
            Stand = stand;
            Startpunkt = startpunkt;
            ProjektLeiter = projektLeiter;
            DeadLine = deadLine;
            Probleme = probleme;
            Status = status;
            DateOfTheEnd = dateOfTheEnd;
        }

        [Key] 
        public string ProjektNr { get; set; }

        public string Auftraggeber { get; set; }



        public DateTime Stand { get; set; }

        public DateTime Startpunkt { get; set; }

        public string ProjektLeiter { get; set; }


        public DateTime DeadLine { get; set; }

        public List<ProblemDTO> Probleme { get; set; }

        public int Status { get; set; }
        public DateTime DateOfTheEnd { get; set; }


        public static ProjektDTO ToProjektDTO(Projekt projekt)
        {
            List<ProblemDTO> problemDTOs = new List<ProblemDTO>();
            foreach (Problem problem in projekt.Probleme)
            {
                problemDTOs.Add(ProblemDTO.ToProblemDTO(problem));
            };

            return new ProjektDTO()
            {
                
                ProjektNr = projekt.ProjektNr,
                Auftraggeber = projekt.Auftraggeber,
                Stand = projekt.Stand,
                Startpunkt = projekt.Startpunkt,
                ProjektLeiter = projekt.ProjektLeiter,
                Probleme = problemDTOs,
                DeadLine = projekt.DeadLine.Value,
            };
        }

        public static Projekt FromProjektDTO(ProjektDTO projektDTO)
        {
            List<Problem> problems = new List<Problem>();
            int[] values = [0, 0, 0, 0, 0, 0, 0,];
            if (projektDTO.Probleme != null)
            {
                foreach (var p in projektDTO.Probleme)
                {
                    problems.Add(ProblemDTO.FromProblemDTO(p));

                    switch (p.ProzessStatus)
                    {

                        case ProzessStatus.Problem_Erkannt:
                            values[0]++;
                            break;
                        case ProzessStatus.Vorgang_Abgeschlossen:
                            values[4]++;
                            break;
                        case ProzessStatus.Umsetzung_Beendet:
                            values[3]++;
                            break;
                        case ProzessStatus.Umsetzung_Laufend:
                            values[2]++;
                            break;
                        case ProzessStatus.Umsetzung_Eingeleitet:
                            values[1]++;
                            break;
                        case ProzessStatus.Info:
                            values[5]++;
                            break;
                        case ProzessStatus.Entscheidung:
                            values[6]++;
                            break;
                    }
                }
            }

            IEnumerable<PieSeries<int>> chartData = new[]
            {
                        new PieSeries<int> {Values= new[]{values[0]}, Name=ProzessStatus.Problem_Erkannt, Fill=new SolidColorPaint(SKColors.DarkRed) },
                        new PieSeries<int> {Values= new[]{values[1]}, Name=ProzessStatus.Umsetzung_Eingeleitet, Fill=new SolidColorPaint(SKColors.Red)},
                        new PieSeries<int> {Values= new[]{values[2]}, Name=ProzessStatus.Umsetzung_Laufend, Fill=new SolidColorPaint(SKColors.Yellow)},
                        new PieSeries<int> {Values= new[]{values[3]}, Name=ProzessStatus.Umsetzung_Beendet, Fill=new SolidColorPaint(SKColors.Blue)},
                        new PieSeries<int> {Values= new[]{values[4]}, Name=ProzessStatus.Vorgang_Abgeschlossen, Fill=new SolidColorPaint(SKColors.Green)},
                        new PieSeries<int> {Values= new[]{values[5]}, Name=ProzessStatus.Info, Fill=new SolidColorPaint(SKColors.AntiqueWhite)},
                        new PieSeries<int> {Values= new[]{values[6]}, Name=ProzessStatus.Entscheidung, Fill=new SolidColorPaint(SKColors.Violet)},
             };

            chartData = chartData.Where(x => x.Values.ToList()[0] != 0);

            return new Projekt(projektDTO.Auftraggeber, projektDTO.ProjektNr, projektDTO.Stand, projektDTO.ProjektLeiter, projektDTO.DeadLine, projektDTO.Startpunkt, problems, projektDTO.DateOfTheEnd, chartData);
        }

    }
}
