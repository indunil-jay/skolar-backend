using MediatR;
using Skolar.Domain.Primitives;

namespace Skolar.Application.Primitives;


public interface ICommandBase { }
public interface ICommand : IRequest<Result>, ICommandBase    
{
}


public interface ICommand<TResponse> : IRequest<Result<TResponse>>, ICommandBase
{
}   