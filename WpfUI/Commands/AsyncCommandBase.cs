using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfUI.Commands
{
    public abstract class AsyncCommandBase:CommandBase
    {
        private bool _isExecuting;

        public bool  IsExecuting
        {
            get { return _isExecuting; }
            set 
            { 
                _isExecuting = value;
            
                OnCanExecutedChanged();
            }
        }

        public override bool CanExecute(object? parameter)
        {
            return !IsExecuting &&
                base.CanExecute(parameter);
        }

        public override async void Execute(object? parameter)
        {
            _isExecuting = true;

            try
            {
                await ExecuteAsync(parameter);
            }
            finally
            {
                _isExecuting = false;
            }
        }

        public abstract Task ExecuteAsync(object? parameter);
    }
}
