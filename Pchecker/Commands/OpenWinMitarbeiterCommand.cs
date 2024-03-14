using ProjektManager.Logic;
using ProjektManager.View;
using ProjektManager.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ProjektManager.Commands
{
    /// <summary>
    /// ICommand Class to Open new Windows for Works
    /// </summary>
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
            var window = new MitarbeiterWindow();
            window.ShowDialog();

        }
    }
}
