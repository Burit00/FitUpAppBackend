using FitUpAppBackend.Shared.Abstractions.Exceptions;
using Microsoft.AspNetCore.Identity;

namespace FitUpAppBackend.Core.Identity.Exceptions;

public sealed class CreateUserException : FitUpException
{
    public IDictionary<string, string[]> Errors { get; } = new Dictionary<string, string[]>();

    public CreateUserException() : base("One or more errors occurred during creating user.")
    {
    }

    public CreateUserException(IEnumerable<IdentityError> errors) : base(
        "One or more errors occurred during creating user.")
    {
        Errors = errors.GroupBy(error => error.Code, error => error.Description)
            .ToDictionary(group => group.Key, group => group.ToArray());
    }
}