using System.Security.Claims;
using FitUpAppBackend.Core.Identity.DTO;
using FitUpAppBackend.Core.Identity.Entities;
using FitUpAppBackend.Core.Identity.Exceptions;
using FitUpAppBackend.Core.Identity.Services;
using FitUpAppBackend.Core.Identity.Static;
using FitUpAppBackend.Infrastructure.DAL.EF.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace FitUpAppBackend.Infrastructure.DAL.Identity.Services;

public class IdentityService : IIdentityService 
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly EFContext _dbContext;
    private readonly ITokenService _tokenService;

    public IdentityService(EFContext dbContext, ITokenService tokenService, UserManager<User> userManager, SignInManager<User> signInManager)
    {
        _dbContext = dbContext;
        _tokenService = tokenService;
        _userManager = userManager;
        _signInManager = signInManager;
    }
    
    public async Task SignUpAsync(string email, string password, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByEmailAsync(email);

        if (user == null)
            throw new UserWithEmailAlreadyExistException(email);
            
        await using var transaction = await _dbContext.Database.BeginTransactionAsync(cancellationToken);

        var newUser = new User
        {
            Email = email,
            UserName = email,
        };
        
        var createdUser = await _userManager.CreateAsync(newUser, password);

        if (!createdUser.Succeeded)
            throw new CreateUserException();

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
    }

    public async Task<JsonWebToken> SignInAsync(string email, string password, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByEmailAsync(email);
        
        if (user is null) throw new BadUserCredentialsException();
        if (!user.EmailConfirmed) throw new EmailNotVerifiedException();
        
        var result = await _signInManager.CheckPasswordSignInAsync(user, password, false);
        if (!result.Succeeded) throw new BadUserCredentialsException();

        var jwt = await GenerateJwtAsync(user, cancellationToken);
        return jwt;
    }

    public async Task<EmailConfirmationTokenDto> GenerateEmailConfirmationTokenAsync(string email, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByEmailAsync(email);
        var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
        var encodedToken = Uri.EscapeDataString(token);
        
        return new EmailConfirmationTokenDto
        {
            Token = encodedToken,
            UserId = user.Id,
        };
    }

    public async Task<ResetPasswordTokenDto> GeneratePasswordResetTokenAsync(string email, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByEmailAsync(email);

        if (user == null)
            throw new ResetPasswordRequestException();
        
        var token = await _userManager.GeneratePasswordResetTokenAsync(user);
        return new ResetPasswordTokenDto()
        {
            Token =  Uri.EscapeDataString(token),
            UserId = user.Id,
        };
    }

    public async Task ResetPasswordAsync(Guid userId, string password, string resetPasswordToken, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByIdAsync(userId.ToString());
        
        if(user is null) throw new ResetPasswordException();
        
        var decodedToken = Uri.UnescapeDataString(resetPasswordToken);
        
        var result = await _userManager.ResetPasswordAsync(user, decodedToken, password);
        Console.WriteLine(result.Errors);
        if (!result.Succeeded)
            throw new ResetPasswordException();
    }

    public async Task<User> GetUserByIdAsync(Guid userId, CancellationToken cancellationToken)
    {
        var user = await _userManager.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Id == userId, cancellationToken);
        
        if(user is null)
            throw new UserNotFoundException();

        return user;
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

        if (user == null)
            throw new EmailVerificationException();
        
        var decodedToken = Uri.UnescapeDataString(token);
        
        var result = await _userManager.ConfirmEmailAsync(user, decodedToken);
        
        if(!result.Succeeded) throw new EmailVerificationException();
    }
    
}