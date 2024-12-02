using FitUpAppBackend.Core.WorkoutExercises.Entities;
using FitUpAppBackend.Core.WorkoutExercises.Exceptions;
using FitUpAppBackend.Core.WorkoutExercises.Repositories;
using FitUpAppBackend.Infrastructure.DAL.EF.Context;
using Microsoft.EntityFrameworkCore;

namespace FitUpAppBackend.Infrastructure.DAL.WorkoutExercises.Repositories;

public class WorkoutExerciseRepository : IWorkoutExerciseRepository
{
    private readonly EFContext _context;
    private readonly DbSet<WorkoutExercise> _workoutExercises;

    public WorkoutExerciseRepository(EFContext context)
    {
        _context = context;
        _workoutExercises = _context.WorkoutExercises;
    }

    public Task<IEnumerable<WorkoutExercise>> GetAllAsync(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<WorkoutExercise> GetAsync(Guid workoutExerciseId, CancellationToken cancellationToken)
    {
        var workoutExercise =
            await _workoutExercises.FirstOrDefaultAsync(w => w.Id == workoutExerciseId, cancellationToken);

        if (workoutExercise == null)
            throw new WorkoutExerciseNotFoundException();

        return workoutExercise;
    }

    public async Task<Guid> CreateAsync(WorkoutExercise workoutExercise, CancellationToken cancellationToken)
    {
        var exercisesFromWorkout = await _workoutExercises
            .Where(w => w.WorkoutId == workoutExercise.WorkoutId)
            .ToListAsync(cancellationToken);

        var doesExerciseExistsOnThisDay = exercisesFromWorkout
            .Any(we => we.ExerciseId == workoutExercise.ExerciseId);

        if (doesExerciseExistsOnThisDay)
            throw new WorkoutAlreadyContainsThisExerciseException();

        var lastWorkoutExercises = exercisesFromWorkout.OrderBy(we => we.OrderIndex).LastOrDefault();

        if (lastWorkoutExercises == null)
        {
            workoutExercise.Update(0);
        }
        else if (workoutExercise.OrderIndex < 0 || workoutExercise.OrderIndex >= lastWorkoutExercises.OrderIndex)
        {
            workoutExercise.Update(lastWorkoutExercises.OrderIndex + 1);
        }

        var result = await _workoutExercises.AddAsync(workoutExercise, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return result.Entity.Id;
    }

    public async Task UpdateOrderIndexAsync(Guid workoutExerciseMovedId, Guid workoutExerciseOverId, CancellationToken cancellationToken)
    {
        var workout = _context.Workouts.Include(w => w.WorkoutExercises)
            .FirstOrDefault(w => w.WorkoutExercises.Any(we => we.Id == workoutExerciseMovedId));
        
        if (workout == null || workout.WorkoutExercises.Any(we => we.WorkoutId == workoutExerciseOverId))
            throw new WorkoutExerciseNotFoundException();
        
        var sortedWorkoutExercises = workout.WorkoutExercises.OrderBy(we => we.OrderIndex).ToList();
        var workoutExerciseMoved = sortedWorkoutExercises.Find(we => we.Id == workoutExerciseMovedId);
        var workoutExerciseOverIndex = sortedWorkoutExercises.FindIndex(we => we.Id == workoutExerciseOverId);
        
        sortedWorkoutExercises.Remove(workoutExerciseMoved);
        sortedWorkoutExercises.Insert(workoutExerciseOverIndex, workoutExerciseMoved);        
        
        foreach (var workoutExercise in sortedWorkoutExercises)
        {
            var workoutExerciseIndex = sortedWorkoutExercises.IndexOf(workoutExercise);
            workoutExercise.Update(workoutExerciseIndex);
        }

        _workoutExercises.UpdateRange(sortedWorkoutExercises);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Guid workoutExerciseId, CancellationToken cancellationToken)
    {
        var workoutExercise = await GetAsync(workoutExerciseId, cancellationToken);

        _workoutExercises.Remove(workoutExercise);

        var workout = await _context.Workouts.Include(w => w.WorkoutExercises)
            .FirstOrDefaultAsync(w => w.Id == workoutExercise.WorkoutId, cancellationToken);

        if (workout is not null && !workout.WorkoutExercises.Any())
            _context.Workouts.Remove(workout);
        await _context.SaveChangesAsync(cancellationToken);
    }
}