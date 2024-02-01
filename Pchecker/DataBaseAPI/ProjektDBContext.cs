
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pomelo.EntityFrameworkCore.MySql;
using Pchecker.Models;
using ProjektManager.Models;
using System.IO;
using DocumentFormat.OpenXml.InkML;
using System.Collections.Generic;
using System.Windows.Controls.Primitives;
using LiveChartsCore.SkiaSharpView.Painting;
using LiveChartsCore.SkiaSharpView;
using SkiaSharp;


namespace ProjektManager.DataBaseAPI
{
    public class ProjektDBContext : DbContext
    {

        public DbSet<Projekt> Projekte { get; set; }
        public DbSet<Abteilung> Abteilungen { get; set; }
        public DbSet<Mitarbeiter> Mitarbeiter { get; set; }
        public DbSet<Problem> Probleme { get; set; }


 

        public ProjektDBContext(DbContextOptions options): base(options)
        {

        }



        //private string pwd = "123456789";
        //MySqlConnection sql = new MySqlConnection($"server=localhost; database=projekte; uid=root; Password=123456789");

        //protected override void OnConfiguring(DbContextOptionsBuilder ob)
        //{
        //    if (!ob.IsConfigured)
        //    {

        //        ob.UseMySql(conString, serverVersion);
        //        //sql.Open();
        //    }
            
        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Problem>()
                .HasOne(p => p.Projekt)
                .WithMany(b => b.Probleme)
                .HasForeignKey(p => p.ProjektNr); // Definiert den Fremdschlüssel
        }


        public IEnumerable<Projekt> getAllProjekts()
        {
            
            List<Projekt> projekte = Projekte.Include(p=>p.Probleme).ToList();

            


            foreach (var projekt in projekte) {
                int[] values = [0, 0, 0, 0, 0, 0, 0,];
                foreach (Problem prob in projekt.Probleme) {
                    
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

                    switch (prob.ProzessStatus)
                    {

                        case ProzessStatus.Problem_Erkannt:
                            //prob.ProzessStatus = ProzessStatus.Problem_Erkannt;
                            values[0]++;
                            break;
                        case ProzessStatus.Vorgang_Abgeschlossen:
                            //prob.ProzessStatus = ProzessStatus.Vorgang_Abgeschlossen;
                            values[4]++;
                            break;
                        case ProzessStatus.Umsetzung_Beendet:
                            //prob.ProzessStatus = ProzessStatus.Umsetzung_Beendet;
                            values[3]++;
                            break;
                        case ProzessStatus.Umsetzung_Laufend:
                            //prob.ProzessStatus = ProzessStatus.Umsetzung_Laufend;
                            values[2]++;
                            break;
                        case ProzessStatus.Umsetzung_Eingeleitet:
                            //prob.ProzessStatus = ProzessStatus.Umsetzung_Eingeleitet;
                            values[1]++;
                            break;
                        case ProzessStatus.Info:
                            //prob.ProzessStatus = ProzessStatus.Info;
                            values[5]++;
                            break;
                        case ProzessStatus.Entscheidung:
                            //prob.ProzessStatus = ProzessStatus.Entscheidung;
                            values[6]++;
                            break;
                    }


                    chartData = chartData.Where(x => x.Values.ToList()[0] != 0);
                    projekt.ProblemStatus = chartData;


                }






            }
                return projekte;





   




        }









    }
}

