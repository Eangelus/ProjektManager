using ProjektManager.DataBaseAPI;
using ProjektManager.DTOs;
using ProjektManager.Models;
using ProjektManager.ViewModel;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
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

        private void GridProblemeAktivProjekt_RowEditEnding(object sender, System.Windows.Controls.DataGridRowEditEndingEventArgs e)
        {
            var updated = (sender as DataGrid).CurrentItem as Problem;
            if (updated == null)
            {
                return;
            }
            var _projektDBContextFactory = new ProjektDBContextFactory(App.CONSTRING);
            using (ProjektDBContext dbContext = _projektDBContextFactory.CreateDbContext())
            {
                var result = dbContext.UpdateProblem(ProblemDTO.ToProblemDTO(updated));
            }
        }

        private void ComboBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            Abteilung? newAbteilung = (Abteilung?)e.AddedItems[0];
            ComboBox comboBox = sender as ComboBox;

            Problem changed = (Problem)comboBox.DataContext;

            changed.Abteilung = newAbteilung;

            var _projektDBContextFactory = new ProjektDBContextFactory(App.CONSTRING);
            using (ProjektDBContext dbContext = _projektDBContextFactory.CreateDbContext())
            {
                var result = dbContext.UpdateProblem(ProblemDTO.ToProblemDTO(changed));
            }
            var a = (ViewModelMainWindow)DataContext;
            a.LoadAllProjekte();
        }

        private void ComboBox_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            string? newStatus = (string?)e.AddedItems[0];
            ComboBox comboBox = sender as ComboBox;

            Problem changed = (Problem)comboBox.DataContext;

            changed.ProzessStatus = newStatus ?? String.Empty;

            var _projektDBContextFactory = new ProjektDBContextFactory(App.CONSTRING);
            using (ProjektDBContext dbContext = _projektDBContextFactory.CreateDbContext())
            {
                var result = dbContext.UpdateProblem(ProblemDTO.ToProblemDTO(changed));
            }
            var a = (ViewModelMainWindow)DataContext;
            a.LoadAllProjekte();
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Projekt selectedProjekt = ((sender as ListBox).SelectedItem as Projekt);
            var context = (ViewModelMainWindow)DataContext;
            if (selectedProjekt != null)
            {
                context.SelectedProjekt = selectedProjekt;
                context.FilterProblems = new ObservableCollection<Problem>(selectedProjekt.Probleme);
            }
        }
    }
}