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

        //public string Bezug { get; set; }
        //public DateTime AuftrittsDatum { get; set; }
        //public int KW { get; set; }
        //public Abteilung? Abteilung { get; set; }
        //public string Name { get; set; }
        //public string Initiator { get; set; }
        //public string Kategorie { get; set; }
        //public string Thema { get; set; }
        //public string Maßnahme { get; set; }

        //public string Bewertung { get; set; }

        //public DateTime? Termin { get; set; }
        //public DateTime ReTermin { get; set; }
        //public string ProzessStatus { get; set; }
        //public string ProjektNr { get; set; }


        //public TimeSpan LengthOfTheProblem => ReTermin.Subtract(AuftrittsDatum);

        public ViewModelNewProblem() {

            LoadAbteilungen();
            CreateProblemCommand = new CreateProblemCommand(this);
        }

        public ICommand CreateProblemCommand { get; set; }

        private Projekt _selectedProjekt;

        public Projekt SelectedProjekt
        {
            get { return _selectedProjekt; }
            set { _selectedProjekt = value; }
        }


        private string myProzessStatus;

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



        private string myBewertung;

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



        private string myMaßnahme;

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



        private string myThema;

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

        public void LoadAbteilungen()
        {
            var _projektDBContextFactory = new ProjektDBContextFactory(App.CONSTRING);
            using (ProjektDBContext dbContext = _projektDBContextFactory.CreateDbContext())
            {
                Abteilungen.Clear();
                dbContext.Abteilungen.Select(p => AbteilungDTO.FromAbteilungDTO(p)).ToList().ForEach(Abteilungen.Add);
            }
        }


        private static ObservableCollection<Abteilung> _abteilungen = new ObservableCollection<Abteilung>();

        public static ObservableCollection<Abteilung> Abteilungen
        {
            get
            {
                return _abteilungen;
            }
            set
            {

                _abteilungen = value;
            }
        }


        private Abteilung myAbteilung;

        public Abteilung Abteilung
        {
            get
            {
                return myAbteilung;
            }
            set
            {
                myAbteilung = value;
                OnPropertyChanged(nameof(Abteilung));
            }
        }




        private string bezug;

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


        private string myName;

        public string Name
        {
            get
            {
                return myName;
            }
            set
            {
                myName = value;
                OnPropertyChanged(nameof(Name));
            }
        }


        private string myInitiator;

        public string Initiator
        {
            get
            {
                return myInitiator;
            }
            set
            {
                myInitiator = value;
                OnPropertyChanged(nameof(Initiator));
            }
        }


        private string myKategorie;

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
