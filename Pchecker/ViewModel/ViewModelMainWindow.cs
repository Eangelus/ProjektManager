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

namespace ProjektManager.ViewModel
{
    public class ViewModelMainWindow : ViewModelBase
    {

        private readonly NaviStore _naviStore;
        public ViewModelBase CurrentViewModel => _naviStore.CurrentViewModel;

        public static ObservableCollection<Abteilung> UniqueAbteilungen = new ObservableCollection<Abteilung>();



        private bool _isNewProjektButtonEnabled = true;

        public bool IsNewProjektButtonEnabled
        {
            get { return _isNewProjektButtonEnabled; }
            set { _isNewProjektButtonEnabled = value; OnPropertyChanged(nameof(IsNewProjektButtonEnabled)); }
        }


        public ICommand OpenWindowCommand { get; set; }

        public ICommand ShowChartDetailsCommand { get; set; }

        public ICommand PartSaveCommand { get; set; }

        private ObservableCollection<Projekt> projekte;

        public ObservableCollection<Projekt> Projekte
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
                    where p.Name.Contains(searchText) || 
                    (p.Abteilung == null ? false : p.Abteilung.AbBezeichung.Contains(searchText)) ||
                    (p.Bewertung.Contains(searchText)) ||
                    (p.Termin == null ? false : p.Termin.ToString().Contains(searchText)) ||
                    (p.AuftrittsDatum.ToString().Contains(searchText)) ||
                    (p.ProjektNr.Contains(searchText)) ||
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

            

        private void OnCurrentViewModelChanged()
        {
            OnPropertyChanged(nameof(CurrentViewModel));
        }


        public async Task addProjekt(Projekt p)
        {
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
                Projekte = new ObservableCollection<Projekt>(dbContext.GetAllProjekts().Select(p => ProjektDTO.FromProjektDTO(p)).ToList());
            }

            if (SelectedProjekt != null)
            {
                var newSelectedProjekt = Projekte.Single(p => SelectedProjekt.ProjektNr == p.ProjektNr);
                SelectedProjekt = newSelectedProjekt;
            }

            HashSet<Abteilung> uniqueAbteilungenSet = new HashSet<Abteilung>();


            foreach (var projekt in Projekte)
            {

                foreach(var abteilung in projekt.Abteilungen)
                {
                    uniqueAbteilungenSet.Add(abteilung);
                }

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

            var abteilungenList = uniqueAbteilungenSet.ToList();
            if(abteilungenList != null)
            {
                foreach(var a in abteilungenList)
                {
                    UniqueAbteilungen.Add(a);
                }
                OnPropertyChanged(nameof(UniqueAbteilungen));
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




        private Projekt selectedProjekt;

        public Projekt SelectedProjekt
        {
            get { return selectedProjekt; }
            set
            {
                selectedProjekt = value;
                OnPropertyChanged(nameof(SelectedProjekt));
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

    }
}
