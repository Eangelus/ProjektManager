using LiveChartsCore.Kernel.Sketches;
using LiveChartsCore.Kernel;
using LiveChartsCore.SkiaSharpView.Drawing.Geometries;
using LiveChartsCore.SkiaSharpView.Painting;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore;
using ProjektManager.DataBaseAPI.ProjektProviders;
using ProjektManager.DataBaseAPI.Services.ProjektCreator;
using ProjektManager.Models;
using ProjektManager.Stores;
using ProjektManager.Commands;
using SkiaSharp;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows.Controls;
using System.Windows.Input;
using ProjektManager.Commands;
using ProjektManager.DataBaseAPI;
using ProjektManager.DTOs;

namespace ProjektManager.ViewModel
{
    public class ViewModelMainWindow : ViewModelBase
    {

        private readonly NaviStore _naviStore;
        public ViewModelBase CurrentViewModel => _naviStore.CurrentViewModel;

        private bool _isNewProjektButtonEnabled = true;

        public bool IsNewProjektButtonEnabled
        {
            get { return _isNewProjektButtonEnabled; }
            set { _isNewProjektButtonEnabled = value; OnPropertyChanged(nameof(IsNewProjektButtonEnabled)); }
        }


        public ICommand OpenWindowCommand { get; set; }

        public ICommand ShowChartDetailsCommand { get; set; }

        public ICommand PartSaveCommand { get; set; }

        private readonly IProjektCreator _projektCreator;
        private readonly IProjektproviders _projektproviders;

        private List<Projekt> projekte;

        public List<Projekt> Projekte
        {
            get { return projekte; }
            set { projekte = value; OnPropertyChanged(nameof(Projekte)); }
        }



            



        public ViewModelMainWindow()
        {
            OpenWindowCommand = new OpenWinNewProjektCommmand();
            PartSaveCommand = new PartSaveCommand(SaveChanges);
            LoadAllProjekte();
        }

        public ViewModelMainWindow(NaviStore naviStore)
        {
            _naviStore = naviStore;
            _naviStore.CurrentViewModelChanged += OnCurrentViewModelChanged;
            OpenWindowCommand = new OpenWinNewProjektCommmand();
            PartSaveCommand = new PartSaveCommand(SaveChanges);
            LoadAllProjekte();
        }


        private void OnCurrentViewModelChanged()
        {
            OnPropertyChanged(nameof(CurrentViewModel));
        }


        public async Task addProjekt(Projekt p)
        {

            //await _projektCreator.CreateProjekt(p);
            //projekte.Add(p);
            //SaveChanges(); //    <<<<<<<<<<<<<<<<<<   geht das?

            OnPropertyChanged(nameof(Projekte));
        }

        private void SaveChanges(DataGridRowEditEndingEventArgs entity)
        {

            Debug.WriteLine(entity);

            //DBContext db = new DBContext();
            //db.Entry(entity).State = EntityState.Modified;
            //db.SaveChanges();
        }


        public void LoadAllProjekte()
        {
            var _projektDBContextFactory = new ProjektDBContextFactory(App.CONSTRING);
            using (ProjektDBContext dbContext = _projektDBContextFactory.CreateDbContext())
            {
                 Projekte = dbContext.GetAllProjekts().Select(p => ProjektDTO.FromProjektDTO(p)).ToList();
            }


            foreach (var projekt in Projekte)
            {


                foreach (var series in projekt.ChartData)
                {
                    PieSeries<int> casted = (PieSeries<int>)series;
                    casted.ChartPointPointerDown += (sender, e) =>
                    {
                        IEnumerable<Problem> filtered = projekt.Probleme.Where(p =>
                        {
                            return p.ProzessStatus.Equals(series.Name);
                        });

                        FilterProblems = new ObservableCollection<Problem>(filtered);
                    };
                }

            }

        }

        public Problem? NewProb
        {
            get
            {
                if (SelectedProjekt == null)
                {
                    return null;
                }

                // Sortiert die Probleme des selektierten Projekts um das aktuellste zurückzugeben
                // Sortiert Probleme, die als Auftrittsdatum null haben, ganz nach unten.
                SelectedProjekt.Probleme.Sort((p1, p2) =>
                {
                    if (p1.AuftrittsDatum == null || p2.AuftrittsDatum == null)
                    {
                        return -1;
                    }
                    return p1.AuftrittsDatum.CompareTo(p2.AuftrittsDatum);
                });
                return SelectedProjekt.Probleme[0];
            }
        }



        private DateTime pickedTimeToFilterProblems = DateTime.Now.AddDays(-7);

        public DateTime PickedTimeToFilterProblems
        {
            get { return pickedTimeToFilterProblems; }
            set
            {
                if (pickedTimeToFilterProblems.Ticks == value.Ticks)
                {
                    return;
                }
                pickedTimeToFilterProblems = value;

                IEnumerable<Problem> problems = new List<Problem>();
                foreach (var projekt in Projekte)
                {
                    problems = problems.Concat(projekt.Probleme);
                }

                IEnumerable<Problem> result = problems.Where(f => f.AuftrittsDatum - PickedTimeToFilterProblems > TimeSpan.Zero);
                if (result.Count() == 0)
                {
                    FilterProblems = new ObservableCollection<Problem>();
                }
                FilterProblems = new ObservableCollection<Problem>(result);
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


        public ObservableCollection<Problem>? FilterProblemsOnID
        {
            get
            {
                if (SelectedProjekt != null)
                {
                    IEnumerable<Problem> problems = new List<Problem>();

                    problems = problems.Concat(SelectedProjekt.Probleme);
                    problems = problems.OrderBy(problem => problem.ProjektNr);
                    return new ObservableCollection<Problem>(problems.TakeLast(5));
                }
                else
                {
                    return null;
                }
            }
        }


        private Projekt selectedProjekt;

        public Projekt SelectedProjekt
        {
            get { return selectedProjekt; }
            set
            {
                selectedProjekt = value;
                OnPropertyChanged(nameof(SelectedProjekt));
                OnPropertyChanged(nameof(FilterProblemsOnID));
                OnPropertyChanged(nameof(ChartData));
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

        private void OnPointerDown(IChartView chart, ChartPoint<int, RoundedRectangleGeometry, LabelGeometry>? point)
        {
            if (point?.Visual is null) return;

        }

    }
}
