using BridgePatternMultiDB.Abstractions;
using BridgePatternMultiDB.Model;

namespace BridgePatternMultiDB.Operations;

public class SqlServerDatabaseOperations : IDatabaseOperations
{
    public void InsertUser(User user)
    {
        // Simulate inserting user into SQL Server
        Console.WriteLine($"Inserting user into SQL Server: {user.Name}");
    }

    public User FetchUser(int id)
    {
        // Simulate fetching user from SQL Server
        Console.WriteLine($"Fetching user with id {id} from SQL Server");
        return new User { Id = id, Name = "SQL Server User", Email = "sqlserveruser@example.com" };
    }
}