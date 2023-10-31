using DotNetBar.DataAccess.Config;
using DotNetBar.DataAccess.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace DotNetBar.DataAccess.Services;

public class BarsService
{
    private readonly IMongoClient mongoClient;
    private readonly IMongoCollection<Bar> barsCollection;

    public BarsService(
        IMongoClientWrapper mongoClientWrapper,
        IOptions<BarManagementDatabaseSettings> barManagementDatabaseSettings)
    {
        this.mongoClient = mongoClientWrapper.GetMongoClient();

        var mongoDatabase = mongoClient.GetDatabase(
            barManagementDatabaseSettings.Value.DatabaseName);

        barsCollection = mongoDatabase.GetCollection<Bar>(
            barManagementDatabaseSettings.Value.BarsCollectionName);
    }

    public async Task<IEnumerable<Bar>> GetBars() =>
        await barsCollection.Find(_ => true).ToListAsync();
}
