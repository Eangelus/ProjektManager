using ClosedXML.Excel;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using Pchecker.Models;
using SkiaSharp;
using System.Globalization;
using System.IO;

namespace Pchecker.Logic
{
    public class ExcelConnection
    {
        public ExcelConnection() { }

        public static IEnumerable<Projekt> ReadAllExcelFiles()
        {
            var path = "C:\\Users\\t.bernecker\\Desktop\\TestData";
            string[] files = Directory.GetFiles(path);

            IEnumerable<Projekt> projekte = new List<Projekt>();

            return files.AsParallel().Select(ReadExcelFile);

            //foreach (string file in files)
            //{
            //    projekte = projekte.Append(ReadExcelFile(file));

            //}

            //return projekte;

        }

        public static Projekt ReadExcelFile(string path)
        {

            Projekt projekt = new Projekt();


            Console.WriteLine(path);
            var workbook = new XLWorkbook(new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read));
            var sheet = workbook.Worksheet(1);

            projekt.ProjektNr = sheet.Cell("C7").Value.GetText();
            projekt.Auftraggeber = sheet.Cell("C8").Value.GetText();
            projekt.Startpunkt = DateTime.Now;

            int counter = 11;
            //         //        ◔ - Problem erkannt
            //◑ - Umsetzung eingeleitet 
            //◕ - Umsetzung in Arbeit
            // ● - Umsetzung beendet
            //
            // 
            int[] values = [0, 0, 0, 0, 0, 0, 0,];


            while (true)
            {
                Problem problem = new Problem();
                XLCellValue problemNummerZumPrüfen = sheet.Cell($"A{counter}").Value;
                if (problemNummerZumPrüfen.IsBlank || problemNummerZumPrüfen.ToString() == "")
                {
                    break;
                }
                if (!int.TryParse(problemNummerZumPrüfen.ToString(), out _))
                {
                    throw new Exception();
                }

                problem.PID = Convert.ToInt32(problemNummerZumPrüfen.ToString());
                problem.Bezug = sheet.Cell($"B{counter}").Value.ToString();
                problem.AuftritsDatum = sheet.Cell($"C{counter}").IsEmpty() ? null : sheet.Cell($"C{counter}").Value.GetDateTime();

                if (problem.AuftritsDatum != null)
                {
                    problem.KW = CultureInfo.InvariantCulture.Calendar.GetWeekOfYear(problem.AuftritsDatum.Value, CalendarWeekRule.FirstDay, DayOfWeek.Monday);
                }

                problem.Termin = sheet.Cell($"L{counter}").IsEmpty() ? null : sheet.Cell($"L{counter}").Value.GetDateTime();

                problem.Bewertung = sheet.Cell($"K{counter}").Value.ToString();

                problem.Abteilung = sheet.Cell($"E{counter}").Value.ToString();
                problem.Name = sheet.Cell($"F{counter}").Value.ToString();
                problem.Initiator = sheet.Cell($"G{counter}").Value.ToString();
                problem.Kategorie = sheet.Cell($"H{counter}").Value.ToString();
                problem.Thema = sheet.Cell($"I{counter}").Value.ToString();
                problem.Maßnahme = sheet.Cell($"J{counter}").Value.ToString();

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
                switch (statusChar)
                {

                    case "◔":
                        problem.ProzessStatus = ProzessStatus.Problem_Erkannt;
                        values[0]++;
                        break;
                    case "✓":
                        problem.ProzessStatus = ProzessStatus.Vorgang_Abgeschlossen;
                        values[4]++;
                        break;
                    case "●":
                        problem.ProzessStatus = ProzessStatus.Umsetzung_Beendet;
                        values[3]++;
                        break;
                    case "◕":
                        problem.ProzessStatus = ProzessStatus.Umsetzung_Laufend;
                        values[2]++;
                        break;
                    case "◑":
                        problem.ProzessStatus = ProzessStatus.Umsetzung_Eingeleitet;
                        values[1]++;
                        break;
                    case "I":
                        problem.ProzessStatus = ProzessStatus.Info;
                        values[5]++;
                        break;
                    case "E":
                        problem.ProzessStatus = ProzessStatus.Entscheidung;
                        values[6]++;
                        break;
                }

                try
                {
                    problem.ReTermin = sheet.Cell($"M{counter}").IsEmpty() || sheet.Cell($"M{counter}").Value.ToString().Trim() == String.Empty ? null : sheet.Cell($"M{counter}").Value.GetDateTime();
                }
                catch { }
                projekt.Probleme.Add(problem);

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
            projekt.ProblemStatus = chartData;



            return projekt;
        }

    }
}
