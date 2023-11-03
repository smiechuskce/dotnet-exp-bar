using DotNetBar.Api.CQRS.Commands;
using DotNetBar.Api.CQRS.Queries;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DotNetBar.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class BarManagementController : Controller
{
    private readonly IMediator mediator;
    
    public BarManagementController(IMediator mediator)
    {
        this.mediator = mediator;
    }

    [HttpGet]
    [Route("get-bars")]
    public async Task<IActionResult> GetBars(CancellationToken cancellationToken)
    {
        return Ok(await this.mediator.Send(new GetBars.Query(), cancellationToken));
    }

    [HttpPut]
    [Route("update-ingredient-count")]
    public async Task<IActionResult> UpdateBarIngredient(
        [FromBody]UpdateBarIngredient.Command data,
        [FromServices]IValidator<UpdateBarIngredient.Command> validator,
        CancellationToken cancellationToken)
    {
        var validationResult = await validator.ValidateAsync(data, cancellationToken);

        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.Errors);
        }

        var result =  await this.mediator.Send(data, cancellationToken);
        
        return result.IsSuccess ? Ok()
            : BadRequest("The was an error while updating ingredient");
    }
}