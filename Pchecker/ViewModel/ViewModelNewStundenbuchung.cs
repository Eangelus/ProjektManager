using ProjektManager.Commands;
using ProjektManager.Models;
using ProjektManager.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ProjektManager.ViewModel
{
    public class ViewModelNewStundenbuchung : ViewModelBase
    {

        public ICommand CreateBuchungCommand { get; }

        public string mode;


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




        private string _details;

        public string Details
        {
            get
            {
                return _details;
            }
            set
            {
                _details = value;
                OnPropertyChanged(nameof(Details));
            }
        }



        private DateTime _startTime;

        public DateTime StartTime
        {
            get
            {
                return _startTime;
            }
            set
            {
                _startTime = value;
                OnPropertyChanged(nameof(StartTime));
            }
        }

        public ObservableCollection<Stundenbuchung> FilteredStundenbuchungen
        {
            get
            {
                IEnumerable<Stundenbuchung> filtered = new List<Stundenbuchung>();
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


        public static ObservableCollection<Abteilung> Abteilungen = new ObservableCollection<Abteilung>();
        public static ObservableCollection<Mitarbeiter> Mitarbeiter = new ObservableCollection<Mitarbeiter>();


        public static ObservableCollection<Projekt> ProjekteListe = new ObservableCollection<Projekt>();

        private Projekt _selectedProjekt = new Projekt();

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


        private Mitarbeiter _selectedMitarbeiter = new Mitarbeiter();

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
                OnPropertyChanged(nameof(ListOfJobs));
                

            }
        }



        private List<string> _ListofJobs;

        public List<string> ListOfJobs
        {
            get
            {
                
                foreach (Abteilung A in Abteilungen)
                {

                    if (SelectedMitarbeiter.InAbteilung == A.Bezeichung)
                    {
                        _ListofJobs = A.Jobs;

                    }
                }
                return _ListofJobs;
            }
            set
            {
                _ListofJobs = value;
                OnPropertyChanged(nameof(ListOfJobs));
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
            CreateBuchungCommand = new CreateBuchungCommand(this);
            Abteilungen = Abteilung.CreateAllAbteilungen();
            LoadAllMitarbeiter();
            LoadAllProjekts();


        }




    }
}
