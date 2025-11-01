using MediatR;

namespace Skolar.Application.Primitives;

internal interface IQueryHandler<TQuery,TResponse> : IRequestHandler<TQuery, TResponse>
    where TQuery : IQuery<TResponse>   
{
}
