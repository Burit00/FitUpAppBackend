using FitUpAppBackend.Core.WorkoutSets.Entities;
using FitUpAppBackend.Core.WorkoutSets.Exceptions;
using FitUpAppBackend.Core.WorkoutSets.Repositories;
using FitUpAppBackend.Infrastructure.DAL.EF.Context;
using Microsoft.EntityFrameworkCore;

namespace FitUpAppBackend.Infrastructure.DAL.WorkoutSets.Repositories;

public class WorkoutSetRepository : IWorkoutSetRepository
{
    private readonly EFContext _context;
    private readonly DbSet<WorkoutSet> _workoutSets;

    public WorkoutSetRepository(EFContext context)
    {
        _context = context;
        _workoutSets = context.WorkoutSets;
    }

    public async Task<Guid> CreateAsync(WorkoutSet workoutSet, CancellationToken cancellationToken)
    {
        var exercise = await _context.Exercises
            .Include(e => e.SetParameters)
            .Include(e => e.WorkoutExercises)
            .FirstOrDefaultAsync(e => e.WorkoutExercises.Any(we => we.Id == workoutSet.WorkoutExerciseId), cancellationToken);

        var exerciseParameterNames = exercise.SetParameters;
        
        foreach (var setParameter in workoutSet.SetParameters)
        {
            var isWorkoutSetParameterCorrect = exerciseParameterNames.Any(pn => pn.Id == setParameter.SetParameterNameId);
            if (!isWorkoutSetParameterCorrect)
                throw new WorkoutSetHasInvalidParameterException();
            var setParameterCount = workoutSet.SetParameters.Count(sp => sp.Id == setParameter.SetParameterNameId);
            if (setParameterCount > 1)
                throw new WorkoutSetHasDuplicateParametersException();
        }
        
        var setsFromWorkout = await _workoutSets.Where(ws => ws.WorkoutExerciseId == workoutSet.WorkoutExerciseId).ToListAsync(cancellationToken);
        ChangeOrder(workoutSet, setsFromWorkout);

        var result = await _workoutSets.AddAsync(workoutSet, cancellationToken);
        _context.UpdateRange(setsFromWorkout);
        await _context.SaveChangesAsync(cancellationToken);
        return result.Entity.Id;
    }

    public async Task<Guid> UpdateAsync(WorkoutSet workoutSet, CancellationToken cancellationToken)
    {
        var setsFromWorkout = await _workoutSets.Where(ws => ws.WorkoutExerciseId == workoutSet.WorkoutExerciseId).ToListAsync(cancellationToken);
        ChangeOrder(workoutSet, setsFromWorkout);
        
        var result = _workoutSets.Update(workoutSet);
        _context.UpdateRange(setsFromWorkout);
        await _context.SaveChangesAsync(cancellationToken);
        return result.Entity.Id;
    }

    public async Task<WorkoutSet> GetByIdAsync(Guid workoutSetId, CancellationToken cancellationToken)
    {
        var result = await _workoutSets.Include(ws => ws.SetParameters).FirstOrDefaultAsync(sp => sp.Id == workoutSetId, cancellationToken);
        
        if (result is null)
            throw new WorkoutSetNotFoundException();
        
        return result;
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        var workoutSet = await _workoutSets.FirstOrDefaultAsync(ws => ws.Id == id, cancellationToken);
        _workoutSets.Remove(workoutSet);
        await _context.SaveChangesAsync(cancellationToken);
    }

    private void ChangeOrder(WorkoutSet workoutSet, List<WorkoutSet> setsFromWorkout)
    {
        if (workoutSet.OrderIndex < 0 || workoutSet.OrderIndex >= setsFromWorkout.Count)
            workoutSet.Update(setsFromWorkout.Count);
        else
        {
            var initialOrderIndex = workoutSet.OrderIndex;
            foreach (var setFromWorkout in setsFromWorkout)
            {
                if (setFromWorkout.OrderIndex > initialOrderIndex)
                    setFromWorkout.Update(setFromWorkout.OrderIndex + 1);
            }
        }
        
        var tempArr = new List<WorkoutSet>();
        tempArr.Add(workoutSet);
        tempArr.AddRange(setsFromWorkout);
        var orderedSets = tempArr.OrderBy(ws => ws.OrderIndex).ToList();

        foreach (var tempWorkoutSet in orderedSets)
        {
            tempWorkoutSet.Update(orderedSets.IndexOf(tempWorkoutSet));
        }
    }
}