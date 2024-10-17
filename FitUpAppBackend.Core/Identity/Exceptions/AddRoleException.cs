using FitUpAppBackend.Shared.Abstractions.Exceptions;

namespace FitUpAppBackend.Core.Identity.Exceptions;

public class AddRoleException(): FitUpException("Error occured during adding user claims.")
{
    
}