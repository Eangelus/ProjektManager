using ProjektManager.ViewModel;
using ProjektManager.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjektManager.Services
{
    public class NaviService
    {
        private readonly NaviStore _naviStore;
        private readonly Func<ViewModelBase> _createViewModel;

        public NaviService(NaviStore naviStore, Func<ViewModelBase> createViewModel)
        {
            _naviStore = naviStore;
            _createViewModel = createViewModel;
        }
        public void Navigate()
        {
            _naviStore.CurrentViewModel = _createViewModel();

        }

    }
}
