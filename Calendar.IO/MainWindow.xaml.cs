using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Calendar.IO
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainWindow : Window
    {
        private DateTime _currentDate = DateTime.Now;
        public MainWindow()
        {
            InitializeComponent();

            BuildCalendar(_currentDate);

            PrevMonthBtn.Click += (s, e) =>
            {
                _currentDate = _currentDate.AddMonths(-1);
                BuildCalendar(_currentDate);
            };

            NextMonthBtn.Click += (s, e) =>
            {
                _currentDate = _currentDate.AddMonths(1);
                BuildCalendar(_currentDate);
            };

            var hwnd = WinRT.Interop.WindowNative.GetWindowHandle(this);
            var windowId = Microsoft.UI.Win32Interop.GetWindowIdFromWindow(hwnd);
            var appWindow = Microsoft.UI.Windowing.AppWindow.GetFromWindowId(windowId);

            var iconPath = Path.Combine(AppContext.BaseDirectory, "plain.ico");
            appWindow.SetIcon(iconPath);
        }

        private void BuildCalendar(DateTime date)
        {
            CalendarGrid.Children.Clear();
            CalendarGrid.RowDefinitions.Clear();
            CalendarGrid.ColumnDefinitions.Clear();

            // 7 columnas (lunes -> domingo)
            for (int c = 0; c < 7; c++)
                CalendarGrid.ColumnDefinitions.Add(new ColumnDefinition());

            // Etiquetas de días de la semana
            string[] weekNames = { "L", "M", "X", "J", "V", "S", "D" };

            CalendarGrid.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });

            for (int i = 0; i < 7; i++)
            {
                TextBlock dayHeader = new TextBlock
                {
                    Text = weekNames[i],
                    FontSize = 18,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center,
                    Opacity = 0.7
                };
                Grid.SetRow(dayHeader, 0);
                Grid.SetColumn(dayHeader, i);
                CalendarGrid.Children.Add(dayHeader);
            }

            // Días del mes
            int daysInMonth = DateTime.DaysInMonth(date.Year, date.Month);
            DateTime firstDay = new DateTime(date.Year, date.Month, 1);

            int startIndex = ((int)firstDay.DayOfWeek + 6) % 7; // Lunes=0

            int row = 1;
            CalendarGrid.RowDefinitions.Add(new RowDefinition());

            for (int day = 1; day <= daysInMonth; day++)
            {
                int column = (startIndex + day - 1) % 7;

                if (column == 0 && day != 1)
                {
                    row++;
                    CalendarGrid.RowDefinitions.Add(new RowDefinition());
                }

                Border dayCell = new Border
                {
                    Padding = new(10),
                    CornerRadius = new CornerRadius(6),
                    Background = day == DateTime.Now.Day &&
                                 date.Month == DateTime.Now.Month &&
                                 date.Year == DateTime.Now.Year
                                 ? new SolidColorBrush(Microsoft.UI.Colors.CadetBlue)
                                 : new SolidColorBrush(Microsoft.UI.Colors.Transparent)
                };

                TextBlock dayText = new TextBlock
                {
                    Text = day.ToString(),
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center,
                    FontSize = 18,
                    FontWeight = Microsoft.UI.Text.FontWeights.Medium,
                    Foreground = dayCell.Background is SolidColorBrush brush &&
                                 brush.Color != Microsoft.UI.Colors.Transparent
                                 ? new SolidColorBrush(Microsoft.UI.Colors.White)
                                 : new SolidColorBrush(Microsoft.UI.Colors.Black)
                };

                dayCell.Child = dayText;

                Grid.SetRow(dayCell, row);
                Grid.SetColumn(dayCell, column);
                CalendarGrid.Children.Add(dayCell);
            }

            MonthLabel.Text = date.ToString("MMMM yyyy", new CultureInfo("es-ES")).ToUpper();
        }
    }
}
