using System;
using System.Collections.Generic;
using System.Data;
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
using ProjektManager.ViewModel;

namespace ProjektManager.ControlElements
{
    /// <summary>
    /// Interaction logic for HolyDayController.xaml
    /// </summary>
    public partial class HolyDayController : UserControl
    {
        public HolyDayController()
        {
            InitializeComponent();
            this.DataContext = new ViewModelHolyDays();
            AddCalendarsToGrid();
        }

        private void Calendar_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {
            ViewModelHolyDays vm = (this.DataContext as ViewModelHolyDays);

            vm.SelectedStartHolyDay = HoldayCal.SelectedDates.First();
            vm.SelectedEndHolyDay = HoldayCal.SelectedDates.Last();
            
        }

        private void AddCalendarsToGrid()
        {
            // Erstelle ein Grid mit 4 Zeilen und 3 Spalten für die Monate
            Grid calendarGrid = new Grid();
            for (int i = 0; i < 3; i++)
            {
                RowDefinition rowDef = new RowDefinition();
                
                calendarGrid.RowDefinitions.Add(rowDef);
            }
            for (int i = 0; i < 4; i++)
            {
                ColumnDefinition colDef = new ColumnDefinition();
                
                calendarGrid.ColumnDefinitions.Add(colDef);
            }

            
            // Füge die Kalender für jeden Monat hinzu
            for (int month = 1; month <= 12; month++)
            {
                Calendar calendar = new Calendar
                {
                    DisplayDate = new DateTime(DateTime.Now.Year, month, 1),
                    DisplayMode = CalendarMode.Month,
                    VerticalAlignment = VerticalAlignment.Center,
                    HorizontalAlignment = HorizontalAlignment.Center
                };

                // Berechne die Zeile und Spalte basierend auf dem Monat
                int row = (month - 1) / 4;
                int col = (month - 1) % 4;

                // Füge den Kalender zur entsprechenden Zeile und Spalte im Grid hinzu
                Grid.SetRow(calendar, row);
                Grid.SetColumn(calendar, col);
                calendarGrid.Children.Add(calendar);
            }

            // Füge das Grid zur Hauptansicht hinzu
            MainGrid.Children.Add(calendarGrid);
        }
    }
}
