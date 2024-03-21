using DocumentFormat.OpenXml.Office2019.Excel.RichData2;
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
    /// Interaction logic for MitarbeiterWindow.xaml
    /// </summary>
    public partial class MitarbeiterWindow : Window
    {
        public MitarbeiterWindow()
        {
            InitializeComponent();
            DataContext = new ViewModelMitarbeiterListe();
            speicher_btn.Click += this.Button_Click;
            
        }

        private void DataGrid_CellEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {

            var updatedMitarbeiter = (((DataGrid)sender).CurrentItem as Mitarbeiter);
            if (updatedMitarbeiter == null)
            {
                return;
            }
            var _projektDBContextFactory = new ProjektDBContextFactory(App.CONSTRING);
            using (ProjektDBContext dbContext = _projektDBContextFactory.CreateDbContext())
            {
                
                var result = dbContext.Update(MitarbeiterDTO.ToMitarbeiterDTO(updatedMitarbeiter));
                dbContext.SaveChanges();
            }
        }
        
        

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            foreach(Mitarbeiter updatedMitarbeiter in GridMitarbeiter.Items)
            {

                var _projektDBContextFactory = new ProjektDBContextFactory(App.CONSTRING);
                using (ProjektDBContext dbContext = _projektDBContextFactory.CreateDbContext())
                {

                    var result = dbContext.Update(MitarbeiterDTO.ToMitarbeiterDTO(updatedMitarbeiter));
                    dbContext.SaveChanges();
                }


            }

            this.Close();
        }
    }
}
