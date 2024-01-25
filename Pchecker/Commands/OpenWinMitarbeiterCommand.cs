using Pchecker.Logic;
using Pchecker.View;
using Pchecker.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Pchecker.Commands
{
    public class OpenWinMitarbeiterCommand : ICommand
    {
        

        public OpenWinMitarbeiterCommand()
        {
        }

        public event EventHandler? CanExecuteChanged;



        public bool CanExecute(object? parameter)
        {

            return true;
        }


        public void Execute(object? parameter)
        {
            if (parameter != null)
            {
                ((ViewModelMainWindow)parameter).IsNewProjektButtonEnabled = false;
            }
            WindowService.OpenWindow(new NewProjektWindow());
        }
    }
}
