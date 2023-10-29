namespace webapi.Models;

internal class Bar
{
    public Guid Id { get; init; }

    public string Name { get; init; } = default!;

    public Guid WarehouseId { get; init; }

    public Inventory Inventory { get; init; } = new();
}