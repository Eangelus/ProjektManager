
using Microsoft.EntityFrameworkCore;

using ProjektManager.Models;

using LiveChartsCore.SkiaSharpView.Painting;
using LiveChartsCore.SkiaSharpView;
using SkiaSharp;
using ProjektManager.DTOs;
using ProjektManager.Models;
using System;
using System.Collections.Generic;
using System.Windows;


namespace ProjektManager.DataBaseAPI
{
    public class ProjektDBContext : DbContext
    {
        public DbSet<MitarbeiterDTO> Mitarbeiter { get; set; }
        public DbSet<ProblemDTO> Probleme { get; set; }

        public DbSet<ProjektDTO> Projekte { get; set; }
        public DbSet<StundenbuchungDTO> Stundenbuchungen { get; set; }

        //public DbSet<AbteilungDTO> Abteilungen { get; set; }

        public ProjektDBContext(DbContextOptions options) : base(options)
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
            base.OnModelCreating(modelBuilder);
        }


        public IEnumerable<ProjektDTO> GetAllProjekts()
        {

            var erg = Projekte.Include(p => p.Probleme).ThenInclude(p => p.Verantwortlicher).Include(p => p.Probleme).ThenInclude(p => p.Initiator);
            return erg;
        }
        //public IEnumerable<AbteilungDTO> GetAllAbteilung()
        //{

        //    var erg = Abteilungen.Include(m => m.Mitarbeiters);
        //    return erg;
        //}

        public IEnumerable<MitarbeiterDTO> GetAllMitarbeiter()
        {

            var erg = Mitarbeiter;
            return erg;
        }
        public ProblemDTO UpdateProblem(ProblemDTO problemToUpdate)
        {
            if (problemToUpdate.Abteilung == null)
            {
            }
            var update = Probleme.Update(problemToUpdate);
            base.SaveChanges();
            return update.Entity;
        }

        public void DeleteProjekt(ProjektDTO projektToDelete)
        {
            if (projektToDelete != null)
            {
                base.Entry(projektToDelete).State = EntityState.Deleted;
                projektToDelete.Probleme.ForEach(p =>
                {
                    base.Entry(p).State = EntityState.Deleted;
                });
                //Projekte.Remove(projektToDelete);
                base.SaveChanges();
            }
        }

        internal IEnumerable<StundenbuchungDTO> GetAllStundenbuchungen()
        {
            var erg = Stundenbuchungen.Include(pro => pro.Projekt).Include(p => p.Mitarbeiter);
            return erg;
        }

        public StundenbuchungDTO UpdateStunden(StundenbuchungDTO stundenToUpdate)
        {
            if (stundenToUpdate == null)
            {
            }
            var update = Stundenbuchungen.Update(stundenToUpdate);
            base.SaveChanges();
            return update.Entity;
        }
    }
}

