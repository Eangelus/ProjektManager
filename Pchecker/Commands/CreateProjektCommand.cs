using Microsoft.EntityFrameworkCore.Diagnostics;
using ProjektManager.Models;
using ProjektManager.View;
using ProjektManager.ViewModel;
using ProjektManager.Commands;
using ProjektManager.DataBaseAPI;
using ProjektManager.DTOs;
using ProjektManager.Exceptions;
using ProjektManager.Models;
using ProjektManager.Services;
using ProjektManager.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;
using LiveChartsCore;

namespace ProjektManager.Commands
{
    
    /// <summary>
    /// ICommand Class for creatin a new Projekt 
    /// </summary>
    public class CreateProjektCommand : CommandBase
    {
        private readonly ViewModelCreateProjekt _viewModelCreateProjekt;
        private readonly NaviService naviService;

        public event EventHandler? CanExecuteChanged;

        public CreateProjektCommand(ViewModelCreateProjekt viewModelCreateProjekt,
            NaviService naviService)
        {
            _viewModelCreateProjekt = viewModelCreateProjekt;
            this.naviService = naviService;
            _viewModelCreateProjekt.PropertyChanged += OnViewModelPropertyChanged;
        }


        public override bool CanExecute(object? parameter)
        {

            return !string.IsNullOrEmpty(_viewModelCreateProjekt.MyAuftrageber) && base.CanExecute(parameter);
            //throw new NotImplementedException();
        }

        public override void Execute(object? parameter)
        {
            var windows = Application.Current.Windows;
            //Projekt projekt = new Projekt();

            Window? mainWindow = null;
            Window? projektWindow = null; 
            foreach (var window in windows)
            {
                if (window.GetType() == typeof(MainWindow) )
                {
                    mainWindow = (MainWindow)window;
                }
                if (window.GetType() == typeof(NewProjektWindow))
                {
                    projektWindow = (NewProjektWindow)window;
                }

            }
            //var dataContext = (ViewModelMainWindow) mainWindow.DataContext;
            //projekt = (Projekt)projektWindow.DataContext;
            //_jp.CreateProjekt(projekt);
           Projekt projekt = new Projekt(
                                  _viewModelCreateProjekt.MyAuftrageber,
                                  "0",
                                  DateTime.Now,
                                  _viewModelCreateProjekt.MyProjektLeiter,
                                  _viewModelCreateProjekt.MyDeadLine,
                                  _viewModelCreateProjekt.MyStartpunkt,
                                  new List<Problem>(),
                                  DateTime.Now,
                                  new List<ISeries>());

            try
            {
                var _projektDBContextFactory = new ProjektDBContextFactory(App.CONSTRING);
                using (ProjektDBContext dbContext = _projektDBContextFactory.CreateDbContext())
                {
                    dbContext.Add(ProjektDTO.ToProjektDTO(projekt));
                    dbContext.SaveChanges();
                }
                MessageBox.Show("Projekt wurde Angelegt!", "Erfolg", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (ProjektConflictException )
            {
                MessageBox.Show("Die eingaben Stimmen schon mit einen anderen Projekt überein !  btw Sollte niemals kommen, diese meldung!", "Error", MessageBoxButton.OK, MessageBoxImage.Error) ;
            }
            

            if(projektWindow is null)
            {
                return;
            }
            projektWindow.Close();
        }

        private void OnViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if(e.PropertyName == nameof(ViewModelCreateProjekt.MyAuftrageber))
            {
                OnCanExecuteChanged();
            }
        }


    }
}
