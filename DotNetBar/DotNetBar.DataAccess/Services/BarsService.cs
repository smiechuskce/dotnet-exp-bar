﻿using DotNetBar.DataAccess.Config;
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

    public async Task<bool> UpdateIngredientCount(UpdateBarIngredientData data, CancellationToken token)
    {
        var builder = Builders<Bar>.Filter;
        var filter = builder.Eq(x => x.Id, data.BarId)
                     & builder.ElemMatch(x => x.Inventory.Ingredients,
                         ingredient => ingredient.Name == data.IngredientName);

        var ingredient = await GetIngredient(filter, data.IngredientName, token);

        var update = Builders<Bar>.Update.Set("inventory.ingredients.$.count", ingredient.Count - data.Count);

        var result = await barsCollection.UpdateOneAsync(filter, update, null, token);

        return result.IsAcknowledged;
    }

    private async Task<Ingredient> GetIngredient(FilterDefinition<Bar> barFilter, string name, CancellationToken token)
    {
        var result = barsCollection.Find(barFilter);
        var projection = await result.Project(b => b.Inventory.Ingredients.First(i => i.Name == name))
            .ToListAsync(token);

        return projection.First();
    }
}
