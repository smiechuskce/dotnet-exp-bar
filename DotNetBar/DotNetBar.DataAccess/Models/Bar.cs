using MongoDB.Bson.Serialization.Attributes;

namespace DotNetBar.DataAccess.Models;

public class Bar
{
    [BsonId]
    public Guid Id { get; init; }

    public string Name { get; init; } = default!;

    public Guid WarehouseId { get; init; }

    public Inventory Inventory { get; init; } = new();
}