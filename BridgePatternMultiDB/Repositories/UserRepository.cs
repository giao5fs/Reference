using BridgePatternMultiDB.Abstractions;
using BridgePatternMultiDB.Model;

namespace BridgePatternMultiDB.Repositories;

public abstract class UserRepository
{
    protected IDatabaseOperations _databaseOperations;

    protected UserRepository(IDatabaseOperations databaseOperations)
    {
        _databaseOperations = databaseOperations;
    }

    public abstract void AddUser(User user);
    public abstract User GetUser(int id);
}
