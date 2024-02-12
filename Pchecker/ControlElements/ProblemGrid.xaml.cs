using ProjektManager.Models;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace ProjektManager.ControlElements
{
    /// <summary>
    /// Interaktionslogik für ProblemGrid.xaml
    /// </summary>
    public partial class ProblemGrid : UserControl
    {
        public ProblemGrid()
        {
            InitializeComponent();
            DataContext = this;
            //Problems = [new Problem(DateTime.Now, "aksldfj", "klasdjfö", "kalsödfkj")];
        }

        public static readonly DependencyProperty ProblemsProperty = DependencyProperty.Register(nameof(Problems),
                                                                                typeof(ObservableCollection<Problem>),
                                                                                typeof(ProblemGrid), new PropertyMetadata(new ObservableCollection<Problem>(), OnChanged));


        private static void OnChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            if (sender != null)
            {
                (sender as ProblemGrid).OnChanged();
            }
        }

        void OnChanged()
        {
            if(Problems != null)
            {
                Problems.CollectionChanged += new System.Collections.Specialized.NotifyCollectionChangedEventHandler(Problems_CollectionChanged);
            }
        }

        void Problems_CollectionChanged(object? sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs? args)
        {
            Console.WriteLine("Funktioniert?");
        }

        public ObservableCollection<Problem> Problems
        {
            get { return (ObservableCollection<Problem>) GetValue(ProblemsProperty); }
            set
            {
                myDataGrid.ItemsSource = value;
                SetValue(ProblemsProperty, value);
            }
        }
    }
}

