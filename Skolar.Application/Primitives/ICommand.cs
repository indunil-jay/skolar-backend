using MediatR;

namespace Skolar.Application.Primitives;


public interface ICommandBase { }
public interface ICommand : IRequest<Unit>, ICommandBase    
{
}


public interface ICommand<TResponse> : IRequest<TResponse>, ICommandBase
{
}   