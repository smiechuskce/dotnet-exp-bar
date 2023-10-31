namespace DotNetBar.DataAccess.Models;

public class Ingredient
{
    public string Name { get; init; }

    public int Count { get; init; }

    public IngredientCategory Category { get; init; }

    private Ingredient(string name, IngredientCategory category, int count)
    {
        Name = name;
        Count = count;
        Category = category;
    }

    public static Ingredient Create(string name, IngredientCategory category, int count) =>
        new (name, category, count);
}