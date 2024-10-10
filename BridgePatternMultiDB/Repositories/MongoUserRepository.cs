using BridgePatternMultiDB.Abstractions;
using BridgePatternMultiDB.Model;

namespace BridgePatternMultiDB.Repositories;

public class MongoUserRepository : UserRepository
{
    public MongoUserRepository(IDatabaseOperations databaseOperations) : base(databaseOperations)
    {
    }

    public override void AddUser(User user)
    {
        _databaseOperations.InsertUser(user);
    }

    public override User GetUser(int id)
    {
        return _databaseOperations.FetchUser(id);
    }
}

