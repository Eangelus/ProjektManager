﻿using ProjektManager.DataBaseAPI;
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



        public ViewModelMitarbeiterListe()
        {
            _mitarbeiter = DatabankService.loadAllMitarbeiter();
        }


    }
}
