using BridgePatternMultiDB.Abstractions;
using BridgePatternMultiDB.Model;

namespace BridgePatternMultiDB.Operations;

public class MySqlDatabaseOperations : IDatabaseOperations
{
    public void InsertUser(User user)
    {
        // Simulate inserting user into MySQL
        Console.WriteLine($"Inserting user into MySQL: {user.Name}");
    }

    public User FetchUser(int id)
    {
        // Simulate fetching user from MySQL
        Console.WriteLine($"Fetching user with id {id} from MySQL");
        return new User { Id = id, Name = "MySQL User", Email = "mysqluser@example.com" };
    }
}