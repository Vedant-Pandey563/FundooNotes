//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using MongoDB.Driver;
//using UserService.Domain.Entities;
//using UserService.Infrastructure.Configurations;

//namespace UserService.Infrastructure.Persistence
//{
//    // This acts like DbContext (but for MongoDB)
//    public class UserDbContext
//    {
//        private readonly IMongoDatabase _database;

//        public UserDbContext(MongoSettings settings)
//        {
//            // Create Mongo client
//            var client = new MongoClient(settings.ConnectionString);

//            // Get database
//            _database = client.GetDatabase(settings.DatabaseName);

//            // Initialize collection
//            Users = _database.GetCollection<User>(settings.UsersCollectionName);
//        }

//        // Mongo collection for Users
//        public IMongoCollection<User> Users { get; }
//    }
//}
