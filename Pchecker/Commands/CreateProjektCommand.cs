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
        private readonly ViewModelNewProjekt _viewModelCreateProjekt;

        public event EventHandler? CanExecuteChanged;

        public CreateProjektCommand(ViewModelNewProjekt viewModelCreateProjekt)
        {
            _viewModelCreateProjekt = viewModelCreateProjekt;
            _viewModelCreateProjekt.PropertyChanged += OnViewModelPropertyChanged;

            var windows = Application.Current.Windows;
            Window? projektWindow = null;
            Window? mainWindow = null;
            foreach (var window in windows)
            {
                if (window.GetType() == typeof(NewProjektWindow))
                {
                    projektWindow = (NewProjektWindow)window;
                }
                if (window.GetType() == typeof(ProjektWindow))
                {
                    mainWindow = (ProjektWindow)window;
                }
            }
        }

        public override bool CanExecute(object? parameter)
        {
            bool result = !string.IsNullOrEmpty(_viewModelCreateProjekt.MyProjektNummer) && !string.IsNullOrEmpty(_viewModelCreateProjekt.MyAuftrageber) && !string.IsNullOrEmpty(_viewModelCreateProjekt.MyProjektLeiter) &&  base.CanExecute(parameter);
            return result;
        }

        public override void Execute(object? parameter)
        {
            var windows = Application.Current.Windows;

            Window? projektWindow = null;
            Window? mainWindow = null;
            foreach (var window in windows)
            {
                if (window.GetType() == typeof(NewProjektWindow))
                {
                    projektWindow = (NewProjektWindow)window;
                }
                if (window.GetType() == typeof(ProjektWindow))
                {
                    mainWindow = (ProjektWindow)window;
                }

            }

            Projekt projekt = new Projekt(
                                   _viewModelCreateProjekt.MyAuftrageber,
                                   _viewModelCreateProjekt.MyProjektNummer,
                                   _viewModelCreateProjekt.MyStand,
                                   _viewModelCreateProjekt.MyProjektLeiter,
                                   _viewModelCreateProjekt.MyDeadLine,
                                   _viewModelCreateProjekt.MyStartpunkt,
                                   new List<Problem>(),
                                   DateTime.Now,
                                   new List<ISeries>());

            var _projektDBContextFactory = new ProjektDBContextFactory(App.CONSTRING);
            try
            {
                using (ProjektDBContext dbContext = _projektDBContextFactory.CreateDbContext())
                {
                    dbContext.Add(ProjektDTO.ToProjektDTO(projekt));
                    dbContext.SaveChanges();
                }
            }catch (Exception ex)
            {
                // Duplicate entry '0' for key 'projekte.PRIMARY'
                if (ex.InnerException != null && ex.InnerException.Message.Contains("Duplicate entry") && ex.InnerException.Message.Contains("for key"))
                {
                    MessageBox.Show("Fehler beim Anlegen des Projekts. -> Projekt exisitert bereits.", "Fehler", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                MessageBox.Show("Fehler beim Anlegen des Projekts.", "Fehler", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            MessageBox.Show("Projekt wurde Angelegt!", "Erfolg", MessageBoxButton.OK, MessageBoxImage.Information);

            (mainWindow.DataContext as ViewModelProjektWindow).Projekte.Add(projekt);
            if (projektWindow == null)
            {
                return;
            }
            projektWindow.Close();
        }

        private void OnViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            OnCanExecuteChanged();
        }


    }
}
