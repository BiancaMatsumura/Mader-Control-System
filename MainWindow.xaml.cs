using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;

namespace Mader_Control_System
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            clientesButton.Checked += ToggleButton_Checked;
            parceirosButton.Checked += ToggleButton_Checked;
            vendedoresButton.Checked += ToggleButton_Checked;

            clientesButton.Unchecked += ToggleButton_Unchecked;
            parceirosButton.Unchecked += ToggleButton_Unchecked;
            vendedoresButton.Unchecked += ToggleButton_Unchecked;
        }

        private void Close_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            this.Close();
        }

        private void Minimized_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void FileAdd_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            ToggleVisibility(NewFileArea);
        }

        private void UserPage_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            ToggleVisibility(UserArea);
        }

        private void HomeButton_Click(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            NewFileArea.Visibility = Visibility.Collapsed;
            UserArea.Visibility = Visibility.Collapsed;
        }

        private void ToggleVisibility(UIElement element)
        {
            element.Visibility = element.Visibility == Visibility.Collapsed ? Visibility.Visible : Visibility.Collapsed;
        }

        private void ToggleButton_Checked(object sender, RoutedEventArgs e)
        {
            if (sender == clientesButton)
            {
                parceirosButton.IsChecked = false;
                vendedoresButton.IsChecked = false;
            }
            else if (sender == parceirosButton)
            {
                clientesButton.IsChecked = false;
                vendedoresButton.IsChecked = false;
            }
            else if (sender == vendedoresButton)
            {
                clientesButton.IsChecked = false;
                parceirosButton.IsChecked = false;
            }
        }

        private void ToggleButton_Unchecked(object sender, RoutedEventArgs e)
        {
            // Comportamento padrão
        }

        private void ChangeButtonColor(Button button, string colorHex, string originalColorHex, double seconds = 0.1)
        {
            button.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString(colorHex));
            DispatcherTimer timer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(seconds) };
            timer.Tick += (s, args) =>
            {
                button.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString(originalColorHex));
                timer.Stop();
            };
            timer.Start();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            ChangeButtonColor(saveButton, "#757A8B", "#8A90A6");
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            ChangeButtonColor(cancelButton, "#D9D9D9", "#F2F2F2");
        }
    }
}
