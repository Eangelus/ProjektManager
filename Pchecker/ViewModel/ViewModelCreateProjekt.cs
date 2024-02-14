using ProjektManager.Commands;
using ProjektManager.Services;
using System.Windows.Input;

namespace ProjektManager.ViewModel
{
    public class ViewModelCreateProjekt : ViewModelBase
    {

        public ViewModelCreateProjekt()
        {
            
        }

        public ICommand CreateProjektCommand { get; }
        public ICommand ChancelCommand { get; }

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


		private DateTime _Startpunkt;

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



		private DateTime _DeadLine;

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






		public ViewModelCreateProjekt(NaviService naviService)
        {
            CreateProjektCommand = new CreateProjektCommand(this, naviService);
			ChancelCommand = new NaviCommands(naviService);
        }




    }
}
