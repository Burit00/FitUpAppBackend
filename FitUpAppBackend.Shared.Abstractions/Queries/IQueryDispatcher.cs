namespace FitUpAppBackend.Shared.Abstractions.Queries;

public interface IQueryDispatcher
{
    Task<TResult> DispatchAsync<TQuery, TResult>(TQuery query, CancellationToken token) where TQuery : class, IQuery<TResult>;
}