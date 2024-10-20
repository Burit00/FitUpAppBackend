using FitUpAppBackend.Core.Identity.DTO;
using FitUpAppBackend.Shared.Abstractions.Commands;

namespace FitUpAppBackend.Application.Identity.Commands.SignIn;

public sealed record SignInCommand(string Email, string Password) : ICommand<JsonWebToken>;