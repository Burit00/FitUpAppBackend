using System.Security.Claims;
using FitUpAppBackend.Core.Identity.DTO;
using FitUpAppBackend.Core.Identity.Entities;
using FitUpAppBackend.Core.Identity.Exceptions;
using FitUpAppBackend.Core.Identity.Services;
using FitUpAppBackend.Core.Identity.Static;
using FitUpAppBackend.Core.Integrations.Email.Services;
using FitUpAppBackend.Infrastructure.DAL.EF.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace FitUpAppBackend.Infrastructure.DAL.Identity.Services;

public class IdentityService : IIdentityService 
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly EFContext _dbContext;
    private readonly IEmailService _emailService;
    private readonly ITokenService _tokenService;

    public IdentityService(EFContext dbContext, IEmailService emailService, ITokenService tokenService, UserManager<User> userManager, SignInManager<User> signInManager)
    {
        _dbContext = dbContext;
        _emailService = emailService;
        _tokenService = tokenService;
        _userManager = userManager;
        _signInManager = signInManager;
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
        
        // await SendUserVerificationEmailAsync(newUser);
    }

    public async Task<JsonWebToken> SignInAsync(string email, string password, CancellationToken cancellationToken)
    {
        var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Email == email, cancellationToken);

        if (user is null) throw new BadUserCredentialsException();

        var result = await _signInManager.CheckPasswordSignInAsync(user, password, false);
        if (!result.Succeeded) throw new BadUserCredentialsException();

        var jwt = await GenerateJwtAsync(user, cancellationToken);
        return jwt;
    }

    private async Task<JsonWebToken> GenerateJwtAsync(User user, CancellationToken cancellationToken)
    {
        var roles = await _userManager.GetRolesAsync(user);
        var claims = await _userManager.GetClaimsAsync(user);
        
        var jwt = await _tokenService.GenerateJwtAsync(user.Id, user.Email, roles, claims, cancellationToken);

        return jwt;
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