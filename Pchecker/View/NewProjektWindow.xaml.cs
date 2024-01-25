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
using Pchecker.ViewModel;

namespace Pchecker.View
{
    /// <summary>
    /// Interaktionslogik für NewProjektWindow.xaml
    /// </summary>
    public partial class NewProjektWindow : Window
    {
        public NewProjektWindow()
        {
            this.DataContext = new ViewModelNewProjekt();
            InitializeComponent();
        }
    }
}
