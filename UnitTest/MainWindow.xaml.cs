using System;
using System.Windows;
using System.Windows.Input;

namespace UnitTest
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            GoCommand = new GoCommand();

            InitializeComponent();
        }

        public string TargetFileName
        {
            get { return (string)GetValue(TargetFileNameProperty); }
            set { SetValue(TargetFileNameProperty, value); }
        }
        public static readonly DependencyProperty TargetFileNameProperty = DependencyProperty.Register("TargetFileName", typeof(string), typeof(MainWindow));

        public GoCommand GoCommand { get; protected set; }
    }

    public class GoCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            throw new NotImplementedException();
        }
    }
}
