using ProjektManager.Models;
using ProjektManager.View;
using ProjektManager.ViewModel;
using ProjektManager.DataBaseAPI;
using ProjektManager.DTOs;
using System.ComponentModel;
using System.Windows;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.Net.Mail;
using System.Net;
using System.Security.Cryptography.X509Certificates;


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
                if (window.GetType() == typeof(ProjektWindow))
                {
                    mainWindow = (ProjektWindow)window;
                }
            }


        }

        public override bool CanExecute(object? parameter)
        {
            bool result = (_viewModelCreateProblem.SelectedVerantwortlicher != null && !string.IsNullOrEmpty(_viewModelCreateProblem.SelectedVerantwortlicher.Name)) && !string.IsNullOrEmpty(_viewModelCreateProblem.Thema) && !string.IsNullOrEmpty(_viewModelCreateProblem.Maßnahme) && !string.IsNullOrEmpty(_viewModelCreateProblem.Bezug) && !string.IsNullOrEmpty(_viewModelCreateProblem.Bewertung) && (_viewModelCreateProblem.SelectedInitator != null && !string.IsNullOrEmpty(_viewModelCreateProblem.SelectedInitator.Name)) && !string.IsNullOrEmpty(_viewModelCreateProblem.Kategorie) && !string.IsNullOrEmpty(_viewModelCreateProblem.ProzessStatus) && base.CanExecute(parameter);
            return result;
        }

        public override void Execute(object? parameter)
        {
            var windows = Application.Current.Windows;

            Window? problemWindow = null;
            Window? ProjektWindow = null;
            foreach (var window in windows)
            {
                if (window.GetType() == typeof(NewProblemWindow))
                {
                    problemWindow = (NewProblemWindow)window;
                }
                if (window.GetType() == typeof(ProjektWindow))
                {
                    ProjektWindow = (ProjektWindow)window;
                }

            }

            var viewModelNewProblem = (problemWindow.DataContext as ViewModelNewProblem);

            Problem problem = new Problem(null, viewModelNewProblem.Bezug, viewModelNewProblem.AuftrittsDatum, viewModelNewProblem.Abteilung,viewModelNewProblem.SelectedVerantwortlicher, viewModelNewProblem.SelectedInitator, viewModelNewProblem.Kategorie, viewModelNewProblem.Thema, viewModelNewProblem.Maßnahme, viewModelNewProblem.Bewertung, viewModelNewProblem.Termin, viewModelNewProblem.ReTermin, viewModelNewProblem.ProzessStatus);
            

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
                    //dbContext.Entry(problemDTO.Abteilung).State = Microsoft.EntityFrameworkCore.EntityState.Unchanged;
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


            (ProjektWindow.DataContext as ViewModelProjektWindow).LoadAllProjekte();
            (ProjektWindow.DataContext as ViewModelProjektWindow).FilterProblems = new ObservableCollection<Problem>(viewModelNewProblem.SelectedProjekt.Probleme);
            (ProjektWindow.DataContext as ViewModelProjektWindow).SearchText = String.Empty;

            SendEmailForNewProblem(viewModelNewProblem.ListForEmail, problem);


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


        private void SendEmailForNewProblem(ObservableCollection<Mitarbeiter> ListOfEmail, Problem problem)
        {

            foreach ( Mitarbeiter mitarbeiter in ListOfEmail )
            {
                if(mitarbeiter.Email == null)
                {
                    string yourReturnedValue = Microsoft.VisualBasic.Interaction.InputBox("Mitarbeiter hat keine Email hinterlegt, wollen sie das nachholen?", "Keine Email", "Standardwert", 0, 0);

                }
                using( SmtpClient smtpClient = new SmtpClient() )
                {
                    smtpClient.Host = "jp-mail.jpindustrie.local"; // 192.168.23.4
                    smtpClient.Port = 587;
                    smtpClient.EnableSsl = true;
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                    var basicCredentail = new NetworkCredential("t.bernecker@jp-industrieanlagen.de", "TB1982!");
                    smtpClient.Credentials = basicCredentail;
                    smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;

                    X509CertificateCollection certificates= new X509CertificateCollection();
                    X509Store store = new X509Store(StoreName.TrustedPublisher, StoreLocation.CurrentUser);

                    store.Open(OpenFlags.ReadOnly);

                    foreach (X509Certificate2 certificate in store.Certificates)
                    {
                        certificates.Add( certificate );    
                    }

                    smtpClient.ClientCertificates.AddRange( certificates );
                    

                    using (MailMessage message = new MailMessage())
                    {
                        MailAddress fromAdress = new MailAddress("t.bernecker@jp-industrieanlagen.de");
                        message.From = fromAdress;
                        message.To.Add("t.bernecker@jp-industrieanlagen.de");
                        message.Subject = $"{problem.AuftrittsDatum } + {problem.Initiator} + {problem.Kategorie}";
                        message.IsBodyHtml = true;
                        message.Body = $"<h1> Ein Problem wurde erstellt :{problem.Thema} von {problem.Initiator} <h1><br>" +
                            $" vorrauslich bis {problem.Termin}";

                        try
                        {
                            smtpClient.Send(message);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Exception caught in CreateTestMessage2(): {0}",
                                ex.ToString());
                        }

                    }




                }
                
                // Credentials are necessary if the server requires the client
                // to authenticate before it will send email on the client's behalf.
                



            }

        }

    }
}
