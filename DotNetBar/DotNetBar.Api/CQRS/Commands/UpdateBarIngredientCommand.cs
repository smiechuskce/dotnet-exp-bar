using DotNetBar.DataAccess.Models;
using DotNetBar.DataAccess.Services;
using FluentValidation;
using MediatR;

namespace DotNetBar.Api.CQRS.Commands;

public static class UpdateBarIngredient
{
    public sealed class Command : IRequest<CommandResult>
    {
        public Guid BarId { get; init; }

        public string IngredientName { get; init; } = default!;

        public int Count { get; init; }
    }

    public sealed class CommandResult
    {
        public bool IsSuccess { get; init; }
    }

    public sealed class CommandValidator : AbstractValidator<Command>
    {
        public CommandValidator()
        {
            RuleFor(c => c.BarId)
                .NotNull();

            RuleFor(c => c.IngredientName)
                .NotEmpty();

            RuleFor(c => c.Count)
                .GreaterThan(0);
        }
    }

    public sealed class CommandHandler: IRequestHandler<Command, CommandResult>
    {
        private readonly BarsService barsService;

        public CommandHandler(BarsService barsService)
        {
            this.barsService = barsService;
        }

        public async Task<CommandResult> Handle(Command request, CancellationToken cancellationToken)
        {
            var result = await this.barsService.UpdateIngredientCount(new UpdateBarIngredientData
            {
                BarId = request.BarId,
                Count = request.Count,
                IngredientName = request.IngredientName,
            }, cancellationToken);

            return new CommandResult
            {
                IsSuccess = result
            };
        }
    }
}
