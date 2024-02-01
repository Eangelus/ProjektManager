using Pomelo.EntityFrameworkCore.MySql.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace ProjektManager.Commands
{
    internal class PartSaveCommand : ICommand
    {
        private Action<DataGridRowEditEndingEventArgs> execute;

        public PartSaveCommand(Action<DataGridRowEditEndingEventArgs> execute)
        {
            this.execute = execute;
        }


        public event EventHandler? CanExecuteChanged;

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            execute((DataGridRowEditEndingEventArgs)parameter);
        }
    }
}
