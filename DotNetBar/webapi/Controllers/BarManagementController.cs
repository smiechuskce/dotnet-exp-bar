using Microsoft.AspNetCore.Mvc;
using webapi.Models;

namespace webapi.Controllers;

[ApiController]
[Route("[controller]")]
public class BarManagementController : Controller
{
    private readonly Guid BarId = Guid.Parse("61c752e8-993c-4bf2-ba91-6570758a8509");
    private readonly Guid WarehouseId = Guid.Parse("a53ddedf-0a2e-4885-b14a-7760b1c1342a");

    [HttpGet]
    [Route("get-bars")]
    public IActionResult GetBars(CancellationToken token)
    {
        return Ok(GetBars());
    }

    private IEnumerable<Bar> GetBars() => new List<Bar>
    {
        new()
        {
            Id = BarId,
            Name = "The Main",
            WarehouseId = WarehouseId,
            Inventory = new Inventory
            {
                Ingredients = new List<Ingredient>
                {
                    Ingredient.Create("Campari Bitter", IngredientCategory.LowLimit, 15),
                    Ingredient.Create("Jagermeister", IngredientCategory.HighLimit, 12),
                    Ingredient.Create("Bols Blue", IngredientCategory.LowLimit, 7),
                    Ingredient.Create("Angostura Bitters", IngredientCategory.HighLimit, 10),
                    Ingredient.Create("Jack Daniel's Old No.7", IngredientCategory.HighLimit, 20),
                    Ingredient.Create("Johnny Walker Black Label", IngredientCategory.HighLimit, 19),
                    Ingredient.Create("Caol Ila 12", IngredientCategory.LowLimit, 8),
                    Ingredient.Create("Rum-Bar Silver", IngredientCategory.HighLimit, 15),
                    Ingredient.Create("El Jimador Blanco", IngredientCategory.HighLimit, 16),
                    Ingredient.Create("Lemon (kg)", IngredientCategory.HighLimit, 20),
                    Ingredient.Create("Sugar (kg)", IngredientCategory.HighLimit, 20),
                    Ingredient.Create("Orange juice (l)", IngredientCategory.HighLimit, 18)
                }
            }
        }
    };
}