using System.Windows;

namespace Mader_Control_System.Login
{
    public partial class LoginWindow : Window
    {
       

        public LoginWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
          
            string username = UsernameTextBox.Text;

            
            if (username == "admin")
            {
               
                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Nome de usuário incorreto!", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
