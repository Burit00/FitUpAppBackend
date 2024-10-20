using FitUpAppBackend.Shared.Abstractions.Queries;
using Microsoft.Extensions.DependencyInjection;

namespace FitUpAppBackend.Shared.Queries;

public sealed class QueryDispatcher : IQueryDispatcher
{
    private readonly IServiceProvider _serviceProvider;

    public QueryDispatcher(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task<TResult> DispatchAsync<TQuery, TResult>(TQuery query, CancellationToken cancellationToken) where TQuery : class, IQuery<TResult>
    {
        using var scope = _serviceProvider.CreateScope();
        var handler = scope.ServiceProvider.GetRequiredService<IQueryHandler<TQuery, TResult>>();
        
        return await handler.HandleAsync(query, cancellationToken);
    }
}