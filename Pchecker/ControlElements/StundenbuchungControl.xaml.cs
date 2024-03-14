using ProjektManager.View;
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
    /// Interaction logic for StundenbuchungControl.xaml
    /// </summary>
    public partial class StundenbuchungControl : UserControl
    {
        public StundenbuchungControl()
        {
            InitializeComponent();
            this.DataContext = new ViewModelStundenbuchung();
            
        }

        private void ComboBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            var cb = sender as ComboBox;
            cb.IsDropDownOpen = true;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var newStundenW = new NewStundenBuchungWindow();
            newStundenW.Show();
        }


    }
}
