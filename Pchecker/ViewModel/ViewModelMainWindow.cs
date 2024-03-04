using LiveChartsCore.SkiaSharpView;
using LiveChartsCore;
using ProjektManager.Models;
using ProjektManager.Stores;
using ProjektManager.Commands;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows.Controls;
using System.Windows.Input;
using ProjektManager.DataBaseAPI;
using ProjektManager.DTOs;
using SixLabors.Fonts.Unicode;

namespace ProjektManager.ViewModel
{
    public class ViewModelMainWindow : ViewModelBase
    {
        public bool IsNewProblemButtonEnabled
        {
            get { return SelectedProjekt != null; }
        }

        private string _selectedProjektStatus;

        public string SelectedProjektStatus
        {
            get { return _selectedProjektStatus; }
            set { _selectedProjektStatus = value; OnPropertyChanged(nameof(SelectedProjektStatus)); }
        }


        public ICommand OpenNewProjektWindowCommand { get; set; }
        public ICommand OpenNewProblemWindowCommand { get; set; }

        private ObservableCollection<Projekt> projekte;

        public ObservableCollection<Projekt> Projekte
        {
            get { return projekte; }
            set { projekte = value; OnPropertyChanged(nameof(Projekte)); }
        }

        private static ObservableCollection<Mitarbeiter> _allMitarbeiter = new ObservableCollection<Mitarbeiter>();

        public static ObservableCollection<Mitarbeiter> AllMitarbeiter
        {
            get { return _allMitarbeiter; }
            set { _allMitarbeiter = value; }
        }


        public ViewModelMainWindow()
        {
            OpenNewProjektWindowCommand = new OpenNewProjektWindowCommand();
            OpenNewProblemWindowCommand = new OpenNewProblemWindowCommand(this);
            LoadAllProjekte();
            LoadAllMitarbeiter();
        }

        private string searchText;

        public string SearchText
        {
            get
            {
                return searchText;
            }
            set
            {

                searchText = value;


                var suche =
                    from p in SelectedProjekt.Probleme
                    where p.Verantwortlicher == null ? false : p.Verantwortlicher.Name.Contains(searchText) ||
                    (p.Abteilung == null ? false : p.Abteilung.Contains(searchText)) ||
                    (p.Bewertung.Contains(searchText)) ||
                    (p.Termin == null ? false : p.Termin.ToString().Contains(searchText)) ||
                    (p.AuftrittsDatum.ToString().Contains(searchText)) ||
                    (p.Initiator == null ? false : p.Initiator.Contains(searchText)) ||
                    (p.Maßnahme == null ? false : p.Maßnahme.Contains(searchText)) ||
                    (p.Bezug == null ? false : p.Bezug.Contains(searchText)) ||
                    (p.Kategorie == null ? false : p.Kategorie.Contains(searchText)) ||
                    (p.KW.ToString().Contains(searchText)) ||
                    (p.ProzessStatus == null ? false : p.ProzessStatus.Contains(searchText)) ||
                    (p.ReTermin == null ? false : p.ReTermin.ToString().Contains(searchText)) ||
                    (p.Thema == null ? false : p.Thema.Contains(searchText))
                    select p;
                suche.ToList();

                FilterProblems = new ObservableCollection<Problem>(suche.ToList());


                OnPropertyChanged(nameof(SearchText));
            }
        }

        public void LoadAllProjekte()
        {
            var _projektDBContextFactory = new ProjektDBContextFactory(App.CONSTRING);
            using (ProjektDBContext dbContext = _projektDBContextFactory.CreateDbContext())
            {
                Projekte = new ObservableCollection<Projekt>(dbContext.GetAllProjekts().Select(p => ProjektDTO.FromProjektDTO(p)).ToList());
            }

            if (SelectedProjekt != null)
            {
                var newSelectedProjekt = Projekte.Single(p => SelectedProjekt.ProjektNr == p.ProjektNr);
                SelectedProjekt = newSelectedProjekt;
            }


            foreach (var projekt in Projekte)
            {

                foreach (var series in projekt.ChartData)
                {
                    PieSeries<int> casted = (PieSeries<int>)series;
                    casted.ChartPointPointerDown += (sender, e) =>
                    {
                        foreach (var series in ChartData)
                        {
                            (series as PieSeries<int>).Pushout = 0;
                        }
                        casted.Pushout = 20;
                        IEnumerable<Problem> filtered = projekt.Probleme.Where(p =>
                        {
                            return p.ProzessStatus.Equals(series.Name);
                        });

                        FilterProblems = new ObservableCollection<Problem>(filtered);
                        SelectedProjektStatus = series.Name;
                    };
                }

            }
        }

        public void LoadAllMitarbeiter()
        {
            
            var _projektDBContextFactory = new ProjektDBContextFactory(App.CONSTRING);
            using (ProjektDBContext dbContext = _projektDBContextFactory.CreateDbContext())
            {
                AllMitarbeiter.Clear();
                var mitarbeiter = new ObservableCollection<Mitarbeiter>(dbContext.GetAllMitarbeiter().Select(m => MitarbeiterDTO.FromMitarbeiterDTO(m)!).ToList());
                foreach (var m in mitarbeiter)
                {
                    AllMitarbeiter.Add(m);
                }
            }
        }

        ObservableCollection<Problem> _filterProblems = new ObservableCollection<Problem>();

        public ObservableCollection<Problem>? FilterProblems
        {
            get
            {
                return _filterProblems;
            }
            set
            {
                if (value == null)
                {
                    _filterProblems = new ObservableCollection<Problem> { };
                    return;
                }
                _filterProblems = value;
                OnPropertyChanged(nameof(FilterProblems));
            }
        }




        private Projekt selectedProjekt;

        public Projekt SelectedProjekt
        {
            get { return selectedProjekt; }
            set
            {
                SelectedProjektStatus = String.Empty;
                selectedProjekt = value;
                OnPropertyChanged(nameof(SelectedProjekt));
                OnPropertyChanged(nameof(ChartData));
                OnPropertyChanged(nameof(IsNewProblemButtonEnabled));
            }
        }


        public IEnumerable<ISeries> ChartData
        {
            get
            {
                if (SelectedProjekt == null)
                {
                    return new List<ISeries>();
                }
                return SelectedProjekt.ChartData;
            }
        }

    }
}
