using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Pchecker.Logic;
using Pchecker.Models;
using Pchecker.ViewModel;
using ProjektManager.DataBaseAPI;
using Microsoft.EntityFrameworkCore;


namespace Pchecker.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {

            InitializeComponent();
            DataContext = new ViewModelMainWindow();
            var MyContext = new DBContext();


        }

        private void ListBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Projekt clickedProjekt = (Projekt)((ListBox)sender).SelectedItem;
            ProjektDetailsWindow detailWindow = new ProjektDetailsWindow();
            ((ViewModelProjektDetails)detailWindow.DataContext).Projekt = clickedProjekt;
            detailWindow.Show();
            
        }

        private void PieChart_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void PieChart_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
             
        }
    }
}