using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
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


        private ObservableCollection<Abteilung> _abteilungen = Abteilung.CreateAllAbteilungen();
        public ObservableCollection<Abteilung> Abteilungen
        {
            get { return _abteilungen; }
            set { _abteilungen = value; OnPropertyChanged(nameof(Abteilungen)); }
        }

        public Mitarbeiter AddMitarbeiterEmail(Mitarbeiter mitarbeiter)
        {
            if (mitarbeiter.Vorname == null || String.IsNullOrEmpty(mitarbeiter.Vorname))
            {
                return mitarbeiter;
            }
            else
            {
                mitarbeiter.Email = $"{mitarbeiter.Vorname[0]}.{mitarbeiter.Name}@jp-industrieanlagen.de";
                return mitarbeiter;
            }
        }

        public ViewModelMitarbeiterListe()
        {
            _mitarbeiter = DatabankService.loadAllMitarbeiter();
           
            foreach(Mitarbeiter m in  _mitarbeiter)

            {
                if (!(m.Vorname == null || String.IsNullOrEmpty(m.Vorname)))
                {
                    m.Email = $"{m.Vorname[0]}.{m.Name}@jp-industrieanlagen.de";
                    var _projektDBContextFactory = new ProjektDBContextFactory(App.CONSTRING);
                    using (ProjektDBContext dbContext = _projektDBContextFactory.CreateDbContext())
                    {

                        var result = dbContext.Update(MitarbeiterDTO.ToMitarbeiterDTO(m));
                        dbContext.SaveChanges();
                    }
                }

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
