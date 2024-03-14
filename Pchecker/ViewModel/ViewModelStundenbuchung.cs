using ProjektManager.Models;
using ProjektManager.Services;
using System.Collections.ObjectModel;
using System.Globalization;

namespace ProjektManager.ViewModel
{
    public class ViewModelStundenbuchung : ViewModelBase
    {

        private ObservableCollection<Stundenbuchung> _alleStundenbuchungen = new ObservableCollection<Stundenbuchung>();

        public ObservableCollection<Stundenbuchung> AlleStundenbuchungen
        {
            get { return _alleStundenbuchungen; }
            set { _alleStundenbuchungen = value; OnPropertyChanged(nameof(AlleStundenbuchungen)); }
        }


        private string _Details;

        public string Details
        {
            get
            {
                return _Details;
            }
            set
            {
                _Details = value;
                OnPropertyChanged(nameof(Details));
            }
        }


        private double _stunden;

        public double Stunden
        {
            get
            {
                return _stunden;
            }
            set
            {
                _stunden = value;
                OnPropertyChanged(nameof(Stunden));
            }
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
                OnPropertyChanged(nameof(StartTime));
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
            set
            {
                OnPropertyChanged(nameof(FilteredStundenbuchungen));
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
        public void LoadAllStunden()
        {
            AlleStundenbuchungen.Clear();
            var Stunden = DatabankService.LoadAllStundenbuchungen();
            foreach (var b in Stunden)
            {
                AlleStundenbuchungen.Add(b);

            }

        }

        public ViewModelStundenbuchung()
        {
            LoadAllMitarbeiter();
            LoadAllProjekts();
            LoadAllStunden();
        }





    }
}
