using ProjektManager.DataBaseAPI;
using ProjektManager.Models;
using ProjektManager.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ProjektManager.View
{
    /// <summary>
    /// Interaction logic for NewProblemWindow.xaml
    /// </summary>
    public partial class NewProblemWindow : Window
    {
        public NewProblemWindow(Projekt selectedProjekt)
        {
            InitializeComponent();
            DataContext = new ViewModelNewProblem();
            (DataContext as ViewModelNewProblem).SelectedProjekt = selectedProjekt;
        }

        private void comboboxVerantwortlicher_Selected(object sender, SelectionChangedEventArgs e)
        {
            (sender as ComboBox).SelectedIndex = -1;
        }
    }
}
