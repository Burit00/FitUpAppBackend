using FitUpAppBackend.Application.Exercises.DTO;
using FitUpAppBackend.Application.Exercises.Queries.GetAnalyticData;
using FitUpAppBackend.Core.Common.Services;
using FitUpAppBackend.Core.WorkoutSets.Entities;
using FitUpAppBackend.Infrastructure.DAL.EF.Context;
using FitUpAppBackend.Shared.Abstractions.Queries;
using Microsoft.EntityFrameworkCore;

namespace FitUpAppBackend.Infrastructure.DAL.Exercises.QueryHandlers;

public class GetAnalyticDataQueryHandler : IQueryHandler<GetAnalyticDataQuery, AnalyticDataArrayDto>
{
    /*
     *
     * Weight - 1RM
     * Distance - Max Distance
     *
     */
    private readonly EFContext _context;
    private readonly ICurrentUserService _currentUserService;

    public GetAnalyticDataQueryHandler(EFContext context, ICurrentUserService currentUserService)
    {
        _context = context;
        _currentUserService = currentUserService;
    }

    public async Task<AnalyticDataArrayDto> HandleAsync(GetAnalyticDataQuery query, CancellationToken cancellationToken)
    {
        var exercise =
            await _context.Exercises.FirstOrDefaultAsync(e => e.Id.Equals(query.ExerciseId), cancellationToken);
        var setParameterName =
            await _context.SetParameterNames.FirstOrDefaultAsync(spn => spn.Name == query.ParameterName,
                cancellationToken);

        var valuesQuery = _context.WorkoutSets
            .AsNoTracking()
            .Include(we => we.SetParameters)
            .ThenInclude(sp => sp.SetParameterName)
            .Include(ws => ws.WorkoutExercise)
            .ThenInclude(we => we.Workout)
            .Where(ws => ws.WorkoutExercise.Workout.UserId == _currentUserService.UserId
                         && ws.WorkoutExercise.ExerciseId == exercise.Id
                         && ws.SetParameters.Any(sp => sp.SetParameterNameId == setParameterName.Id));

        if (query.StartDate != null)
            valuesQuery = valuesQuery.Where(sp => sp.WorkoutExercise.Workout.Date >= query.StartDate);
        if (query.EndDate != null)
            valuesQuery = valuesQuery.Where(sp => sp.WorkoutExercise.Workout.Date >= query.EndDate);

        var values = (await valuesQuery.ToListAsync(cancellationToken))
            .GroupBy(ws => ws.WorkoutExercise.Workout.Date)
            .OrderBy(ws => ws.Key.Date);

        var result = new AnalyticDataArrayDto()
        {
            ExerciseId = exercise.Id,
            ExerciseName = exercise.Name,
            ParameterName = query.ParameterName,
            Values = SerializeData(query.ParameterName, values)
        };

        return result;
    }

    private IEnumerable<AnalyticDataValueDto> SerializeData(string parameterName,
        IEnumerable<IGrouping<DateTime, WorkoutSet>> workoutSetsGroupedByDate)
    { 
        IAnalyticDataCalculator analyticDataCalculator;
        switch (parameterName)
        {
            case "weight":
                analyticDataCalculator = new WeightAnalyticDataCalculator();
                break;
            case "distance":
                analyticDataCalculator = new DistanceAnalyticDataCalculator();
                break;
            default:
                return new List<AnalyticDataValueDto>();;
        }

        return workoutSetsGroupedByDate.Select(wsgbd => new AnalyticDataValueDto()
        {
            Date = wsgbd.Key.Date,
            Value = analyticDataCalculator.Calculate(wsgbd.ToList())
        });
    }

    private interface IAnalyticDataCalculator
    {
        public double Calculate(List<WorkoutSet> workoutSets);
    }

    private class WeightAnalyticDataCalculator : IAnalyticDataCalculator
    {
        public double Calculate(List<WorkoutSet> workoutSets)
        {
            var bestOneMaxRep = 0.0;

            foreach (var workoutSet in workoutSets)
            {
                double weight;
                int reps;

                var weightParseSuccess = double.TryParse(
                    workoutSet.SetParameters.First(sp =>
                        sp.SetParameterName.Name == Core.SetParameterNames.Static.SetParameterNames.Weight).Value,
                    out weight);
                var repsParseSuccess = int.TryParse(workoutSet.SetParameters.First(sp =>
                    sp.SetParameterName.Name == Core.SetParameterNames.Static.SetParameterNames.Reps).Value, out reps);

                if (!weightParseSuccess || !repsParseSuccess || reps == 0)
                    continue;

                // One max rep Lombardi formula w * (r ^ 0.1)
                var oneMaxRep = reps == 1 ? weight : weight * Math.Pow(reps, 0.1);
                if (oneMaxRep > bestOneMaxRep) bestOneMaxRep = Math.Round(oneMaxRep, 1);
            }

            return bestOneMaxRep;
        }
    }

    private class DistanceAnalyticDataCalculator : IAnalyticDataCalculator
    {
        public double Calculate(List<WorkoutSet> workoutSets)
        {
            var maxDistance = 0.0;

            foreach (var workoutSet in workoutSets)
            {
                double distance;

                var distanceParseSuccess = double.TryParse(
                    workoutSet.SetParameters.First(sp =>
                        sp.SetParameterName.Name == Core.SetParameterNames.Static.SetParameterNames.Distance).Value,
                    out distance);
                
                if (!distanceParseSuccess) continue;
                
                if(distance > maxDistance) maxDistance = distance;
            }
            return maxDistance;
        }
    }
}