using System.Collections.Generic;
using System.Linq;
using Dapper;
using System.Data;
using MySql.Data.MySqlClient;
using connectingToDBTESTING.Models;
 
namespace connectingToDBTESTING.Factory
{
    public class NinjaFactory : IFactory<Ninja>
    {
        private string connectionString;
        public NinjaFactory()
        {
            connectionString = "server=localhost;userid=root;password=root;port=3305;database=c#_users;SslMode=None";
        }
        internal IDbConnection Connection
        {
            get {
                return new MySqlConnection(connectionString);
            }
        }
        public void Add(Ninja item)
        {
            using (IDbConnection dbConnection = Connection) {
                string query = "INSERT INTO ninjas (name, level, dojo_id, description) VALUES (@name, @level, @dojo_id, @description)";
                dbConnection.Open();
                dbConnection.Execute(query, item);
            }
        }
        public IEnumerable<Ninja> FindAll()
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                return dbConnection.Query<Ninja>("SELECT * FROM ninjas");
            }
        }
        public Ninja FindByID(int id)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                return dbConnection.Query<Ninja>("SELECT * FROM ninjas WHERE id = @Id", new { Id = id }).FirstOrDefault();
            }
        }
        public IEnumerable<Ninja> NinjasForDojoById(int id)
        {
            using (IDbConnection dbConnection = Connection)
            {
                var query = $"SELECT * FROM ninjas JOIN dojos ON ninjas.dojo_id WHERE dojos.id = ninjas.dojo_id AND dojos.id = {id}";
                dbConnection.Open();
        
                var myNinjas = dbConnection.Query<Ninja, Dojo, Ninja>(query, (ninja, dojo) => { ninja.dojo = dojo; return ninja; });
                return myNinjas;
            }
        }
    }
}