using DocumentFormat.OpenXml.Bibliography;
using ProjektManager.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ProjektManager.Commands
{
    public abstract class AsyncCommandBase : CommandBase
    {
        private bool _isExecuting;

        public bool IsExecuting
        {
            get
            {
                return _isExecuting;
            }
            set
            {
                _isExecuting = value;
                OnCanExecuteChanged();
            }
        }

        private void OnCanExecuteChanged()
        {
            throw new NotImplementedException();
        }

        public override bool CanExecute(object parameter)
        {
            return !IsExecuting && base.CanExecute(parameter);
        }

        public event EventHandler? CanExecuteChanged;

        


        public override async void Execute(object parameter)
        {
            IsExecuting = true;
            try
            {

            }
            finally {


                IsExecuting = false;
            }
            await ExecuteAsync(parameter);

        }

        public abstract Task ExecuteAsync(object parameter);

    }
}
