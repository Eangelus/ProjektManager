using Microsoft.EntityFrameworkCore;
using ProjektManager.DataBaseAPI;
using ProjektManager.DTOs;
using ProjektManager.Models;
using ProjektManager.View;
using ProjektManager.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ProjektManager.Commands
{
    public class CreateBuchungCommand : CommandBase
    {

        private readonly ViewModelNewStundenbuchung _ViewModelCreateStunden;

        public event EventHandler? CanExecuteChanged;


        public CreateBuchungCommand(ViewModelNewStundenbuchung viewModelCreateStunden)
        {
            _ViewModelCreateStunden = viewModelCreateStunden;
            _ViewModelCreateStunden.PropertyChanged += OnViewModelPropertyChanged;

            //var windows = Application.Current.Windows;
            //Window? buchungWindow = null;
            //Window? newBuchungWindow = null;
            //foreach ( var window in windows)
            //{
            //    if(window.GetType() == typeof(NewStundenBuchungWindow))
            //    {
            //        newBuchungWindow = (NewStundenBuchungWindow)window;
            //    }
                
            //    if(window.GetType() == typeof(StundenBuchungWindow))
            //    {
            //        newBuchungWindow = (StundenbuchungControl)window;
            //    }

            //}
        }

        private void OnViewModelPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            OnCanExecuteChanged();
        }

        public override void Execute(object? parameter)
        {
            var windows = Application.Current.Windows;

            Window? BuchungWindow = null;
            foreach (var window in windows)
            {
                if(window.GetType() == typeof(NewStundenBuchungWindow)) 
                {
                    BuchungWindow =(NewStundenBuchungWindow)window;
                }
            }
            

            Stundenbuchung buchung = new Stundenbuchung(null, DateTime.Now, _ViewModelCreateStunden.SelectedMitarbeiter, _ViewModelCreateStunden.Details, _ViewModelCreateStunden.SelectedProjekt, _ViewModelCreateStunden.StartTime, _ViewModelCreateStunden.Stunden, _ViewModelCreateStunden.Minuten);



            var _projektDBContextFactory = new ProjektDBContextFactory(App.CONSTRING);
            try
            {
                using (ProjektDBContext dbContext = _projektDBContextFactory.CreateDbContext())
                {
                    var StundenbuchungenDTO = StundenbuchungDTO.ToStundenbuchungDTO(buchung);


                    dbContext.Entry(StundenbuchungenDTO.Mitarbeiter).State = EntityState.Unchanged;
                    dbContext.Entry(StundenbuchungenDTO.Projekt).State = EntityState.Unchanged;
                    dbContext.Add(StundenbuchungenDTO);
                    //dbContext.(projektDTO.Probleme).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                    //dbContext.Entry(problemDTO.Abteilung).State = Microsoft.EntityFrameworkCore.EntityState.Unchanged;

                    dbContext.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Fehler beim erstellen der Buchung der Arbeitszeit.", "Fehler", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            MessageBox.Show("Arbeitszeit wurde gebucht!", "Erfolg", MessageBoxButton.OK, MessageBoxImage.Information);



            if (BuchungWindow == null)
            {
                return;
            }
            BuchungWindow.Close();
        }

        public override bool CanExecute(object? parameter)
        {
            Stundenbuchung buchung = new Stundenbuchung(null, DateTime.Now, _ViewModelCreateStunden.SelectedMitarbeiter, _ViewModelCreateStunden.Details, _ViewModelCreateStunden.SelectedProjekt, _ViewModelCreateStunden.StartTime, _ViewModelCreateStunden.Stunden, _ViewModelCreateStunden.Minuten);

            bool result = buchung.Mitarbeiter != null  && buchung.Projekt != null && !string.IsNullOrEmpty(buchung.Projekt.ProjektNr) && buchung.StartTime.Ticks != 0 && buchung.Stunden != 0 && base.CanExecute(parameter);
            return result;
        }

    }
}
