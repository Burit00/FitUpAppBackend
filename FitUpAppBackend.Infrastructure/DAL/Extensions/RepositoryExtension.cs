using FitUpAppBackend.Core.ExerciseCategories.Repositories;
using FitUpAppBackend.Core.Exercises.Repositories;
using FitUpAppBackend.Core.WorkoutExercises.Repositories;
using FitUpAppBackend.Core.Workouts.Repositories;
using FitUpAppBackend.Infrastructure.DAL.ExerciseCategories.Repositories;
using FitUpAppBackend.Infrastructure.DAL.Exercises.Repositories;
using FitUpAppBackend.Infrastructure.DAL.WorkoutExercises.Repositories;
using FitUpAppBackend.Infrastructure.DAL.Workouts.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace FitUpAppBackend.Infrastructure.DAL.Extensions;

public static class RepositoryExtension
{
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IWorkoutRepository, WorkoutRepository>();
        services.AddScoped<IExerciseCategoryRepository, ExerciseCategoryRepository>();
        services.AddScoped<IExerciseRepository, ExerciseRepository>();
        services.AddScoped<IWorkoutExerciseRepository, WorkoutExerciseRepository>();

        return services;
    }
}