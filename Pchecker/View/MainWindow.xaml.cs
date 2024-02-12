using System.Windows;
using System.Windows.Input;


namespace ProjektManager.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {

            InitializeComponent();
  

        }

        private void ListBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            //Projekt clickedProjekt = (Projekt)((ListBox)sender).SelectedItem;
            //ProjektDetailsWindow detailWindow = new ProjektDetailsWindow();
            //((ViewModelProjektDetails)detailWindow.DataContext).Projekt = clickedProjekt;
            //detailWindow.Show();
            
        }

        private void PieChart_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void PieChart_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
             
        }
    }
}