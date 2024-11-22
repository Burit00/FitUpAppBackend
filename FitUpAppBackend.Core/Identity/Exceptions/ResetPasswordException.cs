using FitUpAppBackend.Shared.Abstractions.Exceptions;

namespace FitUpAppBackend.Core.Identity.Exceptions;

public class ResetPasswordException() : BadRequestFitUpException("Error occured during reset password.");