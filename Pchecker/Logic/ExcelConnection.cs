using ClosedXML.Excel;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using Microsoft.EntityFrameworkCore;
using ProjektManager.Models;
using ProjektManager.DataBaseAPI;
using ProjektManager.DTOs;
using SkiaSharp;
using System.Globalization;
using System.IO;
using DocumentFormat.OpenXml.InkML;
using DocumentFormat.OpenXml.Office2010.Excel;

namespace ProjektManager.Logic
{
    public class ExcelConnection
    {
        public ExcelConnection() { }

        public static IEnumerable<Projekt> ReadAllExcelFiles()
        {
            var path = "C:\\Users\\t.bernecker\\Desktop\\TestData";
            string[] files = Directory.GetFiles(path);

            IEnumerable<Projekt> projekte = new List<Projekt>();

            //return files.AsParallel().Select(ReadExcelFile);

            foreach (string file in files)
            {
                projekte = projekte.Append(ReadExcelFile(file));

            }

            return projekte;

        }

        public static Projekt ReadExcelFile(string path)
        {
            var _projektDBContextFactory = new ProjektDBContextFactory(App.CONSTRING);
            using (ProjektDBContext dbContext = _projektDBContextFactory.CreateDbContext())
            {
                Console.WriteLine(path);
                var workbook = new XLWorkbook(new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read));
                var sheet = workbook.Worksheet(1);


                string ProjektNr = sheet.Cell("C7").Value.GetText();
                string Auftraggeber = sheet.Cell("C8").Value.GetText();
                DateTime Startpunkt = DateTime.Now;
                string ProjektLeiter = "None";

                int counter = 11;
                int[] values = [0, 0, 0, 0, 0, 0, 0,];

                List<ProblemDTO> problems = new List<ProblemDTO>();
                Dictionary<string, AbteilungDTO> uniqueAbteilungen = new Dictionary<string, AbteilungDTO>();


                while (true)
                {
                    XLCellValue problemNummerZumPrüfen = sheet.Cell($"A{counter}").Value;
                    if (problemNummerZumPrüfen.IsBlank || problemNummerZumPrüfen.ToString() == "")
                    {
                        // Wenn Nr. und Auftrittdatum leer sind, gehe davon aus, dass wir am Ende angekommen sind
                        if (sheet.Cell($"C{counter}").IsEmpty())
                        {
                            break;
                        }
                        counter++;
                        continue;
                    }
                    if (!int.TryParse(problemNummerZumPrüfen.ToString(), out _))
                    {
                        throw new Exception();
                    }

                    string Bezug = sheet.Cell($"B{counter}").Value.GetText();


                    DateTime? AuftrittsDatum = sheet.Cell($"C{counter}").IsEmpty() ? null : sheet.Cell($"C{counter}").Value.GetDateTime();
                    int KW = 0;


                    if (!AuftrittsDatum.HasValue)
                    {
                        AuftrittsDatum = DateTime.Now;
                    }

                    if (AuftrittsDatum != null)
                    {
                        KW = CultureInfo.InvariantCulture.Calendar.GetWeekOfYear(AuftrittsDatum.Value, CalendarWeekRule.FirstDay, DayOfWeek.Monday);
                    }

                    DateTime? Termin = sheet.Cell($"L{counter}").IsEmpty() ? null : sheet.Cell($"L{counter}").Value.GetDateTime();

                    string Bewertung = sheet.Cell($"K{counter}").Value.ToString();

                    string AbteilungBezeichnung = sheet.Cell($"E{counter}").Value.ToString();
                    string Name = sheet.Cell($"F{counter}").Value.ToString();
                    string Initiator = sheet.Cell($"G{counter}").Value.ToString();
                    string Kategorie = sheet.Cell($"H{counter}").Value.ToString();
                    string Thema = sheet.Cell($"I{counter}").Value.ToString();
                    string Maßnahme = sheet.Cell($"J{counter}").Value.ToString();

                    // Dic for Specail text in the Excel of the status
                    //var myColors = new List<SKColors>
                    //    {
                    //    SKColors.Red,
                    //    SKColors.Yellow,
                    //    SKColors.Green,
                    //    SKColors.Blue,
                    //    SKColors.YellowGreen
                    //    };


                    string statusChar = sheet.Cell($"N{counter}").Value.ToString();
                    string ProblemProzessStatus = "";
                    switch (statusChar)
                    {

                        case "◔":
                            ProblemProzessStatus = ProzessStatus.Problem_Erkannt;
                            values[0]++;
                            break;
                        case "✓":
                            ProblemProzessStatus = ProzessStatus.Vorgang_Abgeschlossen;
                            values[4]++;
                            break;
                        case "●":
                            ProblemProzessStatus = ProzessStatus.Umsetzung_Beendet;
                            values[3]++;
                            break;
                        case "◕":
                            ProblemProzessStatus = ProzessStatus.Umsetzung_Laufend;
                            values[2]++;
                            break;
                        case "◑":
                            ProblemProzessStatus = ProzessStatus.Umsetzung_Eingeleitet;
                            values[1]++;
                            break;
                        case "I":
                            ProblemProzessStatus = ProzessStatus.Info;
                            values[5]++;
                            break;
                        case "E":
                            ProblemProzessStatus = ProzessStatus.Entscheidung;
                            values[6]++;
                            break;
                    }

                    DateTime ReTermin = DateTime.Now;
                    try
                    {
                        ReTermin = sheet.Cell($"M{counter}").IsEmpty() || sheet.Cell($"M{counter}").Value.ToString().Trim() == string.Empty ? DateTime.Now : sheet.Cell($"M{counter}").Value.GetDateTime();
                    }
                    catch { }

                    AbteilungDTO? abteilungDTO = null;

                    //Prüfen ob die Abteilungsbezeichung größer als null ist
                    if (AbteilungBezeichnung.Length > 0)
                    {
                        // neue abteilung wird erstellt wen die bezeichung größer als 1
                        abteilungDTO = new AbteilungDTO(AbteilungBezeichnung, "", new List<MitarbeiterDTO>(), new List<ProjektDTO>(), new List<ProblemDTO>());
                    }

                    var found = dbContext.Abteilungen.Any(e => e.AbBezeichung == AbteilungBezeichnung);
                    if (!found && abteilungDTO != null)
                    {

                        abteilungDTO = dbContext.Add(abteilungDTO).Entity;
                        dbContext.SaveChanges();
                        uniqueAbteilungen.TryAdd(abteilungDTO.AbBezeichung, abteilungDTO);

                    }
                    if (found)
                    {
                        abteilungDTO = dbContext.Abteilungen.Single(a => a.AbBezeichung == AbteilungBezeichnung);
                        uniqueAbteilungen.TryAdd(abteilungDTO.AbBezeichung, abteilungDTO);
                    }
                    ProblemDTO newProblem = new ProblemDTO(null, Bezug, AuftrittsDatum.GetValueOrDefault(DateTime.Now), KW, abteilungDTO, Name, Initiator, Kategorie, Thema, Maßnahme, Bewertung, Termin, ReTermin, ProblemProzessStatus, ProjektNr);


                    newProblem = dbContext.Add(newProblem).Entity;
                    dbContext.SaveChanges();
                    problems.Add(newProblem);

                    counter++;
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

                var abteilungen = uniqueAbteilungen.Values.ToList();
                ProjektDTO projekt = new ProjektDTO(ProjektNr, Auftraggeber, DateTime.Now, Startpunkt, ProjektLeiter, DateTime.Now, abteilungen, problems, 0, DateTime.Now);

                if (projekt != null)
                {
                    dbContext.Add(projekt);
                    dbContext.SaveChanges();
                }
                return ProjektDTO.FromProjektDTO(projekt);
            }
        }

    }
}
