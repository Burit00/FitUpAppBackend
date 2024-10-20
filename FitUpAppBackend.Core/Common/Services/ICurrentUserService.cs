namespace FitUpAppBackend.Core.Common.Services;

public interface ICurrentUserService
{
    public Guid UserId { get; }
}