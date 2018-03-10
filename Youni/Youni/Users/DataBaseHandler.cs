using System;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

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

        /// <summary>Checks if credentials are correct</summary>
        /// <param name="email">The user email to check</param>
        /// <param name="password">The user password to check</param>
        /// <returns>true if the credentials are correct, false otherwise</returns>
        /// <exception cref="Npgsql.NpgsqlException">Thrown if unable to connect to database</exception>
        /// <exception cref="System.Net.Sockets.SocketException">Thrown if unable to connect to database</exception>
        public async Task<bool> CheckCredentialsAsync(string email, string password)
        {
            string query = "SELECT password FROM utenti WHERE email=@email";
            using (var conn = new NpgsqlConnection(this.ConnString))
            {
                await conn.OpenAsync();
                using (var cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@email", email);
                    string hashedPassword = (string) await cmd.ExecuteScalarAsync();
                    return (hashedPassword != null) && (BCrypt.Net.BCrypt.Verify(password, hashedPassword));
                }
            }
        }

        /// <summary>Checks if a user is registered</summary>
        /// <param name="email">The user email to check</param>
        /// <returns>true if the user is registered, false otherwise</returns>
        /// <exception cref="Npgsql.NpgsqlException">Thrown if unable to connect to database</exception>
        /// <exception cref="System.Net.Sockets.SocketException">Thrown if unable to connect to database</exception>
        public async Task<bool> IsRegisteredAsync(string email)
        {
            string query = "SELECT * FROM utenti WHERE email=@email";
            using (var conn = new NpgsqlConnection(this.ConnString))
            {
                await conn.OpenAsync();
                using (var cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@email", email);
                    return (await cmd.ExecuteScalarAsync()) != null;
                }
            }
        }

        /// <summary>Inserts a new user (it doesn't have to exist already)</summary>
        /// <param name="email">The user email to insert</param>
        /// <param name="password">The user password to insert</param>
        /// <param name="name">The user name to insert</param>
        /// <param name="surname">The user surname to insert</param>
        /// <returns>true if the user is inserted, false otherwise</returns>
        /// <exception cref="Npgsql.PostgresException">Thrown if the user already exists</exception>
        /// <exception cref="Npgsql.NpgsqlException">Thrown if unable to connect to database</exception>
        /// <exception cref="System.Net.Sockets.SocketException">Thrown if unable to connect to database</exception>
        public async Task<bool> InsertUserAsync(string email, string password, string name, string surname)
        {
            string commandText = "INSERT INTO utenti (email, password, nome, cognome) VALUES (@email, @password, @name, @surname)";
            using (var conn = new NpgsqlConnection(this.ConnString))
            {
                await conn.OpenAsync();
                using (var cmd = new NpgsqlCommand(commandText, conn))
                {
                    cmd.Parameters.AddWithValue("@email", email);
                    cmd.Parameters.AddWithValue("@password", BCrypt.Net.BCrypt.HashPassword(password));
                    cmd.Parameters.AddWithValue("@name", name);
                    cmd.Parameters.AddWithValue("@surname", surname);
                    return (await cmd.ExecuteNonQueryAsync()) > 0;
                }
            }
        }

        /// <summary>Used to get all the faculties from the DataBase</summary>
        /// <returns>An ObservableCollection of Faculties, taken from the DataBase</returns>
        /// <exception cref="Npgsql.NpgsqlException">Thrown if unable to connect to database</exception>
        /// <exception cref="System.Net.Sockets.SocketException">Thrown if unable to connect to database</exception>
        public async Task<ObservableCollection<Faculty>> GetFacultiesAsync()
        {
            string query = "SELECT nome, percorso_immagine FROM facolta";
            using (var conn = new NpgsqlConnection(this.ConnString))
            {
                await conn.OpenAsync();
                using (var cmd = new NpgsqlCommand(query, conn))
                {
                    using(var reader = await cmd.ExecuteReaderAsync())
                    {
                        ObservableCollection<Faculty> faculties = new ObservableCollection<Faculty>();
                        while(await reader.ReadAsync())
                        {
                            faculties.Add(new Faculty(reader.GetString(0), reader.GetString(1)));
                        }
                        return faculties;
                    }
                }
            }
        }

        /// <summary>Used to get all the classes of a given faculty in a given year</summary>
        /// <returns>An ObservableCollection of Classes, taken from the DataBase</returns>
        /// <exception cref="Npgsql.NpgsqlException">Thrown if unable to connect to database</exception>
        /// <exception cref="System.Net.Sockets.SocketException">Thrown if unable to connect to database</exception>
        public async Task<ObservableCollection<Class>> GetClassesAsync(Faculty faculty, Year year)
        {
            string query = "SELECT nome, nome_corto FROM esami WHERE facolta=@faculty AND anno=@year";
            using (var conn = new NpgsqlConnection(this.ConnString))
            {
                await conn.OpenAsync();
                using (var cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@faculty", faculty.Name);
                    cmd.Parameters.AddWithValue("@year", year.Code);
                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        ObservableCollection<Class> classes = new ObservableCollection<Class>();
                        while (await reader.ReadAsync())
                        {
                            classes.Add(new Class(reader.GetString(0), reader.GetString(1)));
                        }
                        return classes;
                    }
                }
            }
        }

        /// <summary>Used to get all the years of a given faculty</summary>
        /// <returns>An ObservableCollection of years, taken from the DataBase</returns>
        /// <exception cref="Npgsql.NpgsqlException">Thrown if unable to connect to database</exception>
        /// <exception cref="System.Net.Sockets.SocketException">Thrown if unable to connect to database</exception>
        public async Task<ObservableCollection<Year>> GetYearsAsync(Faculty faculty)
        {
            string query = "SELECT DISTINCT codice, descrizione FROM facolta, esami, anni WHERE facolta.nome=esami.facolta AND esami.anno=anni.codice AND facolta=@faculty ORDER BY codice";
            using (var conn = new NpgsqlConnection(this.ConnString))
            {
                await conn.OpenAsync();
                using (var cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@faculty", faculty.Name);
                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        ObservableCollection<Year> years = new ObservableCollection<Year>();
                        while (await reader.ReadAsync())
                        {
                            years.Add(new Year(reader.GetInt16(0), reader.GetString(1)));
                        }
                        return years;
                    }
                }
            }
        }
    }
}
