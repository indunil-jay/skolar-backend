using MediatR;
using Skolar.Domain.Shared;

namespace Skolar.Application.Primitives;

public interface IQuery<IResponse> : IRequest<Result<IResponse>>
{
}
