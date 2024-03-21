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
using ProjektManager.Services;
using DocumentFormat.OpenXml.Office2016.Drawing.ChartDrawing;
using DocumentFormat.OpenXml.Drawing;
using Irony.Parsing;
using DocumentFormat.OpenXml.InkML;

namespace ProjektManager.ViewModel
{
    public class ViewModelProjektWindow : ViewModelBase
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

        public ICommand OpenWinMitarbeiterCommand { get; set; }

        private ObservableCollection<Projekt> projekte;

        public ObservableCollection<Projekt> Projekte
        {
            get { return projekte; }
            set { projekte = value; OnPropertyChanged(nameof(Projekte)); }
        }


        private ObservableCollection<Problem> probleme = DatabankService.LoadAllProbleme();

        public ObservableCollection<Problem> Probleme
        {
            get { return probleme; }
            set { probleme = value; OnPropertyChanged(nameof(Probleme)); }
        }

        private static ObservableCollection<Mitarbeiter> _allMitarbeiter = new ObservableCollection<Mitarbeiter>();

        public static ObservableCollection<Mitarbeiter> AllMitarbeiter
        {
            get { return _allMitarbeiter; }
            set { _allMitarbeiter = value; }
        }


        public ViewModelProjektWindow()
        {
            OpenNewProjektWindowCommand = new OpenNewProjektWindowCommand();
            OpenNewProblemWindowCommand = new OpenNewProblemWindowCommand(this);
            OpenWinMitarbeiterCommand = new OpenWinMitarbeiterCommand();
            LoadAllProjekte();
            LoadAllMitarbeiter();
            LoadAllProblemeWithTime();
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

                if(SelectedProjekt == null)
                {
                    selectedProjekt = new Projekt();
                    selectedProjekt.Probleme = Probleme.ToList();
                }
                var suche =
                    from p in SelectedProjekt.Probleme
                    where p.Verantwortlicher == null ? false : p.Verantwortlicher.Name.Contains(searchText) ||
                    (p.Abteilung == null ? false : p.Abteilung.Contains(searchText)) ||
                    (p.Bewertung.Contains(searchText)) ||
                    (p.Termin == null ? false : p.Termin.ToString().Contains(searchText)) ||
                    (p.AuftrittsDatum.ToString().Contains(searchText)) ||
                    (p.Initiator == null ? false : p.Initiator.Name.Contains(searchText)) ||
                    (p.Maßnahme == null ? false : p.Maßnahme.Contains(searchText)) ||
                    (p.Bezug == null ? false : p.Bezug.Contains(searchText)) ||
                    (p.Kategorie == null ? false : p.Kategorie.Contains(searchText)) ||
                    (p.KW.ToString().Contains(searchText)) ||
                    (p.ProzessStatus == null ? false : p.ProzessStatus.Contains(searchText)) ||
                    (p.ReTermin == null ? false : p.ReTermin.ToString().Contains(searchText)) ||
                    (p.Thema == null ? false : p.Thema.Contains(searchText))
                    select p;

                suche.ToList();
                if(searchText == null || searchText.Length == 0)
                {
                    suche = SelectedProjekt.Probleme;
                }

                FilterProblems = new ObservableCollection<Problem>(suche.ToList());


                OnPropertyChanged(nameof(SearchText));
            }
        }


        


        public ObservableCollection<Projekt> LoadAllProjekte()
        {

            Projekte = DatabankService.LoadAllProjekte();


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
            return Projekte;
        }

        public void LoadAllMitarbeiter()
        {

            AllMitarbeiter.Clear();
            var mitarbeiter = DatabankService.loadAllMitarbeiter();
            foreach (var m in mitarbeiter)
            {
                AllMitarbeiter.Add(m);
            }
           
        }


        public void LoadAllProblemeWithTime()
        {
            FilterProblemsTime.Clear();
            var Probleme = DatabankService.LoadAllProbleme();
            foreach( var problem in Probleme)
            {
                 if(problem.Termin >= DateTime.Now.AddDays(-14))
                {
                    FilterProblemsTime.Add(problem);
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

        ObservableCollection<Problem> _filterProblemsTime = new ObservableCollection<Problem>();

        public ObservableCollection<Problem>? FilterProblemsTime
        {
            get
            {
                return _filterProblemsTime;
            }
            set
            {
                if (value == null)
                {
                    _filterProblemsTime = new ObservableCollection<Problem> { };
                    return;
                }
                _filterProblemsTime = value;
                OnPropertyChanged(nameof(FilterProblemsTime));
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
