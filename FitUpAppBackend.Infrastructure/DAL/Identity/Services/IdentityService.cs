using System.Security.Claims;
using FitUpAppBackend.Core.Identity;
using FitUpAppBackend.Core.Identity.Entities;
using FitUpAppBackend.Core.Identity.Exceptions;
using FitUpAppBackend.Core.Identity.Static;
using FitUpAppBackend.Core.Integrations.Email.Services;
using FitUpAppBackend.Infrastructure.DAL.EF.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace FitUpAppBackend.Infrastructure.DAL.Identity.Services;

public class IdentityService : IIdentityService 
{
    private readonly UserManager<User> _userManager;
    private readonly EFContext _dbContext;
    private readonly IEmailService _emailService;

    public IdentityService(EFContext dbContext, UserManager<User> userManager, IEmailService emailService)
    {
        _dbContext = dbContext;
        _userManager = userManager;
        _emailService = emailService;
    }
    
    public async Task SignUpAsync(string email, string password, CancellationToken cancellationToken)
    {
        var isEmailUnique = await _userManager.Users.AllAsync(user => user.Email != email, cancellationToken);

        if (!isEmailUnique)
            throw new UserWithEmailAlreadyExistException(email);
            
        await using var transaction = await _dbContext.Database.BeginTransactionAsync(cancellationToken);

        var newUser = new User
        {
            Email = email,
            UserName = email,
        };
        
        var createdUser = await _userManager.CreateAsync(newUser, password);

        if (!createdUser.Succeeded)
            throw new CreateUserException(createdUser.Errors);

        var addRoleResult = await _userManager.AddToRoleAsync(newUser, UserRoles.User);
        if(!addRoleResult.Succeeded)
            throw new AddRoleException();
        
        var claims = new Dictionary<string, string>
        {
            [ClaimTypes.Email] = email,
            [ClaimTypes.NameIdentifier] = newUser.Id.ToString(),
        };
            
        foreach (KeyValuePair<string, string> claim in claims)
        {
            var addClaimResult = await _userManager.AddClaimAsync(newUser, new Claim(claim.Key, claim.Value));
            if (!addClaimResult.Succeeded)
                throw new AddClaimException();
        }
        
        await transaction.CommitAsync(cancellationToken);
        
        await SendUserVerificationEmailAsync(newUser);
    }

    public async Task ConfirmEmailAsync(Guid userId, string token, CancellationToken cancellationToken)
    {
        var user = _userManager.Users.SingleOrDefault(u => u.Id == userId);
        await _userManager.ConfirmEmailAsync(user, token);
    }

    private async Task SendUserVerificationEmailAsync(User user)
    {
        var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
        var verifyEmailUrl = $"https://localhost:3000/auth/confirmEmail?userId={user.Id}&token={token}";
        var body = $"Hi,\nTo verify your email address click link below:\n<a href=\"{verifyEmailUrl}\">{verifyEmailUrl}</a>";
        
        await _emailService.SendAsync(user.Email, "Email Verification", token);
    }
    
}