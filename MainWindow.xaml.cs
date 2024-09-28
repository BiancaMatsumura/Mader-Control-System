using System.Windows;

namespace Mader_Control_System
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
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
            if (InfoGrid.Visibility == Visibility.Collapsed)
            {
                InfoGrid.Visibility = Visibility.Visible;
            }
            else
            {
                InfoGrid.Visibility = Visibility.Collapsed;
            }
        }
    }


}