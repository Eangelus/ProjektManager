using LiveChartsCore;
using LiveChartsCore.Kernel.Sketches;
using LiveChartsCore.Kernel;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Drawing.Geometries;
using LiveChartsCore.SkiaSharpView.Painting;
using Pchecker.Commands;
using Pchecker.Models;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ProjektManager.Logic;
using ProjektManager.DataBaseAPI;
using System.Drawing;

namespace Pchecker.ViewModel
{
    public class ViewModelMainWindow : ViewModelBase
    {


        public ICommand OpenWindowCommand { get; private set; }

        public ICommand ShowChartDetailsCommand { get; private set; }
        public ViewModelMainWindow()
        {

            projekte = new ObservableCollection<Projekt>(ExcelConnection.ReadAllExcelFiles());

            foreach (var projekt in projekte)
            {


                foreach (var series in projekt.ProblemStatus)
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

            projekte.Add(new Projekt(DateTime.Now, "ich", "JP", DateTime.Now.AddYears(2)));
            

            
            OpenWindowCommand = new OpenWinNewProjektCommmand();

        }




        private bool _isNewProjektButtonEnabled = true;

        public bool IsNewProjektButtonEnabled
        {
            get { return _isNewProjektButtonEnabled; }
            set { _isNewProjektButtonEnabled = value; OnPropertyChanged(nameof(IsNewProjektButtonEnabled)); }
        }


        private ObservableCollection<Projekt> projekte;


        public ObservableCollection<Projekt> Projekte
        {
            get { return projekte; }
            set { projekte = value; }
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
                    if (p1.AuftritsDatum == null || p2.AuftritsDatum == null)
                    {
                        return -1;
                    }
                    return p1.AuftritsDatum.Value.CompareTo(p2.AuftritsDatum);
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

                IEnumerable<Problem> result = problems.Where(f => f.AuftritsDatum - PickedTimeToFilterProblems > TimeSpan.Zero);
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
                    problems = problems.OrderBy(problem => problem.PID);
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
                return SelectedProjekt.ProblemStatus;
            }
        }

        private void OnPointerDown(IChartView chart, ChartPoint<int, RoundedRectangleGeometry, LabelGeometry>? point)
        {
            if (point?.Visual is null) return;

        }


        public void addProjekt(Projekt p)
        {
            projekte.Add(p);
            OnPropertyChanged(nameof(Projekte));
        }


    }
}
