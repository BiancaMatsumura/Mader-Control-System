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



        private void TextBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {

        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {

        }
    }


}