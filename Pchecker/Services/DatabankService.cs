using LiveChartsCore.SkiaSharpView;
using ProjektManager.DataBaseAPI;
using ProjektManager.DTOs;
using ProjektManager.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjektManager.Services
{
    public class DatabankService
    {

        public DatabankService() { }

        
        public static ObservableCollection<Mitarbeiter> loadAllMitarbeiter()
        {
            ObservableCollection<Mitarbeiter> MitarbeiterListe = new ObservableCollection<Mitarbeiter>();
            var _projektDBContextFactory = new ProjektDBContextFactory(App.CONSTRING);
            using (ProjektDBContext dbContext = _projektDBContextFactory.CreateDbContext())
            {
               MitarbeiterListe = new ObservableCollection<Mitarbeiter>(dbContext.GetAllMitarbeiter().Select(m => MitarbeiterDTO.FromMitarbeiterDTO(m)).ToList());
            }
            return MitarbeiterListe;
        }


        public static ObservableCollection<Projekt> LoadAllProjekte()
        {
            ObservableCollection<Projekt> Projekte = new ObservableCollection<Projekt>();
            var _projektDBContextFactory = new ProjektDBContextFactory(App.CONSTRING);
            using (ProjektDBContext dbContext = _projektDBContextFactory.CreateDbContext())
            {
                Projekte = new ObservableCollection<Projekt>(dbContext.GetAllProjekts().Select(p => ProjektDTO.FromProjektDTO(p)).ToList());
            }
            return Projekte;

        }

        public static ObservableCollection<Problem> LoadAllProbleme()
        {
            ObservableCollection<Problem> Probleme = new ObservableCollection<Problem>();
            var _projektDBContextFactory = new ProjektDBContextFactory(App.CONSTRING);
            using (ProjektDBContext dbContext = _projektDBContextFactory.CreateDbContext())
            {
                Probleme = new ObservableCollection<Problem>(dbContext.GetAllProblem().Select(p => ProblemDTO.FromProblemDTO(p)).ToList());
            }
            return Probleme;

        }

        //public static ObservableCollection<Abteilung> LoadAllAbteilungen()
        //{
        //    ObservableCollection<Abteilung> Abteilung = new ObservableCollection<Abteilung>();
        //    var _projektDBContextFactory = new ProjektDBContextFactory(App.CONSTRING);
        //    using (ProjektDBContext dbContext = _projektDBContextFactory.CreateDbContext())
        //    {
        //        Abteilung = new ObservableCollection<Abteilung>(dbContext.GetAllAbteilung().Select(p => AbteilungDTO.FromAbteilungDTO(p)).ToList());
        //    }
        //    return Abteilung;

        //}

        public static ObservableCollection<Stundenbuchung> LoadAllStundenbuchungen()
        {
            ObservableCollection<Stundenbuchung> Stundenbuchungen = new ObservableCollection<Stundenbuchung>();
            var _projektDBContextFactory = new ProjektDBContextFactory(App.CONSTRING);
            using (ProjektDBContext dbContext = _projektDBContextFactory.CreateDbContext())
            {
                Stundenbuchungen = new ObservableCollection<Stundenbuchung>(dbContext.GetAllStundenbuchungen().Select(s => StundenbuchungDTO.FromStundenbuchungDTO(s)).ToList());
            }
            return Stundenbuchungen;
        }

        public static void Updatetundenbuchungen(Stundenbuchung stunden)
        {
            
            var _projektDBContextFactory = new ProjektDBContextFactory(App.CONSTRING);
            using (ProjektDBContext dbContext = _projektDBContextFactory.CreateDbContext())
            {
                
                   dbContext.UpdateStunden(StundenbuchungDTO.ToStundenbuchungDTO(stunden));
                
            }
           
        }

    }
}
