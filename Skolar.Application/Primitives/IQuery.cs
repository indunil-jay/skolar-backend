using MediatR;
using Skolar.Domain.Primitives;

namespace Skolar.Application.Primitives;

public interface IQuery<IResponse> : IRequest<Result<IResponse>>
{
}
