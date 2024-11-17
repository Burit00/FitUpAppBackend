using FitUpAppBackend.Core.ExerciseCategories.Repositories;
using FitUpAppBackend.Core.Exercises.Repositories;
using FitUpAppBackend.Core.SetParameters.Repositories;
using FitUpAppBackend.Core.WorkoutExercises.Repositories;
using FitUpAppBackend.Core.Workouts.Repositories;
using FitUpAppBackend.Core.WorkoutSets.Repositories;
using FitUpAppBackend.Infrastructure.DAL.ExerciseCategories.Repositories;
using FitUpAppBackend.Infrastructure.DAL.Exercises.Repositories;
using FitUpAppBackend.Infrastructure.DAL.SetParameters;
using FitUpAppBackend.Infrastructure.DAL.WorkoutExercises.Repositories;
using FitUpAppBackend.Infrastructure.DAL.Workouts.Repositories;
using FitUpAppBackend.Infrastructure.DAL.WorkoutSets.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace FitUpAppBackend.Infrastructure.DAL.Extensions;

public static class RepositoryExtension
{
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IWorkoutRepository, WorkoutRepository>();
        services.AddScoped<ISetParameterRepository, SetParameterRepository>();
        services.AddScoped<IExerciseCategoryRepository, ExerciseCategoryRepository>();
        services.AddScoped<IExerciseRepository, ExerciseRepository>();
        services.AddScoped<IWorkoutExerciseRepository, WorkoutExerciseRepository>();
        services.AddScoped<IWorkoutSetRepository, WorkoutSetRepository>();

        return services;
    }
}