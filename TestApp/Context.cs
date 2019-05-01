using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Input;

namespace TestApp.Model
{
    public class Context : DependencyObject
    {
        public Context()
        {
            TestCommand = new TestCommand();
        }

        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }
        public static readonly DependencyProperty TextProperty = DependencyProperty.Register("Text", typeof(string), typeof(Context));

        public TestCommand TestCommand { get; set; }
    }

    public class TestCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;
        public void OnCanExecuteChanged() { CanExecuteChanged?.Invoke(this, new EventArgs()); }

        public bool CanExecute(object parameter)
        {
            Trace.WriteLine("TestCommand.CanExecute()");
            return true;
        }

        public void Execute(object parameter)
        {
            Trace.WriteLine("TestCommand.Execute()");
        }
    }
}
