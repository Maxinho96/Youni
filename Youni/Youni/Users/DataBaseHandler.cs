using System;
using System.Threading.Tasks;

using Npgsql;

namespace Youni
{
    public class DataBaseHandler
    {
        private string ConnString;
        
        public DataBaseHandler()
        {
            this.ConnString = "Host=younidb.cw9vlhucwihr.eu-central-1.rds.amazonaws.com;Port=5432;Username=younidbmaster;Password=Y982fZhd9B8r;Database=younidb";
        }

        /// <summary>Tells if a query returns an empty result</summary>
        /// <param name="query">The query to be tested</param>
        /// <returns>true if the query result is empty, false otherwise</returns>
        /// <exception cref="Npgsql.PostgresException">Thrown when the query can't be executed (e.g. the table doesn't exist)</exception>
        /// <exception cref="System.Net.Sockets.SocketException">Thrown if unable to connect to database</exception>
        public async Task<bool> IsQueryEmptyAsync(string query)
        {
            using (var conn = new NpgsqlConnection(ConnString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand(query, conn))
                {
                    return (await cmd.ExecuteScalarAsync() == null);
                }
            }
        }

        /// <summary>Checks if credentials are correct</summary>
        /// <param name="email">The user email to check</param>
        /// <param name="password">The user password to check</param>
        /// <returns>true if the credentials are correct, false otherwise</returns>
        public async Task<bool> CheckCredentialsAsync(string email, string password)
        {
            string query = String.Format("SELECT * FROM utenti WHERE email='{0}' AND password='{1}'", email, password);
            return !(await this.IsQueryEmptyAsync(query));
        }

        /// <summary>Executes a non-query command (e.g. an INSERT)</summary>
        /// <param name="commandText">The command to executec</param>
        /// <returns>true if the command did some change (e.g. some rows were added), false otherwise<returns>
        /// <exception cref="System.Net.Sockets.SocketException">Thrown if unable to connect to database</exception>
        public async Task<bool> ExecuteCommandAsync(string commandText)
        {
            using (var conn = new NpgsqlConnection(ConnString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand(commandText, conn))
                {
                    return (await cmd.ExecuteNonQueryAsync()) > 0;
                }
            }
        }

        /// <summary>Checks if a user is registered</summary>
        /// <param name="email">The user email to check</param>
        /// <returns>true if the user is registered, false otherwise</returns>
        public async Task<bool> IsRegisteredAsync(string email)
        {
            string query = String.Format("SELECT * FROM utenti WHERE email='{0}'", email);
            return !(await this.IsQueryEmptyAsync(query));
        }

        /// <summary>Inserts a new user (it doesn't have to exist already)</summary>
        /// <param name="email">The user email to insert</param>
        /// <param name="password">The user password to insert</param>
        /// <param name="name">The user name to insert</param>
        /// <param name="surname">The user surname to insert</param>
        /// <returns>true if the user is inserted, false otherwise</returns>
        /// <exception cref="Npgsql.PostgresException">Thrown if the user already exists</exception>
        public async Task<bool> InsertUserAsync(string email, string password, string name, string surname)
        {
            string commandText = String.Format("INSERT INTO utenti (email, password, name, surname) VALUES ('{0}', '{1}', '{2}', '{3}')", email, password, name, surname);
            return (await this.ExecuteCommandAsync(commandText));
        }
    }
}
