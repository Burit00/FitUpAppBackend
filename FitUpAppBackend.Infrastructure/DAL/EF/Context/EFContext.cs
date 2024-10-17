using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using FitUpAppBackend.Core.ExerciseCategories.Entities;
using FitUpAppBackend.Core.Exercises.Entities;
using FitUpAppBackend.Core.Identity.Entities;
using FitUpAppBackend.Core.SetParameterNames.Entities;
using FitUpAppBackend.Core.SetParameters.Entities;
using FitUpAppBackend.Core.WorkoutExercises.Entities;
using FitUpAppBackend.Core.Workouts.Entities;
using FitUpAppBackend.Core.WorkoutSets.Entities;

namespace FitUpAppBackend.Infrastructure.DAL.EF.Context;

public class EFContext : IdentityDbContext<User, IdentityRole<Guid>, Guid, IdentityUserClaim<Guid>, IdentityUserRole<Guid>, IdentityUserLogin<Guid>, IdentityRoleClaim<Guid>,  IdentityUserToken<Guid>>
{
    public DbSet<Workout> Workouts { get; set; }
    public DbSet<WorkoutExercise> WorkoutExercises { get; set; }
    public DbSet<WorkoutSet> WorkoutSets { get; set; }
    public DbSet<Exercise> Exercises { get; set; }
    public DbSet<ExerciseCategory> ExerciseCategories { get; set; }
    public DbSet<SetParameterName> SetParameterNames { get; set; }
    public DbSet<SetParameter> SetParameters { get; set; }
    
    private readonly Guid _userId = Guid.Empty;

    public EFContext(DbContextOptions<EFContext> options) : base(options)
    {
    }

    public EFContext(DbContextOptions<EFContext> options, IHttpContextAccessor httpContextAccessor) : base(options)
    {
        _ = Guid.TryParse(httpContextAccessor?.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier), out _userId);
    }
}