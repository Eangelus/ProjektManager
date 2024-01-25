using Pchecker.Models;
using Pchecker.View;
using Pchecker.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Pchecker.Commands
{
    public class CreateProjektCommand : ICommand
    {
        public event EventHandler? CanExecuteChanged;

        public bool CanExecute(object? parameter)
        {

            return true;
            //throw new NotImplementedException();
        }

        public void Execute(object? parameter)
        {
            var windows = Application.Current.Windows;

            Window? mainWindow = null;
            Window? projektWindow = null; 
            foreach (var window in windows)
            {
                if (window.GetType() == typeof(MainWindow) )
                {
                    mainWindow = (MainWindow)window;
                }
                if (window.GetType() == typeof(NewProjektWindow))
                {
                    projektWindow = (NewProjektWindow)window;
                }

            }
            var dataContext = (ViewModelMainWindow) mainWindow.DataContext;
            var newProjectDataContext = (ViewModelNewProjekt)projektWindow.DataContext;
            dataContext.addProjekt(newProjectDataContext.Projekt);
            if(projektWindow is null)
            {
                return;
            }
            projektWindow.Close();
        }
    }
}
