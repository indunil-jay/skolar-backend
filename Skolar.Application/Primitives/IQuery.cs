using MediatR;

namespace Skolar.Application.Primitives;

public interface IQuery<IResponse> : IRequest<IResponse>
{
}
