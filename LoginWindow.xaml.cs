using System;
using System.Data.SQLite;
using System.IO;
using System.Threading.Tasks;
using System.Windows;

namespace Mader_Control_System.Login
{
    public partial class LoginWindow : Window
    {
        private readonly DatabaseHelper dbHelper;

        public LoginWindow()
        {
            InitializeComponent();
            dbHelper = new DatabaseHelper();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            string username = UsernameTextBox.Text.Trim();
            string password = PasswordTextBox.Password.Trim();

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Por favor, insira o nome de usuário e a senha.", "Aviso", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            bool isValid = await dbHelper.IsUserValidAsync(username, password);
            if (isValid)
            {
                new MainWindow().Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Nome de usuário ou senha incorretos!", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public class DatabaseHelper
        {
            private readonly string connectionString = "Data Source=users.db;Version=3;";

            public DatabaseHelper()
            {
                InitializeDatabase();
            }

            private void InitializeDatabase()
            {
                if (!File.Exists("users.db"))
                {
                    SQLiteConnection.CreateFile("users.db");
                    Console.WriteLine("Banco de dados criado com sucesso.");
                }

                using (SQLiteConnection conn = new SQLiteConnection(connectionString))
                {
                    conn.Open();
                    CreateUsersTable(conn);
                    InsertDefaultUser(conn);
                }
            }

            private void CreateUsersTable(SQLiteConnection conn)
            {
                string tableCommand = @"CREATE TABLE IF NOT EXISTS Users (
                                        Id INTEGER PRIMARY KEY AUTOINCREMENT,
                                        Username TEXT NOT NULL UNIQUE,
                                        Password TEXT NOT NULL);";

                using (SQLiteCommand createTable = new SQLiteCommand(tableCommand, conn))
                {
                    createTable.ExecuteNonQuery();
                }
                Console.WriteLine("Tabela 'Users' verificada/criada.");
            }

            private void InsertDefaultUser(SQLiteConnection conn)
            {
                string checkUserQuery = "SELECT COUNT(1) FROM Users WHERE Username = @Username";
                using (SQLiteCommand cmdCheck = new SQLiteCommand(checkUserQuery, conn))
                {
                    cmdCheck.Parameters.AddWithValue("@Username", "admin");
                    int userCount = Convert.ToInt32(cmdCheck.ExecuteScalar());

                    if (userCount == 0)
                    {
                        string insertCommand = "INSERT INTO Users (Username, Password) VALUES (@Username, @Password)";
                        using (SQLiteCommand insertCmd = new SQLiteCommand(insertCommand, conn))
                        {
                            insertCmd.Parameters.AddWithValue("@Username", "admin");
                            insertCmd.Parameters.AddWithValue("@Password", "1234"); // Em produção, use hashing
                            insertCmd.ExecuteNonQuery();
                        }
                        Console.WriteLine("Usuário 'admin' inserido.");
                    }
                    else
                    {
                        Console.WriteLine("Usuário 'admin' já existe.");
                    }
                }
            }

            public async Task<bool> IsUserValidAsync(string username, string password)
            {
                using (SQLiteConnection conn = new SQLiteConnection(connectionString))
                {
                    await conn.OpenAsync();

                    string query = "SELECT COUNT(1) FROM Users WHERE Username=@Username AND Password=@Password";
                    using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Username", username);
                        cmd.Parameters.AddWithValue("@Password", password);
                        int count = Convert.ToInt32(await cmd.ExecuteScalarAsync());
                        return count > 0;
                    }
                }
            }

            // Método para adicionar um usuário
            public async Task<bool> AddUserAsync(string username, string password)
            {
                using (SQLiteConnection conn = new SQLiteConnection(connectionString))
                {
                    await conn.OpenAsync();
                    string query = "INSERT INTO Users (Username, Password) VALUES (@Username, @Password)";
                    using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Username", username);
                        cmd.Parameters.AddWithValue("@Password", password);
                        try
                        {
                            await cmd.ExecuteNonQueryAsync();
                            return true;
                        }
                        catch (SQLiteException ex)
                        {
                            Console.WriteLine("Erro ao adicionar usuário: " + ex.Message);
                            return false;
                        }
                    }
                }
            }

            // Método para deletar um usuário
            public async Task<bool> DeleteUserAsync(string username)
            {
                using (SQLiteConnection conn = new SQLiteConnection(connectionString))
                {
                    await conn.OpenAsync();
                    string query = "DELETE FROM Users WHERE Username=@Username";
                    using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Username", username);
                        try
                        {
                            int rowsAffected = await cmd.ExecuteNonQueryAsync();
                            return rowsAffected > 0;
                        }
                        catch (SQLiteException ex)
                        {
                            Console.WriteLine("Erro ao deletar usuário: " + ex.Message);
                            return false;
                        }
                    }
                }
            }

            // Método para atualizar a senha de um usuário
            public async Task<bool> UpdateUserAsync(string username, string newPassword)
            {
                using (SQLiteConnection conn = new SQLiteConnection(connectionString))
                {
                    await conn.OpenAsync();
                    string query = "UPDATE Users SET Password=@Password WHERE Username=@Username";
                    using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Username", username);
                        cmd.Parameters.AddWithValue("@Password", newPassword);
                        try
                        {
                            int rowsAffected = await cmd.ExecuteNonQueryAsync();
                            return rowsAffected > 0;
                        }
                        catch (SQLiteException ex)
                        {
                            Console.WriteLine("Erro ao atualizar usuário: " + ex.Message);
                            return false;
                        }
                    }
                }
            }


        }
    }
}

