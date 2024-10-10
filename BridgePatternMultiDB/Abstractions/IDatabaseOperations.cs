using BridgePatternMultiDB.Model;

namespace BridgePatternMultiDB.Abstractions;
public interface IDatabaseOperations
{
    void InsertUser(User user);
    User FetchUser(int id);
}