using System.Security.Claims;
using FitUpAppBackend.Core.ExerciseCategories.Entities;
using FitUpAppBackend.Core.Exercises.Entities;
using FitUpAppBackend.Core.Identity.Entities;
using FitUpAppBackend.Core.SetParameterNames.Entities;
using FitUpAppBackend.Core.SetParameters.Entities;
using FitUpAppBackend.Core.WorkoutExercises.Entities;
using FitUpAppBackend.Core.Workouts.Entities;
using FitUpAppBackend.Core.WorkoutSets.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

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

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        
        builder.Entity<SetParameter>()
            .HasOne(e => e.WorkoutSet)
            .WithMany(e => e.SetParameters)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.Entity<WorkoutSet>()
            .HasOne(e => e.WorkoutExercise)
            .WithMany(e => e.WorkoutSets)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.Entity<WorkoutExercise>()
            .HasOne(e => e.Workout)
            .WithMany(e => e.WorkoutExercises)
            .OnDelete(DeleteBehavior.Cascade);
    }
}