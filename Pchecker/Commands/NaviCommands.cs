using ProjektManager.ViewModel;
using ProjektManager.Services;
using ProjektManager.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjektManager.Commands
{
    public class NaviCommands : CommandBase
    {
        private readonly NaviService _naviService;

        public NaviCommands(NaviService naviService)
        {
            _naviService = naviService;
        }

        public override void Execute(object? parameter)
        {
            _naviService.Navigate();
        }
    }
}
