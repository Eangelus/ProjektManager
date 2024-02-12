using CommunityToolkit.Mvvm.Input;
using LiveChartsCore;
using ProjektManager.Commands;
using ProjektManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ProjektManager.ViewModel
{
    public class ViewModelProjekt : ViewModelBase
    {
        public ViewModelProjekt()
        {
            
        }

        private readonly Projekt _projekt;

        public string ProjektNr => _projekt.ProjektNr.ToString();
        public string Auftraggeber => _projekt.Auftraggeber.ToString();
        public string ProjektLeiter => _projekt.ProjektLeiter;


        public List<Abteilung> Abteilungen => _projekt.Abteilungen;

        public DateTime Start => _projekt.Startpunkt.Date;

        public DateTime? Deadline => (DateTime)_projekt.DeadLine;

        public DateTime Stand => _projekt.Stand.Date;
        public List<Problem> Probleme => _projekt.Probleme;

        public IEnumerable<ISeries> ProblemStatus => _projekt.ChartData;





        private List<Abteilung> abteilungenList = new List<Abteilung> { };
        private List<string> kategorien = new List<string>() { "I", "FF", "LF", "SF", "BBÜ" };


        //public Projekt Projekt
        //{
        //    get
        //    {
        //        return new Projekt(MyStartpunkt, MyProjektLeiter, MyAuftrageber, MyDeadLine);
        //    }
        //}

        //public CreateProjektCommand createProjekt { get; set; }



        public ViewModelProjekt(Projekt projekt)
        {
            this._projekt = projekt;

        }
    }
}
