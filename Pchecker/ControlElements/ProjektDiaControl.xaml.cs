using DocumentFormat.OpenXml.Office.PowerPoint.Y2021.M06.Main;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ProjektManager.ControlElements
{
    /// <summary>
    /// Interaction logic for ProjektDiaControl.xaml
    /// </summary>
    public partial class ProjektDiaControl : UserControl
    {
        public ProjektDiaControl()
        {
            InitializeComponent();
            this.DataContext = new ViewModelProjektDia();
        }
    }
}
