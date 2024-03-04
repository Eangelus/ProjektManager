using DocumentFormat.OpenXml.Bibliography;
using ProjektManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjektManager.ViewModel
{
    public class ViewModelMitarbeiterForm: ViewModelBase
    {
        public EditMode Mode { get; set; } = EditMode.CREATE;


        private Mitarbeiter _mitarbeiter;

        public Mitarbeiter Mitarbeiter
        {
            get
            {
                return _mitarbeiter;
            }
            set
            {
                _mitarbeiter = value;
                OnPropertyChanged(nameof(Mitarbeiter));
            }
        }


    }
}
