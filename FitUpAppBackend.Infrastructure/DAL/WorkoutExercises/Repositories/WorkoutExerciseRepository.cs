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

    public Task<WorkoutExercise> GetAsync(Guid workoutId, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
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

        CorrectOrder(workoutExercise, exercisesFromWorkout);

        _workoutExercises.UpdateRange(exercisesFromWorkout);
        var result = await _workoutExercises.AddAsync(workoutExercise, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        
        return result.Entity.Id;
    }

    public Task<Guid> UpdateAsync(WorkoutExercise workoutExercise, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task DeleteAsync(Guid workoutExerciseId, CancellationToken cancellationToken)
    {
        var workoutExercise = await _workoutExercises.FirstOrDefaultAsync(w => w.Id == workoutExerciseId, cancellationToken);
    
        if (workoutExercise is null)
            throw new WorkoutExerciseNotFoundException();
        
        var workout = await _context.Workouts.Include(w => w.WorkoutExercises).FirstOrDefaultAsync(w => w.Id == workoutExercise.WorkoutId, cancellationToken);
        _workoutExercises.Remove(workoutExercise);
        if(workout is not null && workout.WorkoutExercises.Count == 1)
            _context.Workouts.Remove(workout);
        await _context.SaveChangesAsync(cancellationToken);
    }

    private void CorrectOrder(WorkoutExercise workoutExercise, List<WorkoutExercise> exercisesFromWorkout)
    {
        if (workoutExercise.OrderIndex < 0 || workoutExercise.OrderIndex >= exercisesFromWorkout.Count)
            workoutExercise.Update(exercisesFromWorkout.Count);
        else
        {
            var initialOrderIndex = workoutExercise.OrderIndex;
            foreach (var exerciseFromWorkout in exercisesFromWorkout)
            {
                if (exerciseFromWorkout.OrderIndex > initialOrderIndex)
                    exerciseFromWorkout.Update(exerciseFromWorkout.OrderIndex + 1);
            }
        }
        
        var tempArr = new List<WorkoutExercise>();
        tempArr.Add(workoutExercise);
        tempArr.AddRange(exercisesFromWorkout);
        var orderedTempArr = tempArr.OrderBy(w => w.OrderIndex).ToList();
        
        foreach (var tempWorkoutExercise in orderedTempArr)
        {
            tempWorkoutExercise.Update(orderedTempArr.IndexOf(tempWorkoutExercise));
        }
    }
}