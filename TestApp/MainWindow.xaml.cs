using System.Diagnostics;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

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

            Loaded += MainWindow_Loaded;
            Closed += MainWindow_Closed;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            log("MainWindow_Loaded({0},{1})", sender, e);
        }

        private void MainWindow_Closed(object sender, System.EventArgs e)
        {
            log("MainWindow_Closed({0},{1})", sender, e);
        }

        protected void log(string format, params object[] args)
        {
            var msg = string.Format(format, args);
            Trace.WriteLine(msg);
        }

        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Context == null) return;

            var tabControl = sender as TabControl;
            Selector sel = null;
            switch(tabControl.SelectedIndex)
            {
            case 0:
                sel = ListItemOneList;
                break;
            case 1:
                sel = ListItemTwoList;
                break;
            }

            var item = sel?.SelectedItem as TestApp.Model.ListItemBase;
            Context.SelectedListItem = item;
            var msg = string.Format("TabControl_SelectionChanged(Type={0}, Name={1})", item?.Type, item?.Name);
            Trace.WriteLine(msg);
        }
    }
}
