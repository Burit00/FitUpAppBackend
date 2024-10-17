using System.Security.Claims;
using FitUpAppBackend.Core.Identity;
using FitUpAppBackend.Core.Identity.Entities;
using FitUpAppBackend.Core.Identity.Exceptions;
using FitUpAppBackend.Core.Identity.Static;
using FitUpAppBackend.Infrastructure.DAL.EF.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace FitUpAppBackend.Infrastructure.DAL.Identity.Services;

public class IdentityService : IIdentityService 
{
    private readonly UserManager<User> _userManager;
    private readonly EFContext _dbContext;

    public IdentityService(EFContext dbContext, UserManager<User> userManager)
    {
        _dbContext = dbContext;
        _userManager = userManager;
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
    }
}