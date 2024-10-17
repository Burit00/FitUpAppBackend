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

    public async Task<TResult> DispatchAsync<TResult>(IQuery<TResult> query, CancellationToken cancellationToken)
    {
        using var scope = _serviceProvider.CreateScope();
        var handlerType = typeof(IQueryHandler<,>).MakeGenericType(query.GetType(), typeof(TResult));
        var handler = scope.ServiceProvider.GetRequiredService(handlerType) as IQueryHandler<IQuery<TResult>, TResult>;
        
        return await  handler?.HandleAsync(query, cancellationToken)!;
    }
}