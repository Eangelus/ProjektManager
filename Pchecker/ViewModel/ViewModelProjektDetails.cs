using Pchecker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pchecker.ViewModel
{
    public class ViewModelProjektDetails: ViewModelBase
    {
		private Projekt? _projekt;

		public Projekt Projekt
		{
			get { return _projekt; }
			set { 
				_projekt = value;
				OnPropertyChanged(nameof(Projekt));
			}
		}




	}
}
