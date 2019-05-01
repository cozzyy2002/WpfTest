using System.Windows;

namespace TestApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public Model.Context Context { get; set; }

        public MainWindow()
        {
            Context = new Model.Context();

            InitializeComponent();
        }
    }
}
