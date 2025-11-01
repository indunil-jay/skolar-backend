using MediatR;
using Skolar.Domain.Primitives;

namespace Skolar.Application.Primitives;

internal interface IQueryHandler<TQuery,TResponse> : IRequestHandler<TQuery, Result<TResponse>>
    where TQuery : IQuery<TResponse>   
{
}
