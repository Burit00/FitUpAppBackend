using FitUpAppBackend.Shared.Abstractions.Exceptions;

namespace FitUpAppBackend.Core.Identity.Exceptions;

public class AddRoleException(): BadRequestFitUpException("Error occured during adding user claims.")
{
    
}