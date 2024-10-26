using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;

public static class ClientesDatabaseHelper
{
    private static readonly string dbFilePath = "clientes.db";
    private static readonly string connectionString = $"Data Source={dbFilePath};Version=3;";

    static ClientesDatabaseHelper()
    {
        if (!File.Exists(dbFilePath))
        {
            SQLiteConnection.CreateFile(dbFilePath);
            CriarTabelaClientes();
        }
    }

    private static void CriarTabelaClientes()
    {
        using (var connection = new SQLiteConnection(connectionString))
        {
            connection.Open();
            string sql = @"
            CREATE TABLE IF NOT EXISTS Clientes (
                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                DataCadastro TEXT,
                Nome TEXT NOT NULL,  
                NomeSocial TEXT,
                DataNasc TEXT,
                CPF TEXT,
                RG TEXT,
                CNPJ TEXT,
                IE TEXT,
                Celular01 TEXT,
                Celular02 TEXT,
                Email01 TEXT,
                Email02 TEXT,
                Endereco TEXT,
                Bairro TEXT,
                Cidade TEXT,
                Estado TEXT,
                CEP TEXT,
                Complemento TEXT, 
                ReferenciasParaEntrega TEXT,
                ObservacoesGerais TEXT
            )";
            using (var command = new SQLiteCommand(sql, connection))
            {
                command.ExecuteNonQuery();
            }
        }
    }

    public static SQLiteConnection GetConnection() => new SQLiteConnection(connectionString);

