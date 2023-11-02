using DotNetBar.Api.ViewModels;
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

    public async Task<IEnumerable<Bar>> GetBars(CancellationToken token) =>
        await barsCollection.Find(_ => true).ToListAsync(token);

    public async Task UpdateIngredientCount(UpdateBarIngredientData data, CancellationToken token)
    {
        var builder = Builders<Bar>.Filter;
        var filter = builder.Eq(x => x.Id, data.BarId)
                     & builder.ElemMatch(x => x.Inventory.Ingredients,
                         ingredient => ingredient.Name == data.IngredientName);

        var update = Builders<Bar>.Update.Set("Bar.inventory.ingredients.$.count", data.Count);

        await barsCollection.UpdateOneAsync(filter, update, null, token);
    }
}
