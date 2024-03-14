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
    /// Interaction logic for NewStundenBuchungWindow.xaml
    /// </summary>
    public partial class NewStundenBuchungWindow : Window
    {
        public NewStundenBuchungWindow()
        {
            InitializeComponent();
            this.DataContext = new ViewModelStundenbuchung();
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {

        }
    }
}
