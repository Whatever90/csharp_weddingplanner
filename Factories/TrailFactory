using System.Collections.Generic;
using System.Linq;
using Dapper;
using System.Data;
using MySql.Data.MySqlClient;
using connectingToDBTESTING.Models;
 
namespace connectingToDBTESTING.Factory
{
    public class TrailFactory : IFactory<User>
    {
        private string connectionString;
        public TrailFactory()
        {
            connectionString = "server=localhost;userid=root;password=root;port=3305;database=c#_users;SslMode=None";
        }
        internal IDbConnection Connection
        {
            get {
                return new MySqlConnection(connectionString);
            }
        }
        public void Add(Trail item)
        {
            using (IDbConnection dbConnection = Connection) {
                string query =  $"INSERT INTO trails (name, length, elevation, latitude, longitude, description) VALUES (@name, @length, @elevation, @latitude, @longitude, @description)";
                dbConnection.Open();
                dbConnection.Execute(query, item);
            }
        }
        public IEnumerable<Trail> FindAll()
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                return dbConnection.Query<Trail>("SELECT * FROM trails");
            }
        }
        public Trail FindByID(int id)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                return dbConnection.Query<Trail>("SELECT * FROM trails WHERE id = @Id", new { Id = id }).FirstOrDefault();
            }
        }
    }
}