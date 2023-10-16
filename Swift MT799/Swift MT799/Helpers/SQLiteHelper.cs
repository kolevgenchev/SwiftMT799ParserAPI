using Microsoft.Data.Sqlite;
using Swift_MT799.Models;
using System;
using System.Collections.Generic;

namespace Swift_MT799.Helpers
{
    public class SQLiteHelper
    {
        private const string ConnectionString = "Data Source=swiftMessages.db";

        public void InitializeDatabase()
        {
            using var connection = new SqliteConnection(ConnectionString);
            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText =
            @"
            CREATE TABLE IF NOT EXISTS SwiftMessages (
                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                Content TEXT NOT NULL
            );
            ";

            command.ExecuteNonQuery();
        }

        public void InsertMessage(SwiftMessage message)
        {
            using var connection = new SqliteConnection(ConnectionString);
            connection.Open();

            using var transaction = connection.BeginTransaction();
            var command = connection.CreateCommand();

            command.CommandText =
            @"
            INSERT INTO SwiftMessages (Content) VALUES ($content);
            ";
            command.Parameters.AddWithValue("$content", message.Content);

            command.ExecuteNonQuery();
            transaction.Commit();
        }

    }
}

