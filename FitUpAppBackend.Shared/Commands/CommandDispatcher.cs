using FitUpAppBackend.Shared.Abstractions.Commands;
using Microsoft.Extensions.DependencyInjection;

namespace FitUpAppBackend.Shared.Commands;

public sealed class CommandDispatcher: ICommandDispatcher    
{
    private readonly IServiceProvider _serviceProvider;

    public CommandDispatcher(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }
    
    public async Task DispatchAsync<TCommand>(TCommand command, CancellationToken cancellationToken) where TCommand : class, ICommand
    {
        using var scope = _serviceProvider.CreateScope();
        var handler = scope.ServiceProvider.GetRequiredService<ICommandHandler<TCommand>>();
        
        await handler.HandleAsync(command, cancellationToken); 
    }
}