    public static List<Cliente> ObterClientes()
    {
        var clientes = new List<Cliente>();
        using (var connection = GetConnection())
        {
            connection.Open();
            string sql = "SELECT * FROM Clientes";
            using (var command = new SQLiteCommand(sql, connection))
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    clientes.Add(new Cliente
                    {
                        Id = reader.GetInt32(0),
                        DataCadastro = reader.IsDBNull(1) ? (DateTime?)null : DateTime.Parse(reader.GetString(1)),
                        Nome = reader.GetString(2),
                        NomeSocial = reader.IsDBNull(3) ? null : reader.GetString(3),
                        DataNasc = reader.IsDBNull(4) ? (DateTime?)null : DateTime.Parse(reader.GetString(4)),
                        CPF = reader.IsDBNull(5) ? null : reader.GetString(5),
                        RG = reader.IsDBNull(6) ? null : reader.GetString(6),
                        CNPJ = reader.IsDBNull(7) ? null : reader.GetString(7),
                        IE = reader.IsDBNull(8) ? null : reader.GetString(8),
                        Celular01 = reader.IsDBNull(9) ? null : reader.GetString(9),
                        Celular02 = reader.IsDBNull(10) ? null : reader.GetString(10),
                        Email01 = reader.IsDBNull(11) ? null : reader.GetString(11),
                        Email02 = reader.IsDBNull(12) ? null : reader.GetString(12),
                        Endereco = reader.IsDBNull(13) ? null : reader.GetString(13),
                        Bairro = reader.IsDBNull(14) ? null : reader.GetString(14),
                        Cidade = reader.IsDBNull(15) ? null : reader.GetString(15),
                        Estado = reader.IsDBNull(16) ? null : reader.GetString(16),
                        CEP = reader.IsDBNull(17) ? null : reader.GetString(17),
                        Complemento = reader.IsDBNull(18) ? null : reader.GetString(18),
                        ReferenciasParaEntrega = reader.IsDBNull(19) ? null : reader.GetString(19),
                        ObservacoesGerais = reader.IsDBNull(20) ? null : reader.GetString(20)
                    });
                }
            }
        }
        return clientes;
    }

    public static void AdicionarCliente(Cliente cliente)
    {
        using (var connection = GetConnection())
        {
            connection.Open();
            string sql = @"
            INSERT INTO Clientes 
            (Nome, DataCadastro, NomeSocial, DataNasc, CPF, RG, CNPJ, IE, Celular01, Celular02, 
            Email01, Email02, Endereco, Bairro, Cidade, Estado, CEP, Complemento, 
            ReferenciasParaEntrega, ObservacoesGerais) 
            VALUES 
            (@Nome, @DataCadastro, @NomeSocial, @DataNasc, @CPF, @RG, @CNPJ, @IE, @Celular01, @Celular02, 
            @Email01, @Email02, @Endereco, @Bairro, @Cidade, @Estado, @CEP, @Complemento, 
            @ReferenciasParaEntrega, @ObservacoesGerais)";

            using (var command = new SQLiteCommand(sql, connection))
            {
                command.Parameters.AddWithValue("@Nome", cliente.Nome);
                command.Parameters.AddWithValue("@DataCadastro", cliente.DataCadastro?.ToString("yyyy-MM-dd"));
                command.Parameters.AddWithValue("@NomeSocial", cliente.NomeSocial ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@DataNasc", cliente.DataNasc?.ToString("yyyy-MM-dd") ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@CPF", cliente.CPF ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@RG", cliente.RG ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@CNPJ", cliente.CNPJ ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@IE", cliente.IE ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@Celular01", cliente.Celular01 ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@Celular02", cliente.Celular02 ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@Email01", cliente.Email01 ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@Email02", cliente.Email02 ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@Endereco", cliente.Endereco ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@Bairro", cliente.Bairro ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@Cidade", cliente.Cidade ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@Estado", cliente.Estado ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@CEP", cliente.CEP ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@Complemento", cliente.Complemento ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@ReferenciasParaEntrega", cliente.ReferenciasParaEntrega ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@ObservacoesGerais", cliente.ObservacoesGerais ?? (object)DBNull.Value);

                command.ExecuteNonQuery();
            }
        }
    }

    public static void AtualizarCliente(Cliente cliente)
    {
        using (var connection = GetConnection())
        {
            connection.Open();
            string sql = @"
            UPDATE Clientes SET 
                Nome = @Nome,
                DataCadastro = @DataCadastro,
                NomeSocial = @NomeSocial,
                DataNasc = @DataNasc,
                CPF = @CPF,
                RG = @RG,
                CNPJ = @CNPJ,
                IE = @IE,
                Celular01 = @Celular01,
                Celular02 = @Celular02,
                Email01 = @Email01,
                Email02 = @Email02,
                Endereco = @Endereco,
                Bairro = @Bairro,
                Cidade = @Cidade,
                Estado = @Estado,
                CEP = @CEP,
                Complemento = @Complemento,
                ReferenciasParaEntrega = @ReferenciasParaEntrega,
                ObservacoesGerais = @ObservacoesGerais
            WHERE Id = @Id";

            using (var command = new SQLiteCommand(sql, connection))
            {
                command.Parameters.AddWithValue("@Nome", cliente.Nome);
                command.Parameters.AddWithValue("@DataCadastro", cliente.DataCadastro?.ToString("yyyy-MM-dd"));
                command.Parameters.AddWithValue("@NomeSocial", cliente.NomeSocial ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@DataNasc", cliente.DataNasc?.ToString("yyyy-MM-dd") ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@CPF", cliente.CPF ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@RG", cliente.RG ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@CNPJ", cliente.CNPJ ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@IE", cliente.IE ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@Celular01", cliente.Celular01 ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@Celular02", cliente.Celular02 ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@Email01", cliente.Email01 ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@Email02", cliente.Email02 ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@Endereco", cliente.Endereco ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@Bairro", cliente.Bairro ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@Cidade", cliente.Cidade ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@Estado", cliente.Estado ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@CEP", cliente.CEP ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@Complemento", cliente.Complemento ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@ReferenciasParaEntrega", cliente.ReferenciasParaEntrega ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@ObservacoesGerais", cliente.ObservacoesGerais ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@Id", cliente.Id);

                command.ExecuteNonQuery();
            }
        }
    }

    public static void DeletarCliente(int id)
    {
        using (var connection = GetConnection())
        {
            connection.Open();
            string sql = "DELETE FROM Clientes WHERE Id = @Id";
            using (var command = new SQLiteCommand(sql, connection))
            {
                command.Parameters.AddWithValue("@Id", id);
                command.ExecuteNonQuery();
            }
        }
    }


}
