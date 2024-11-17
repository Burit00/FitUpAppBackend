using FitUpAppBackend.Shared.Abstractions.Exceptions;

namespace FitUpAppBackend.Core.SetParameters.Exceptions;

public class SetParameterNotFoundException() : NotFoundFitUpException("Not found set parameter")
{
    
}