using DotNetBar.DataAccess.Models;
using DotNetBar.DataAccess.Services;
using Microsoft.AspNetCore.Mvc;

namespace DotNetBar.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class BarManagementController : Controller
{
    private readonly BarsService barsService;

    private readonly Guid BarId = Guid.Parse("61c752e8-993c-4bf2-ba91-6570758a8509");
    private readonly Guid WarehouseId = Guid.Parse("a53ddedf-0a2e-4885-b14a-7760b1c1342a");

    public BarManagementController(BarsService barsService)
    {
        this.barsService = barsService;
    }

    [HttpGet]
    [Route("get-bars")]
    public async Task<IActionResult> GetBars(CancellationToken token)
    {
        return Ok(await this.barsService.GetBars(token));
    }

    [HttpPut]
    [Route("update-ingredient-count")]
    public async Task<IActionResult> UpdateBarIngredient([FromBody]UpdateBarIngredientData data, CancellationToken token)
    {
        return await this.barsService.UpdateIngredientCount(data, token)
            ? Ok()
            : BadRequest("The was an error while updating ingredient");
    }
}