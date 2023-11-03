using DotNetBar.DataAccess.Models;
using DotNetBar.DataAccess.Services;
using MediatR;

namespace DotNetBar.Api.CQRS.Queries;

public static class GetBars
{
    public sealed class Query : IRequest<QueryResult>
    {

    }

    public sealed class QueryResult
    {
        public IEnumerable<Bar> Bars { get; init; }
    }

    public sealed class QueryHandler: IRequestHandler<Query, QueryResult>
    {
        private readonly BarsService barsService;

        public QueryHandler(BarsService barsService)
        {
            this.barsService = barsService;
        }

        public async Task<QueryResult> Handle(Query request, CancellationToken cancellationToken)
        {
            return new QueryResult
            {
                Bars = await this.barsService.GetBars(cancellationToken)
            };
        }
    }
}
