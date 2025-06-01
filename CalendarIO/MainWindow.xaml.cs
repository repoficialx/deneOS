using System.Drawing;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace CalendarIO
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Border_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        private void lblNote_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //txtNote.Focus();
        }

        private void lblTime_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //txtTime.Focus();
        }

        private void txtNote_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            /*
            if (!string.IsNullOrEmpty(txtNote.Text) && txtNote.Text.Length > 0)
                lblNote.Visibility = Visibility.Collapsed;
            else
                lblNote.Visibility = Visibility.Visible;
            */
        }

        private void txtTime_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            /*
            if (!string.IsNullOrEmpty(txtTime.Text) && txtTime.Text.Length > 0)
                lblTime.Visibility = Visibility.Collapsed;
            else
                lblTime.Visibility = Visibility.Visible;
            */
        }

        public void UpdateMonth()
        {
            // Assuming 'month' is a UI element like a TextBlock or Label defined in XAML.
            month.Text = getMonth(System.DateTime.Now.Month);
            switch (DateTime.Now.Month)
            {
                default:
                    break;
                case 1:
                    btnEnero.Foreground = new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#C73F69"));
                    btnEnero.FontWeight = FontWeights.SemiBold;
                    break;
                case 2:
                    btnFebrero.Foreground = new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#C73F69"));
                    btnFebrero.FontWeight = FontWeights.SemiBold;
                    break;
                case 3:
                    btnMarzo.Foreground = new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#C73F69"));
                    btnMarzo.FontWeight = FontWeights.SemiBold;
                    break;
                case 4:
                    btnAbril.Foreground = new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#C73F69"));
                    btnAbril.FontWeight = FontWeights.SemiBold;
                    break;
                case 5:
                    btnMayo.Foreground = new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#C73F69"));
                    btnMayo.FontWeight = FontWeights.SemiBold;
                    break;
                case 6:
                    btnJunio.Foreground = new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#C73F69"));
                    btnJunio.FontWeight = FontWeights.SemiBold;
                    break;
                case 7:
                    btnJulio.Foreground = new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#C73F69"));
                    btnJulio.FontWeight = FontWeights.SemiBold;
                    break;
                case 8:
                    btnAgosto.Foreground = new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#C73F69"));
                    btnAgosto.FontWeight = FontWeights.SemiBold;
                    break;
                case 9:
                    btnSeptiembre.Foreground = new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#C73F69"));
                    btnSeptiembre.FontWeight = FontWeights.SemiBold;
                    break;
                case 10:
                    btnOctubre.Foreground = new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#C73F69"));
                    btnOctubre.FontWeight = FontWeights.SemiBold;
                    break;
                case 11:
                    btnNoviembre.Foreground = new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#C73F69"));
                    btnNoviembre.FontWeight = FontWeights.SemiBold;
                    break;
                case 12:
                    btnDiciembre.Foreground = new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#C73F69"));
                    btnDiciembre.FontWeight = FontWeights.SemiBold;
                    break;

            }
        }

        public void UpdateDay()
        {
            // Assuming 'day' is a UI element like a TextBlock or Label defined in XAML.
            day.Text = System.DateTime.Now.Day.ToString();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            UpdateMonth();
            UpdateDay();
            UpdateDayOfWeek();
            uYear();
        }
        void uYear()
        {
            año2atras.Content = (System.DateTime.Now.Year - 2).ToString();
            año1atras.Content = (System.DateTime.Now.Year - 1).ToString();
            año.Content = System.DateTime.Now.Year.ToString();
            año1delante.Content = (System.DateTime.Now.Year + 1).ToString();
            año2delante.Content = (System.DateTime.Now.Year + 2).ToString();
        }
        public void UpdateDayOfWeek()
        {
            // Assuming 'dow' is a UI element like a TextBlock or Label defined in XAML.
            dow.Text = System.DateTime.Now.DayOfWeek.ToString();
        }

        private void MenuButton_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void MenuButton_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            UpdateMonth();
        }

        string getMonth(int monthNumber)
        {
            return System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(monthNumber).Substring(0, 1).ToUpper() + System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(monthNumber).Substring(1).ToLower();
        }

    }
}