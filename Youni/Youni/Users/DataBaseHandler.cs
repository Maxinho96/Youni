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
    }
}
