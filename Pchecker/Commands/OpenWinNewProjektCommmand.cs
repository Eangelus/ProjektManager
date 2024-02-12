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
    public class OpenWinNewProjektCommmand : CommandBase
    {

        

        public OpenWinNewProjektCommmand()
        {
        }

        public event EventHandler? CanExecuteChanged;



        public bool CanExecute(object? parameter)
        {

            return true;
        }


        public override void Execute(object? parameter)
        {
            if (parameter != null)
            {
                ((ViewModelMainWindow)parameter).IsNewProjektButtonEnabled = false;
            }
            WindowService.OpenWindow(new NewProjektWindow());
        }

        //private ICommand _addProjektCommand;

        //public ICommand addProjektCommand
        //{
        //    get
        //    {
        //        if (_addProjektCommand == null)
        //        {
        //            _addProjektCommand = new RelayCommand(
        //                () => this.ExecuteCommand(),
        //                () => this.CanExecuteCommand()
        //            );
        //        }
        //        return _addProjektCommand;
        //    }
        //}

        //private bool CanExecuteCommand()
        //{
        //    // Logik zur Bestimmung, ob der Befehl ausgeführt werden kann
        //    return true;
        //}

        //private void ExecuteCommand()
        //{

        //}


    }
}
