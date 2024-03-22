using DocumentFormat.OpenXml.Office2010.Excel;
using DocumentFormat.OpenXml.Office2016.Drawing.ChartDrawing;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.Wordprocessing;
using LiveChartsCore;
using ProjektManager.ControlElements;
using ProjektManager.Models;
using ProjektManager.Services;
using System.Collections.Generic;
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


        private int _MinutenGesamt;

        public int MinutenGesamt
        {
            get
            {
                return _MinutenGesamt;
            }
            set
            {
                _MinutenGesamt = _MinutenGesamt = Stunden * 60 + Minuten; 
                OnPropertyChanged(nameof(MinutenGesamt));
                
            }
        }



        private int _Minuten;

        public int Minuten
        {
            get
            {
              
                return _Minuten;
                
            }
            set
            {
                _Minuten = value;
                OnPropertyChanged(nameof(Minuten));
                
            }
        }


        private int _stunden;

        public int Stunden
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
                _StartTime = value;
                OnPropertyChanged(nameof(StartTime));
            }
        }

        public ObservableCollection<Stundenbuchung> FilteredStundenbuchungen
        {
            get
            {
                IEnumerable<Stundenbuchung> filtered = new List<Stundenbuchung>(AlleStundenbuchungen);
                if (!(SelectedProjekt == null || SelectedProjekt.ProjektNr == "Alle"))
                {
                    filtered = filtered.Where(x => x.Projekt.ProjektNr == SelectedProjekt.ProjektNr);
                }
                if (!(SelectedMitarbeiter == null || SelectedMitarbeiter.Name == "Alle"))
                {
                    filtered = filtered.Where(x => x.Mitarbeiter.Id == SelectedMitarbeiter.Id);
                }
                
                return new ObservableCollection<Stundenbuchung>(filtered);
            }
            set
            {
                OnPropertyChanged(nameof(FilteredStundenbuchungen));
                OnPropertyChanged(nameof(AlleStundenbuchungen));
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
                LoadAllStunden();
                OnPropertyChanged(nameof(SelectedProjekt));
                OnPropertyChanged(nameof(FilteredStundenbuchungen));
                OnPropertyChanged(nameof(AlleStundenbuchungen));
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
                OnPropertyChanged(nameof(AlleStundenbuchungen));
                LoadAllStunden();
            }
        }

        public void LoadAllMitarbeiter()
        {
            Mitarbeiter.Clear();
            Mitarbeiter.Add(new Mitarbeiter(null, "Alle", "", "", ""));
            var mitarbeiter = DatabankService.loadAllMitarbeiter();
            foreach (var m in mitarbeiter)
            {
                Mitarbeiter.Add(m);

            }

        }

        public void LoadAllProjekts()
        {
            ProjekteListe.Clear();
            ProjekteListe.Add(new Projekt("","Alle", DateTime.Now,"", DateTime.Now, DateTime.Now,  new List<Problem>(), DateTime.Now, null));
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
