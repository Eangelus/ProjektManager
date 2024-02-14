
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

        public DbSet<AbteilungDTO> Abteilungen { get; set; }
        public DbSet<MitarbeiterDTO> Mitarbeiter { get; set; }
        public DbSet<ProblemDTO> Probleme { get; set; }

        public DbSet<ProjektDTO> Projekte { get; set; }



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

            modelBuilder.Entity<ProjektDTO>()
                                .HasMany(e => e.Abteilungen)
                                .WithMany(e => e.Projekte);
        }


        public IEnumerable<ProjektDTO> GetAllProjekts()
        {

            var erg = Projekte.Include(p => p.Abteilungen).Include(p => p.Probleme).ThenInclude(prob => prob.Abteilung);
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
    }
}

