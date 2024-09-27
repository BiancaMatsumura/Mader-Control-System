using System.Data.SQLite;
using System.Windows;

namespace Mader_Control_System.Login
{
    public partial class LoginWindow : Window
    {
        private string connectionString = "Data Source=users.db;Version=3;";

        public class DatabaseHelper
        {
            private string connectionString = "Data Source=users.db;Version=3;";

            public DatabaseHelper()
            {
                CreateDatabase();
                CreateUsersTable();
                InsertDefaultUser();
            }

            // Método para criar o banco de dados se não existir
            private void CreateDatabase()
            {
                if (!System.IO.File.Exists("users.db"))
                {
                    SQLiteConnection.CreateFile("users.db");
                    Console.WriteLine("Banco de dados criado com sucesso.");
                }
            }

            // Método para criar a tabela de usuários
            private void CreateUsersTable()
            {
                using (SQLiteConnection conn = new SQLiteConnection(connectionString))
                {
                    conn.Open();
                    string tableCommand = @"CREATE TABLE IF NOT EXISTS Users (
                                        Id INTEGER PRIMARY KEY AUTOINCREMENT,
                                        Username TEXT NOT NULL UNIQUE,
                                        Password TEXT NOT NULL
                                        );";

                    SQLiteCommand createTable = new SQLiteCommand(tableCommand, conn);
                    createTable.ExecuteNonQuery();
                    conn.Close();
                    Console.WriteLine("Tabela 'Users' verificada/criada.");
                }
            }

            // Método para inserir um usuário padrão
            private void InsertDefaultUser()
            {
                using (SQLiteConnection conn = new SQLiteConnection(connectionString))
                {
                    conn.Open();

                    // Verifica se o usuário 'admin' já existe
                    string checkUser = "SELECT COUNT(1) FROM Users WHERE Username = @Username";
                    SQLiteCommand cmdCheck = new SQLiteCommand(checkUser, conn);
                    cmdCheck.Parameters.AddWithValue("@Username", "admin");
                    int userCount = Convert.ToInt32(cmdCheck.ExecuteScalar());

                    if (userCount == 0)
                    {
                        string insertCommand = "INSERT INTO Users (Username, Password) VALUES (@Username, @Password)";
                        SQLiteCommand insertCmd = new SQLiteCommand(insertCommand, conn);
                        insertCmd.Parameters.AddWithValue("@Username", "admin");
                        insertCmd.Parameters.AddWithValue("@Password", "1234"); // Em produção, use hashing
                        insertCmd.ExecuteNonQuery();
                        Console.WriteLine("Usuário 'admin' inserido.");
                    }
                    else
                    {
                        Console.WriteLine("Usuário 'admin' já existe.");
                    }

                    conn.Close();
                }
            }
        }


        public LoginWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // Obter o texto inserido na TextBox e PasswordBox
            string username = UsernameTextBox.Text.Trim();
            string password = PasswordTextBox.Password.Trim();

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Por favor, insira o nome de usuário e a senha.", "Aviso", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (IsUserValid(username, password))
            {
                // Criar uma instância da nova janela
                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();

                // Fechar a janela de login
                this.Close();
            }
            else
            {
                MessageBox.Show("Nome de usuário ou senha incorretos!", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private bool IsUserValid(string username, string password)
        {
            using (SQLiteConnection conn = new SQLiteConnection(connectionString))
            {
                try
                {
                    conn.Open();

                    string query = "SELECT COUNT(1) FROM Users WHERE Username=@Username AND Password=@Password";
                    SQLiteCommand cmd = new SQLiteCommand(query, conn);
                    cmd.Parameters.AddWithValue("@Username", username);
                    cmd.Parameters.AddWithValue("@Password", password);

                    int count = Convert.ToInt32(cmd.ExecuteScalar());

                    return count > 0;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao conectar ao banco de dados: " + ex.Message, "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }
            }
        }

    }
}
