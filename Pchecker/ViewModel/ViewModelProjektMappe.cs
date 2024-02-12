using LiveChartsCore.SkiaSharpView;
using ProjektManager.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjektManager.ViewModel
{
    public class ViewModelProjektMappe: ViewModelBase
    {

        private readonly IEnumerable<ViewModelProjekt> _projekte;
        public IEnumerable<ViewModelProjekt> Projekte => _projekte;

        public ViewModelProjektMappe()
        {
            _projekte = new ObservableCollection<ViewModelProjekt>();




            //projekte = new ObservableCollection<Projekt>(ExcelConnection.ReadAllExcelFiles());


            //var projekte = ;
            //IEnumerable<Projekt> projekte = projekte;


            //projekt = new ObservableCollection<Projekt>((IEnumerable<Projekt>)GetAllProjekts());

        }










    }
}
