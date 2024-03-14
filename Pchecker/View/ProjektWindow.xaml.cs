using ProjektManager.DataBaseAPI;
using ProjektManager.DTOs;
using ProjektManager.Models;
using ProjektManager.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for ProjektWindow.xaml
    /// </summary>
    public partial class ProjektWindow : Window
    {
        public ProjektWindow()
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
            var a = (ViewModelProjektWindow)DataContext;
            a.LoadAllProjekte();
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Projekt selectedProjekt = ((sender as ListBox).SelectedItem as Projekt);
            var context = (ViewModelProjektWindow)DataContext;
            if (selectedProjekt != null)
            {
                context.SelectedProjekt = selectedProjekt;
                context.FilterProblems = new ObservableCollection<Problem>(selectedProjekt.Probleme);
            }
        }

        private void Delete_Projekt_Click(object sender, RoutedEventArgs e)
        {
            Projekt selectedProjekt = ((sender as MenuItem).DataContext as Projekt);
            var context = (ViewModelProjektWindow)DataContext;

            if (context != null && selectedProjekt != null)
            {
                var filtered = new ObservableCollection<Projekt>(context.Projekte.Where(p => p.ProjektNr != selectedProjekt.ProjektNr));
                context.Projekte = filtered;
                var _projektDBContextFactory = new ProjektDBContextFactory(App.CONSTRING);
                using (ProjektDBContext dbContext = _projektDBContextFactory.CreateDbContext())
                {
                    dbContext.DeleteProjekt(ProjektDTO.ToProjektDTO(selectedProjekt));
                }

            }


        }

        private void OnVerantwortlicherChanged(object sender, SelectionChangedEventArgs e)
        {
            Mitarbeiter newVerantwortlicher = (Mitarbeiter)e.AddedItems[0];
            ComboBox comboBox = sender as ComboBox;

            Problem changed = (Problem)comboBox.DataContext;

            if (newVerantwortlicher == null)
            {
                return;
            }

            changed.Verantwortlicher = newVerantwortlicher;

            var _projektDBContextFactory = new ProjektDBContextFactory(App.CONSTRING);
            using (ProjektDBContext dbContext = _projektDBContextFactory.CreateDbContext())
            {
                var result = dbContext.Update(ProblemDTO.ToProblemDTO(changed));
                dbContext.SaveChanges();
            }
            var a = (ViewModelProjektWindow)DataContext;
            a.LoadAllProjekte();
        }
    }
}
