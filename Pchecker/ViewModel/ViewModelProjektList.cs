using ProjektManager.ViewModel;
using ProjektManager.Commands;
using ProjektManager.Services;
using ProjektManager.Stores;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ProjektManager.ViewModel
{
    public class ViewModelProjektList : ViewModelBase
    {

        public ViewModelProjektList()
        {
            
        }

        private readonly ObservableCollection<ViewModelProjekt> _projektList;

        public IEnumerable<ViewModelProjekt> Projekte => _projektList;

        public ICommand CreateProjektCommand { get;}

        public ViewModelProjektList(NaviService WoauchImmerNaviService)
        {
            _projektList = new ObservableCollection<ViewModelProjekt>();

            CreateProjektCommand = new NaviCommands(WoauchImmerNaviService);

        }
    }

}
