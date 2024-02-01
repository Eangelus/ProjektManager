
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
    public class DBContext : DbContext
    {

        public DbSet<Projekt> Projekte { get; set; }
        public DbSet<Abteilung> Abteilungen { get; set; }
        public DbSet<Mitarbeiter> Mitarbeiter { get; set; }
        public DbSet<Problem> Probleme { get; set; }


        public DBContext()
        {

        }

        public DBContext(DbContextOptions<DBContext> options): base(options)
        {

        }



        //private string pwd = "123456789";
        //MySqlConnection sql = new MySqlConnection($"server=localhost; database=projekte; uid=root; Password=123456789");

        protected override void OnConfiguring(DbContextOptionsBuilder ob)
        {
            if (!ob.IsConfigured)
            {
                var conString = "server=localhost; database=projekte; uid=root; Password=123456789";
                var serverVersion = new MySqlServerVersion(new Version(8, 3, 0));
                ob.UseMySql(conString, serverVersion);
                //sql.Open();
            }
            
        }



        public IEnumerable<Projekt> getAllProjekts()
        {
            var db = new DBContext();
            List<Projekt> projekte = db.Projekte.Include(p=>p.Probleme).ToList();

            


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





            //var result = (from p in db.Projekte
            //           join pro in db.Probleme
            //           on p.ProjektNr equals pro.ProjketNr
            //           select new { Projekte = p, Probleme = p }).ToList();
            //IEnumerable<Projekt> projekte = ((IEnumerable<Projekt>)(db. select * From projekte inner join probleme on projekte.ProjektNr = probleme.ProjektNr;));
            //IEnumerable< Problem> Probleme = ((IEnumerable<Problem>)(from p in db.Probleme select new {  p }));

            //foreach (var projekt in projekte)
            //{
            //    projekt.Probleme.Add((IEnumerable<Problem>)(from p in db.Probleme where db.Projekte.ProjektNr = db.Probleme.ProjektNr  select new { p }))
            //}


            //using (var db = new DBContext() ){
            //    List<Projekt> projekte = db.Projekte.ToList();

            //    foreach (var projekt in projekte)
            //    {
            //        var prob = db.Probleme.Where(p => p.ProjketNr == projekt.ProjektNr).ToList();


            //        //Problem pro = prob;
            //        projekt.Probleme.Add(prob);
            //    }

            //    return projekte;
            //}





        }



        //protected override void OnModelCreating(ModelBuilder mb)
        //{



        //    var projekt = Setup<Projekt>(mb);
        //    projekt.Property(x => x.ProjektNr)
        //        .IsRequired();
        //    projekt.OwnsMany(x => x.Probleme);
        //    projekt.HasMany(x => x.Abteilungen);

        //    base.OnModelCreating(mb);

        //    //var problem = Setup<Problem>(mb);
        //    //problem.Property(Probleme => Probleme.).IsRequired();

        //    //var abteilungen = Setup<Abteilung>(mb);
        //    //abteilungen.Property(x => x.Abteilungen)
        //    //    .IsRequired();



        //    base.OnModelCreating(mb);
        //}



        //private static EntityTypeBuilder<Projekt> Setup<T>(ModelBuilder mb)
        //where T: Entity
        //{ 
        //    var entity = mb.Entity<Projekt>();
        //    entity.HasKey(x => x.Id);
        //    entity.ToTable(typeof(T).Name + "s");



        //    return entity;

        //}






    }
}

