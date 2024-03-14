using ProjektManager.Models;
using ProjektManager.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjektManager.ViewModel
{
    public class ViewModelNewStundenbuchung : ViewModelBase
    {

        private IEnumerable<Stundenbuchung> _alleStundenbuchungen;

        public IEnumerable<Stundenbuchung> AlleStundenbuchungen
        {
            get { return _alleStundenbuchungen; }
            set { _alleStundenbuchungen = value; }
        }

        private DateTime _StartTime;

        public DateTime StartTime
        {
            get
            {
                return _StartTime;
            }
            set
            {
                Console.WriteLine("Test2");
                OnPropertyChanged(nameof(StartTime));
            }
        }


        private DateTime _EndTime;

        public DateTime EndTime
        {
            get
            {
                return _EndTime;
            }
            set
            {
                Console.WriteLine("Test1");
                OnPropertyChanged(nameof(EndTime));
            }
        }


        public ObservableCollection<Stundenbuchung> FilteredStundenbuchungen
        {
            get
            {
                IEnumerable<Stundenbuchung> filtered = new List<Stundenbuchung>(AlleStundenbuchungen);
                if (SelectedProjekt != null)
                {
                    filtered = filtered.Where(x => x.Projekt.ProjektNr == SelectedProjekt.ProjektNr);
                }
                if (SelectedMitarbeiter != null)
                {
                    filtered = filtered.Where(x => x.Mitarbeiter.Id == SelectedMitarbeiter.Id);
                }
                return new ObservableCollection<Stundenbuchung>(filtered);
            }
        }



        public static ObservableCollection<Mitarbeiter> Mitarbeiter = new ObservableCollection<Mitarbeiter>();


        public static ObservableCollection<Projekt> ProjekteListe = new ObservableCollection<Projekt>();

        private Projekt _selectedProjekt;

        public Projekt SelectedProjekt
        {
            get
            {
                return _selectedProjekt;
            }
            set
            {
                _selectedProjekt = value;
                OnPropertyChanged(nameof(SelectedProjekt));
                OnPropertyChanged(nameof(FilteredStundenbuchungen));
            }
        }


        private Mitarbeiter _selectedMitarbeiter;

        public Mitarbeiter SelectedMitarbeiter
        {
            get
            {
                return _selectedMitarbeiter;
            }
            set
            {
                _selectedMitarbeiter = value;
                OnPropertyChanged(nameof(SelectedMitarbeiter));
                OnPropertyChanged(nameof(FilteredStundenbuchungen));
            }
        }

        public void LoadAllMitarbeiter()
        {
            Mitarbeiter.Clear();
            var mitarbeiter = DatabankService.loadAllMitarbeiter();
            foreach (var m in mitarbeiter)
            {
                Mitarbeiter.Add(m);

            }

        }

        public void LoadAllProjekts()
        {
            ProjekteListe.Clear();
            var Projekte = DatabankService.LoadAllProjekte();
            foreach (var p in Projekte)
            {
                ProjekteListe.Add(p);

            }

        }

        public ViewModelNewStundenbuchung()
        {
            LoadAllMitarbeiter();
            LoadAllProjekts();
        }




    }
}
