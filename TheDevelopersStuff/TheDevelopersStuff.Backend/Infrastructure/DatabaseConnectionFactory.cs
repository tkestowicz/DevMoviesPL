using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;

namespace TheDevelopersStuff.Backend.Infrastructure
{
    public static class DatabaseConnectionFactory
    {
        public static MongoDatabase CreateDatabaseConnection(string connectionStringName, string dbName)
        {
            var client = new MongoClient(ConfigurationManager.ConnectionStrings[connectionStringName].ConnectionString);

            return client.GetServer().GetDatabase(dbName);
        }
    }
}
