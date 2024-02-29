using CommunityToolkit.Mvvm.Input;
using Microsoft.EntityFrameworkCore.Query.Internal;
using ProjektManager.Commands;
using ProjektManager.DataBaseAPI;
using ProjektManager.DTOs;
using ProjektManager.Models;
using ProjektManager.Services;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace ProjektManager.ViewModel
{
    public class ViewModelNewProjekt : ViewModelBase
    {

        public ViewModelNewProjekt()
        {
            
            CreateProjektCommand = new CreateProjektCommand(this);
        }

        public ICommand CreateProjektCommand { get; }

		private string _Auftrageber;

		public string MyAuftrageber
        {
			get
			{
				return _Auftrageber;
			}
			set
			{
                _Auftrageber = value;
				OnPropertyChanged(nameof(MyAuftrageber));
			}
		}

		private string _myProjektNummer;

		public string MyProjektNummer
        {
			get { return _myProjektNummer; }
			set { _myProjektNummer = value; OnPropertyChanged(nameof(MyProjektNummer)); }
		}

		private DateTime _myStand = DateTime.Now;

		public DateTime MyStand
		{
			get { return _myStand; }
			set { _myStand = value; }
		}


		private string _ProjektLeiter;

		public string MyProjektLeiter
        {
			get
			{
				return _ProjektLeiter;
			}
			set
			{
                _ProjektLeiter = value;
				OnPropertyChanged(nameof(MyProjektLeiter));
			}
		}

		private DateTime _Startpunkt = DateTime.Now;

		public DateTime MyStartpunkt
        {
			get
			{
				return _Startpunkt;
			}
			set
			{
                _Startpunkt = value;
				OnPropertyChanged(nameof(MyStartpunkt));
			}
		}

		private DateTime _DeadLine = DateTime.Now;

		public DateTime MyDeadLine
        {
			get
			{
				return _DeadLine;
			}
			set
			{
                _DeadLine = value;
				OnPropertyChanged(nameof(MyDeadLine));
			}
		}





    }
}
