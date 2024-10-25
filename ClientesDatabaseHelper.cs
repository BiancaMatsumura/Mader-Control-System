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
                    ObservaçõesGerais TEXT,

                )";
            using (var command = new SQLiteCommand(sql, connection))
            {
                command.ExecuteNonQuery();
            }
        }
    }

    public static SQLiteConnection GetConnection() => new SQLiteConnection(connectionString);
}
