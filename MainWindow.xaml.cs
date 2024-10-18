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
            if (NewFileArea.Visibility == Visibility.Collapsed)
            {
                NewFileArea.Visibility = Visibility.Visible;
            }

        }
        private void UserPage_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (UserArea.Visibility == Visibility.Collapsed)
            {
                UserArea.Visibility = Visibility.Visible;
            }

        }

        private void HomeButton_Click(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            
            if (NewFileArea.Visibility == Visibility.Visible)
            {
                NewFileArea.Visibility = Visibility.Collapsed;
            }

            
            if (UserArea.Visibility == Visibility.Visible)
            {
                UserArea.Visibility = Visibility.Collapsed;
            }
          
        }

        private void ToggleButton_Checked(object sender, RoutedEventArgs e)
        {
            // Verifica qual botão foi marcado e desmarca os outros
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
            // O comportamento padrão quando desmarcado
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            // Mudar a cor de fundo do botão ao ser clicado
            saveButton.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#757A8B")); // Altere para a cor desejada

            // Criar um DispatcherTimer para retornar à cor original
            DispatcherTimer timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(0.1) // Tempo de espera antes de voltar
            };
            timer.Tick += (s, args) =>
            {
                saveButton.Background = new SolidColorBrush(Color.FromRgb(138, 144, 166)); // Cor original
                timer.Stop(); // Parar o timer
            };
            timer.Start(); // Iniciar o timer
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            // Mudar a cor de fundo do botão ao ser clicado
            cancelButton.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#D9D9D9")); // Altere para a cor desejada

            // Criar um DispatcherTimer para retornar à cor original
            DispatcherTimer timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(0.1) // Tempo de espera antes de voltar
            };
            timer.Tick += (s, args) =>
            {
                cancelButton.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#F2F2F2")); // Cor original
                timer.Stop(); // Parar o timer
            };
            timer.Start(); // Iniciar o timer
        }



    }


}