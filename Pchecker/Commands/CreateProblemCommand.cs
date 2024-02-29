using ProjektManager.Models;
using ProjektManager.View;
using ProjektManager.ViewModel;
using ProjektManager.DataBaseAPI;
using ProjektManager.DTOs;
using System.ComponentModel;
using System.Windows;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;

namespace ProjektManager.Commands
{

    /// <summary>
    /// ICommand Class for creatin a new Projekt 
    /// </summary>
    public class CreateProblemCommand : CommandBase
    {
        private readonly ViewModelNewProblem _viewModelCreateProblem;

        public event EventHandler? CanExecuteChanged;

        public CreateProblemCommand(ViewModelNewProblem viewModelCreateProblem)
        {
            _viewModelCreateProblem = viewModelCreateProblem;
            _viewModelCreateProblem.PropertyChanged += OnViewModelPropertyChanged;

            var windows = Application.Current.Windows;
            Window? problemWindow = null;
            Window? mainWindow = null;
            foreach (var window in windows)
            {
                if (window.GetType() == typeof(NewProblemWindow))
                {
                    problemWindow = (NewProblemWindow)window;
                }
                if (window.GetType() == typeof(MainWindow))
                {
                    mainWindow = (MainWindow)window;
                }
            }



        }

        public override bool CanExecute(object? parameter)
        {
            bool result = !string.IsNullOrEmpty(_viewModelCreateProblem.Name) && !string.IsNullOrEmpty(_viewModelCreateProblem.Thema) && !string.IsNullOrEmpty(_viewModelCreateProblem.Maßnahme) && !string.IsNullOrEmpty(_viewModelCreateProblem.Bezug) && !string.IsNullOrEmpty(_viewModelCreateProblem.Bewertung) && !string.IsNullOrEmpty(_viewModelCreateProblem.Initiator) && !string.IsNullOrEmpty(_viewModelCreateProblem.Kategorie) && (_viewModelCreateProblem.Abteilung != null &&!string.IsNullOrEmpty(_viewModelCreateProblem.Abteilung.AbBezeichung)) && !string.IsNullOrEmpty(_viewModelCreateProblem.ProzessStatus) && base.CanExecute(parameter);
            return result;
        }

        public override void Execute(object? parameter)
        {
            var windows = Application.Current.Windows;

            Window? problemWindow = null;
            Window? mainWindow = null;
            foreach (var window in windows)
            {
                if (window.GetType() == typeof(NewProblemWindow))
                {
                    problemWindow = (NewProblemWindow)window;
                }
                if (window.GetType() == typeof(MainWindow))
                {
                    mainWindow = (MainWindow)window;
                }

            }

            var viewModelNewProblem = (problemWindow.DataContext as ViewModelNewProblem);

            Problem problem = new Problem(null, viewModelNewProblem.Bezug, viewModelNewProblem.AuftrittsDatum, viewModelNewProblem.Abteilung, viewModelNewProblem.Name, viewModelNewProblem.Initiator, viewModelNewProblem.Kategorie, viewModelNewProblem.Thema, viewModelNewProblem.Maßnahme, viewModelNewProblem.Bewertung, viewModelNewProblem.Termin, viewModelNewProblem.ReTermin, viewModelNewProblem.ProzessStatus);


            var _projektDBContextFactory = new ProjektDBContextFactory(App.CONSTRING);
            try
            {
                using (ProjektDBContext dbContext = _projektDBContextFactory.CreateDbContext())
                {
                    var projektDTO = dbContext.Projekte.Include("Probleme").Single(p => p.ProjektNr == viewModelNewProblem.SelectedProjekt.ProjektNr);
                    var problemDTO = ProblemDTO.ToProblemDTO(problem);

                    if(projektDTO.Probleme == null)
                    {
                        projektDTO.Probleme = new List<ProblemDTO>();
                    }



                    //projektDTO.Probleme.Add(problemDTO);
                    var entity = dbContext.Attach(projektDTO);
                    //dbContext.(projektDTO.Probleme).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                    dbContext.Entry(problemDTO.Abteilung).State = Microsoft.EntityFrameworkCore.EntityState.Unchanged;
                    entity.Entity.Probleme.Add(problemDTO);

                    dbContext.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Fehler beim Anlegen des Problems.", "Fehler", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            MessageBox.Show("Problem wurde Angelegt!", "Erfolg", MessageBoxButton.OK, MessageBoxImage.Information);


            (mainWindow.DataContext as ViewModelMainWindow).LoadAllProjekte();
            (mainWindow.DataContext as ViewModelMainWindow).FilterProblems = new ObservableCollection<Problem>(viewModelNewProblem.SelectedProjekt.Probleme);
            (mainWindow.DataContext as ViewModelMainWindow).SearchText = String.Empty;


            if (problemWindow == null)
            {
                return;
            }
            problemWindow.Close();
        }

        private void OnViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            OnCanExecuteChanged();
        }


    }
}
