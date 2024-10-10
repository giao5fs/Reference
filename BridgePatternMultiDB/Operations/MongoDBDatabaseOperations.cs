using BridgePatternMultiDB.Abstractions;
using BridgePatternMultiDB.Model;

namespace BridgePatternMultiDB.Operations;

public class MongoDBDatabaseOperations : IDatabaseOperations
{
    public void InsertUser(User user)
    {
        // Simulate inserting user into MongoDB
        Console.WriteLine($"Inserting user into MongoDB: {user.Name}");
    }

    public User FetchUser(int id)
    {
        // Simulate fetching user from MongoDB
        Console.WriteLine($"Fetching user with id {id} from MongoDB");
        return new User { Id = id, Name = "MongoDB User", Email = "mongodbuser@example.com" };
    }
}