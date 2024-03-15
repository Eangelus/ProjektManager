using ProjektManager.DataBaseAPI;
using ProjektManager.DTOs;
using ProjektManager.Models;
using ProjektManager.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjektManager.ViewModel
{
    public class ViewModelMitarbeiterListe : ViewModelBase
    {

        private ObservableCollection<Mitarbeiter> _mitarbeiter;

        public ObservableCollection<Mitarbeiter> Mitarbeiter
        {
            get { return _mitarbeiter; }
            set { _mitarbeiter = value; OnPropertyChanged(nameof(Mitarbeiter)); }
        }


        private ObservableCollection<Abteilung> _abteilungen = new ObservableCollection<Abteilung> {new Abteilung { Id = 01, Bezeichung = "Projektleitung", Jobs = new List<string> { "Projektleitung" }
                                                                                                                  },
                                                                                                    new Abteilung { Id = 02, Bezeichung = "Konstruktion", Jobs = new List<string> { "Konstruktion", "Dokumentation", "Besprechung (K)", "Beschaffung (K)" } },
                                                                                                    new Abteilung { Id = 03, Bezeichung = "Elektrotechnik", Jobs = new List<string> { "Programmierung", "E-Plan", "Dokumenation", "Besprechung (E)", "Beschaffung (E)" } },
                                                                                                    new Abteilung { Id = 04, Bezeichung = "Monatge", Jobs = new List<string> { "Montage mechanisch", "Montage elektronisch", "Montage mechanisch extern", "Monatge elektronisch extern"} }};

        public ObservableCollection<Abteilung> Abteilungen
        {
            get { return _abteilungen; }
            set { _abteilungen = value; OnPropertyChanged(nameof(Abteilungen)); }
        }



        public ViewModelMitarbeiterListe()
        {
            _mitarbeiter = DatabankService.loadAllMitarbeiter();
           
            foreach(Mitarbeiter m in  _mitarbeiter)
            {
                foreach (Abteilung a in _abteilungen)
                {
                    if (a.Bezeichung == m.InAbteilung)
                    {
                        a.AddMitarbeiter(m);
                    }
                }
            }
        }


    }
}
