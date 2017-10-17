using System.Collections.Generic;
using System.Linq;
using Dapper;
using System.Data;
using MySql.Data.MySqlClient;
using connectingToDBTESTING.Models;
 
namespace connectingToDBTESTING.Factory
{
    public class UserFactory : IFactory<User>
    {
        private string connectionString;
        public UserFactory()
        {
            connectionString = "server=localhost;userid=root;password=root;port=3305;database=c#_users;SslMode=None";
        }
        internal IDbConnection Connection
        {
            get {
                return new MySqlConnection(connectionString);
            }
        }
        public void Add(User item)
        {
            using (IDbConnection dbConnection = Connection) {
                string query =  $"INSERT INTO users (first_name, last_name, email, password, age) VALUES (@model.FirstName, @model.LastName, @model.Email, @model.Password, 0)";
                dbConnection.Open();
                dbConnection.Execute(query, item);
            }
        }
        public IEnumerable<User> FindAll()
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                return dbConnection.Query<User>("SELECT * FROM users");
            }
        }
        public User FindByID(int id)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                return dbConnection.Query<User>("SELECT * FROM users WHERE id = @Id", new { Id = id }).FirstOrDefault();
            }
        }
    }
}