using DotNetBar.DataAccess.Config;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace DotNetBar.DataAccess;

public class MongoClientWrapper : IMongoClientWrapper
{
    private readonly IMongoClient client;

    public MongoClientWrapper(
        IOptions<BarManagementDatabaseSettings> barManagementDatabaseSettings)
    {
        client = new MongoClient(barManagementDatabaseSettings.Value.ConnectionString);
    }

    public IMongoClient GetMongoClient()
        => client;
}
