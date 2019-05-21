using System;
using System.Collections.ObjectModel;
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
            TestCommand.CanExecuteChanged += delegate (object sender, EventArgs e)
            {
                Trace.WriteLine(string.Format("TestCommand.CanExecute() returns {0}", TestCommand.CanExecute(null)));
            };
            TestType = TestType.One;

            ListItemOneList = new ObservableCollection<ListItemOne>() {
                new ListItemOne() { Name = "John", Age = 20 },
                new ListItemOne() { Name = "Gorge", Age = 21 }
            };

            ListItemTwoList = new ObservableCollection<ListItemTwo>()
            {
                new ListItemTwo() { Name = "Apple" },
                new ListItemTwo() { Name = "Orange" },
                new ListItemTwo() { Name = "Banana" }
            };
        }

        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }
        public static readonly DependencyProperty TextProperty = DependencyProperty.Register("Text", typeof(string), typeof(Context));

        public TestType TestType
        {
            get { return (TestType)GetValue(TestTypeProperty); }
            set { SetValue(TestTypeProperty, value); }
        }
        public static readonly DependencyProperty TestTypeProperty = DependencyProperty.Register("TestType", typeof(TestType), typeof(Context));

        public bool TestCheckBox
        {
            get { return (bool)GetValue(TestCheckBoxProperty); }
            set { SetValue(TestCheckBoxProperty, value); }
        }
        public static readonly DependencyProperty TestCheckBoxProperty = DependencyProperty.Register("TestCheckBox", typeof(bool), typeof(Context),
            new PropertyMetadata(TestCheckBoxChanged));

        protected static void TestCheckBoxChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var _this = d as Context;
            _this?.TestCommand.OnCanExecuteChanged((bool)e.NewValue);
        }

        public TestCommand TestCommand { get; set; }

        public ObservableCollection<ListItemOne> ListItemOneList { get; protected set; }
        public ObservableCollection<ListItemTwo> ListItemTwoList { get; protected set; }

        public ListItemBase SelectedListItem
        {
            get { return (ListItemBase)GetValue(SelectedListItemProperty); }
            set { SetValue(SelectedListItemProperty, value); }
        }
        public static readonly DependencyProperty SelectedListItemProperty = DependencyProperty.Register("SelectedListItem", typeof(ListItemBase), typeof(Context),
            new PropertyMetadata(ListItemChanged));

        protected static void ListItemChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var item = e.NewValue as ListItemBase;
            var msg = string.Format("ListItemChanged(Type={0}, Name={1})", item?.Type, item?.Name);
            Trace.WriteLine(msg);
        }
    }

    public enum TestType
    {
        Zero,
        One,
        Two
    }

    public class ListItemBase
    {
        public virtual TestType Type { get; set; }
        public string Name { get; set; }
    }

    public class ListItemOne : ListItemBase
    {
        public override TestType Type => TestType.One;
        public int Age { get; set; }
    }

    public class ListItemTwo : ListItemBase
    {
        public override TestType Type => TestType.Two;
    }

    public class TestCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;
        public void OnCanExecuteChanged(bool _CanExecute)
        {
            this._CanExecute = _CanExecute;
            CanExecuteChanged?.Invoke(this, new EventArgs());
        }

        public bool CanExecute(object parameter)
        {
            Trace.WriteLine("TestCommand.CanExecute()");
            return _CanExecute;
        }

        protected bool _CanExecute;

        public void Execute(object parameter)
        {
            Trace.WriteLine("TestCommand.Execute()");
        }
    }
}
