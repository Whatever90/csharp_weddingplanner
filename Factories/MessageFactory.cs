using System.Collections.Generic;
using System.Linq;
using Dapper;
using System.Data;
using MySql.Data.MySqlClient;
using connectingToDBTESTING.Models;
 
namespace connectingToDBTESTING.Factory
{
    public class messageFactory : IFactory<Message>
    {
        private string connectionString;
        public messageFactory()
        {
            connectionString = "server=localhost;userid=root;password=root;port=3305;database=c#_users;SslMode=None";
        }
        internal IDbConnection Connection
        {
            get {
                return new MySqlConnection(connectionString);
            }
        }
        public void Add(Message item)
        {
            using (IDbConnection dbConnection = Connection) {
                string query =  $"INSERT INTO messages (text, user_id) VALUES (@model.text, @model.user_id)";
                dbConnection.Open();
                dbConnection.Execute(query, item);
            }
        }
        public IEnumerable<Message> FindAll()
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                return dbConnection.Query<Message>("SELECT * FROM messages");
            }
        }
        public Message FindByID(int id)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                return dbConnection.Query<Message>("SELECT * FROM messages WHERE id = @Id", new { Id = id }).FirstOrDefault();
            }
        }
    }
}