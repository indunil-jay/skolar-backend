using MediatR;
using Skolar.Domain.Shared;

namespace Skolar.Application.Primitives;


public interface ICommandBase { }
public interface ICommand : IRequest<Result>, ICommandBase    
{
}


public interface ICommand<TResponse> : IRequest<Result<TResponse>>, ICommandBase
{
}   