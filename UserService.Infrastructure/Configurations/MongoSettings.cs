using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserService.Infrastructure.Configurations
{
    // This class maps MongoDB settings from appsettings.json
    public class MongoSettings
    {
        // Connection string to MongoDB Atlas/local DB
        public string ConnectionString { get; set; } = string.Empty;

        // Database name
        public string DatabaseName { get; set; } = string.Empty;

        // Collection name for Users
        public string UsersCollectionName { get; set; } = "Users";
    }
}
