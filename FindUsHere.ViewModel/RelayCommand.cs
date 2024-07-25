using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FindUsHere.ViewModel
{
    internal class RelayCommand : ICommand
    {

        private Action<object> m_execute;
        private Func<object, bool> m_CanExecute;

        public RelayCommand(Action<object> execute, Func<object, bool> canExecute = null)
        {
            m_execute = execute;
            m_CanExecute = canExecute;
        }

        public event EventHandler? CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return m_CanExecute == null || m_CanExecute(parameter);
        }

        public void Execute(object parameter)
        {
            m_execute(parameter);
        }
    }
}
