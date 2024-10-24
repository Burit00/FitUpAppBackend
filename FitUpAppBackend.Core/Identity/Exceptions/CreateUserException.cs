using FitUpAppBackend.Shared.Abstractions.Exceptions;

namespace FitUpAppBackend.Core.Identity.Exceptions;

public sealed class CreateUserException()
    : BadRequestFitUpException("Password must be 8 to 20 characters long and include at least one of each: lowercase letter, uppercase letter, number, and symbol.");