﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Monefy.Commands
{
    public class ButtonCommand<T> : ICommand
    {
        readonly Action<T> _execute = null;

        readonly Predicate<T> _canExecute = null;

        public ButtonCommand(Action<T> execute)

            : this(execute, null)
        {

        }

        public ButtonCommand(Action<T> execute, Predicate<T> canExecute)
        {
            if (execute == null)

                throw new ArgumentNullException("execute");

            _execute = execute;

            _canExecute
         = canExecute;
        }


        [DebuggerStepThrough]

        public bool CanExecute(object parameter)
        {
            return _canExecute == null ? true : _canExecute((T)parameter);
        }

        public event EventHandler CanExecuteChanged
        {
            add
            { CommandManager.RequerySuggested += value; }

            remove { CommandManager.RequerySuggested -= value; }
        }

        public void Execute(object parameter)
        {
            _execute((T)parameter);
        }
    }
}