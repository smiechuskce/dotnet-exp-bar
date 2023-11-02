namespace DotNetBar.DataAccess.Models;

public class UpdateBarIngredientData
{
    public Guid BarId { get; init; }

    public string IngredientName { get; init; } = default!;

    public int Count { get; init; }
}
