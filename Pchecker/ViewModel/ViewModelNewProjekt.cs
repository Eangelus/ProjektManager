using CommunityToolkit.Mvvm.Input;
using Pchecker.Commands;
using Pchecker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Pchecker.ViewModel
{
    public class ViewModelNewProjekt : ViewModelBase
    {


        private string auftraggeber = "";

        public string MyAuftrageber
        {
            get { return auftraggeber; }
            set { auftraggeber = value; OnPropertyChanged(nameof(MyAuftrageber)); }
        }

        private int projektNr;

        public int myProjektNr
        {
            get { return projektNr; }
            set { projektNr = value; OnPropertyChanged(nameof(myProjektNr)); }
        }


        private DateTime stand;

        public DateTime MyStand
        {
            get { return stand; }
            set { stand = value; OnPropertyChanged(nameof(MyStand)); }
        }


        private int status;

        public int myStatus
        {
            get { return status; }
            set { this.status = value; OnPropertyChanged(nameof(myStatus)); }

        }


        
        private DateTime startpunkt = DateTime.Now;

        public DateTime MyStartpunkt
        {
            get { return startpunkt; }
            set { startpunkt = value; OnPropertyChanged(nameof(MyStartpunkt)); }
        }





        private Mitarbeiter? mitarbeiter;

        public Mitarbeiter? MyMitarbeiter
        {
            get { return mitarbeiter; }
            set { mitarbeiter = value; OnPropertyChanged(nameof(MyMitarbeiter)); }
        }

        private DateTime deadLine = DateTime.Now.AddYears(1);

        public DateTime MyDeadLine 
        {
            get { return deadLine; }
            set { deadLine = value; OnPropertyChanged(nameof(MyDeadLine)); }
        }


        private string projektLeiter = "";

        public string MyProjektLeiter
        {
            get { return projektLeiter; }
            set { projektLeiter = value; OnPropertyChanged(nameof(MyProjektLeiter)); }
        }



        private List<Abteilung> abteilungenList = new List<Abteilung> { };
        private List<string> kategorien = new List<string>() { "I", "FF", "LF", "SF", "BBÜ" };


        public Projekt Projekt
        {
            get
            {
                return new Projekt(MyStartpunkt, MyProjektLeiter, MyAuftrageber, MyDeadLine);
            }
        }

        public CreateProjektCommand createProjekt { get; set; }



        public ViewModelNewProjekt()
        {
            this.createProjekt = new CreateProjektCommand();

        }
    }
}
