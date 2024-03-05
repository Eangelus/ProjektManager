using DocumentFormat.OpenXml.Bibliography;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using ProjektManager.Commands;
using ProjektManager.DataBaseAPI;
using ProjektManager.DTOs;
using ProjektManager.Models;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace ProjektManager.ViewModel
{
    public class ViewModelNewProblem : ViewModelBase
    {

        public static ObservableCollection<Mitarbeiter> AllMitarbeiter = new ObservableCollection<Mitarbeiter>();

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

        public ViewModelNewProblem() {
            CreateProblemCommand = new CreateProblemCommand(this);
            LoadAllMitarbeiter();
        }

        public ICommand CreateProblemCommand { get; set; }

        private ObservableCollection<Mitarbeiter> _listForEmail = new ObservableCollection<Mitarbeiter>();

        public ObservableCollection<Mitarbeiter> ListForEmail
        {
            get
            {
                return _listForEmail;
            }
            set
            {
                _listForEmail = value;
                OnPropertyChanged(nameof(ListForEmail));
            }
        }




        private Mitarbeiter _selected4EmailVer;

        public Mitarbeiter Selected4EmailVer
        {
            get
            {
                return _selected4EmailVer;
            }
            set
            {
                _selected4EmailVer = value;
                ListForEmail.Add(value);
                OnPropertyChanged(nameof(Selected4EmailVer));
                OnPropertyChanged(nameof(ListForEmail));


            }
        }
        private Mitarbeiter _selected4EmailInfo;

        public Mitarbeiter Selected4EmailInfo
        {
            get
            {
                return _selected4EmailInfo;
            }
            set
            {
                _selected4EmailInfo = value;
                ListForEmail.Add(value);
                OnPropertyChanged(nameof(Selected4EmailInfo));
                OnPropertyChanged(nameof(ListForEmail));


            }
        }



        private Mitarbeiter _selectedVerantwortlicher;

        public Mitarbeiter SelectedVerantwortlicher
        {
            get
            {
                return _selectedVerantwortlicher;
            }
            set
            {
                _selectedVerantwortlicher = value;
                OnPropertyChanged(nameof(SelectedVerantwortlicher));
            }
        }


        private Mitarbeiter _selectedInitiator;

        public Mitarbeiter SelectedInitator
        {
            get
            {
                return _selectedInitiator;
            }
            set
            {
                _selectedInitiator = value;
                OnPropertyChanged(nameof(SelectedInitator));
            }
        }





        private Projekt _selectedProjekt;

        public Projekt SelectedProjekt
        {
            get { return _selectedProjekt; }
            set { _selectedProjekt = value; }
        }


        private string myProzessStatus = "Abgeschlossen";

        public string ProzessStatus
        {
            get
            {
                return myProzessStatus;
            }
            set
            {
                myProzessStatus = value;
                OnPropertyChanged(nameof(Bewertung));
            }
        }


        private DateTime myReTermin = DateTime.Now;

        public DateTime ReTermin
        {
            get
            {
                return myReTermin;
            }
            set
            {
                myReTermin = value;
                OnPropertyChanged(nameof(ReTermin));
            }
        }



        private DateTime myTermin = DateTime.Now;

        public DateTime Termin
        {
            get
            {
                return myTermin;
            }
            set
            {
                myTermin = value;
                OnPropertyChanged(nameof(Termin));
            }
        }



        private string myBewertung = "askldöjf";

        public string Bewertung
        {
            get
            {
                return myBewertung;
            }
            set
            {
                myBewertung = value;
                OnPropertyChanged(nameof(Bewertung));
            }
        }



        private string myMaßnahme = "lkasjdf";

        public string Maßnahme
        {
            get
            {
                return myMaßnahme;
            }
            set
            {
                myMaßnahme = value;
                OnPropertyChanged(nameof(Maßnahme));
            }
        }



        private string myThema = "alkdsjfö";

        public string Thema
        {
            get
            {
                return myThema;
            }
            set
            {
                myThema = value;
                OnPropertyChanged(nameof(Thema));
            }
        }

        private string _abteilung = "alkdsjfö";

        public string Abteilung
        {
            get
            {
                return _abteilung;
            }
            set
            {
                _abteilung = value;
                OnPropertyChanged(nameof(Abteilung));
            }
        }

        private string bezug = "alkdsjfö";

        public string Bezug
        {
            get
            {
                return bezug;
            }
            set
            {
                bezug = value;
                OnPropertyChanged(nameof(Bezug));
            }
        }


        private DateTime myAuftrittsDatum = DateTime.Now;

        public DateTime AuftrittsDatum
        {
            get
            {
                return myAuftrittsDatum;
            }
            set
            {
                myAuftrittsDatum = value;
                OnPropertyChanged(nameof(AuftrittsDatum));
            }
        }


        private Mitarbeiter _verantwortlicher;

        public Mitarbeiter Verantwortlicher
        {
            get
            {
                return _verantwortlicher;
            }
            set
            {
                _verantwortlicher = value;
                OnPropertyChanged(nameof(Verantwortlicher));
            }
        }


        private Mitarbeiter _initiator;

        public Mitarbeiter Initiator
        {
            get
            {
                return _initiator;
            }
            set
            {
                _initiator = value;
                OnPropertyChanged(nameof(Initiator));
            }
        }


        private string myKategorie = "akljdsf";

        public string Kategorie
        {
            get
            {
                return myKategorie;
            }
            set
            {
                myKategorie = value;
                OnPropertyChanged(nameof(Kategorie));
            }
        }



    }
}
