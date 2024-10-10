using BridgePatternMultiDB.Abstractions;
using BridgePatternMultiDB.Model;
using BridgePatternMultiDB.Operations;
using BridgePatternMultiDB.Repositories;

public class Program
{
    public static void Main(string[] args)
    {
        User user = new User { Id = 1, Name = "John Doe", Email = "john.doe@example.com" };

        // Using SQL Server
        IDatabaseOperations sqlOperations = new SqlServerDatabaseOperations();
        UserRepository sqlRepository = new SqlUserRepository(sqlOperations);
        sqlRepository.AddUser(user);
        User sqlUser = sqlRepository.GetUser(1);

        // Using MySQL
        IDatabaseOperations mySqlOperations = new MySqlDatabaseOperations();
        UserRepository mySqlRepository = new MySqlUserRepository(mySqlOperations);
        mySqlRepository.AddUser(user);
        User mySqlUser = mySqlRepository.GetUser(1);

        // Using MongoDB
        IDatabaseOperations mongoOperations = new MongoDBDatabaseOperations();
        UserRepository mongoRepository = new MongoUserRepository(mongoOperations);
        mongoRepository.AddUser(user);
        User mongoUser = mongoRepository.GetUser(1);
    }
}
