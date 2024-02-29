using CommunityToolkit.Mvvm.Input;
using ProjektManager.Logic;
using ProjektManager.View;
using ProjektManager.ViewModel;
using ProjektManager.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ProjektManager.Commands
{
    /// <summary>
    /// ICommand class for a new window to make a new Projekt
    /// </summary>
    public class OpenNewProjektWindowCommand : CommandBase
    {

        

        public OpenNewProjektWindowCommand()
        {
        }

        public event EventHandler? CanExecuteChanged;



        public bool CanExecute(object? parameter)
        {

            return true;
        }


        public override void Execute(object? parameter)
        {
            var window = new NewProjektWindow();
            window.ShowDialog();
        }

    }
}
