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
using ProjektManager.ViewModel;
namespace ProjektManager.View
{
    /// <summary>
    /// Interaction logic for AuftragsDateien.xaml
    /// </summary>
    public partial class AuftragsDateien : Window
    {
        public AuftragsDateien(ViewModelAuftragsdateien vm)
        {
            InitializeComponent();
            this.DataContext = vm;
        }
    }
}
