using FitUpAppBackend.Core.ExerciseCategories.Entities;
using FitUpAppBackend.Core.Exercises.Entities;
using FitUpAppBackend.Core.SetParameterNames.Entities;
using FitUpAppBackend.Infrastructure.DAL.EF.Context;
using Microsoft.EntityFrameworkCore;

namespace FitUpAppBackend.Infrastructure.DAL.EF.Seeder.CategoriesAndExercises;

public static class CategoriesAndExercisesSeeder
{
    private static IEnumerable<string> _categoryNames;
    private static IEnumerable<ExerciseRecord> _exercises;

    private static string ChestCategoryName = "Klatka piersiowa";
    private static string ShouldersCategoryName = "Ramiona";
    private static string TricepsCategoryName = "Triceps";
    private static string BicepsCategoryName = "Biceps";
    private static string BackCategoryName = "Plecy";
    private static string LegsCategoryName = "Nogi";
    private static string AbsCategoryName = "Brzuch";
    private static string CardioCategoryName = "Cardio";

    private static ExerciseRecord FlatBenchPressExercise =
        new ExerciseRecord("Wyciskanie sztangi na ławce poziomej", ChestCategoryName, new List<string>()
        {
            Core.SetParameterNames.Static.SetParameterNames.Weight,
            Core.SetParameterNames.Static.SetParameterNames.Reps,
        });

    private static ExerciseRecord FlyExercise = new ExerciseRecord("Rozpiętki", ChestCategoryName, new List<string>()
    {
        Core.SetParameterNames.Static.SetParameterNames.Weight,
        Core.SetParameterNames.Static.SetParameterNames.Reps,
    });
    private static ExerciseRecord PushUpsExercise = new ExerciseRecord("Pompki", ChestCategoryName, new List<string>()
    {
        Core.SetParameterNames.Static.SetParameterNames.Weight,
        Core.SetParameterNames.Static.SetParameterNames.Reps,
    });

    private static ExerciseRecord PullUpExercise = new ExerciseRecord("Podciąganie nachwytem", BackCategoryName, new List<string>()
    {
        Core.SetParameterNames.Static.SetParameterNames.Weight,
        Core.SetParameterNames.Static.SetParameterNames.Reps,
    });
    private static ExerciseRecord BarbelRowExercise = new ExerciseRecord("Wiosłowanie sztangą", BackCategoryName, new List<string>()
    {
        Core.SetParameterNames.Static.SetParameterNames.Weight,
        Core.SetParameterNames.Static.SetParameterNames.Reps,
    });

    private static ExerciseRecord SeatedCableRowExercise =
        new ExerciseRecord("Wiosłowanie na lince", BackCategoryName, new List<string>()
        {
            Core.SetParameterNames.Static.SetParameterNames.Weight,
            Core.SetParameterNames.Static.SetParameterNames.Reps,
        });

    private static ExerciseRecord OverheadPressExercise =
        new ExerciseRecord("Wyciskanie nad głowę sztangą", ShouldersCategoryName, new List<string>()
        {
            Core.SetParameterNames.Static.SetParameterNames.Weight,
            Core.SetParameterNames.Static.SetParameterNames.Reps,
        });

    private static ExerciseRecord DumbbellLateralRaiseExercise =
        new ExerciseRecord("Wznosy bokiem", ShouldersCategoryName, new List<string>()
        {
            Core.SetParameterNames.Static.SetParameterNames.Weight,
            Core.SetParameterNames.Static.SetParameterNames.Reps,
        });

    private static ExerciseRecord DumbbellFrontRaiseExercise =
        new ExerciseRecord("Wzonsy przodem", ShouldersCategoryName, new List<string>()
        {
            Core.SetParameterNames.Static.SetParameterNames.Weight,
            Core.SetParameterNames.Static.SetParameterNames.Reps,
        });

    private static ExerciseRecord FrenchPressExercise =
        new ExerciseRecord("Wyciskanie francuskie", TricepsCategoryName, new List<string>()
        {
            Core.SetParameterNames.Static.SetParameterNames.Weight,
            Core.SetParameterNames.Static.SetParameterNames.Reps,
        });

    private static ExerciseRecord DipsExercise = new ExerciseRecord("Pompki na poręczach", TricepsCategoryName, new List<string>()
        {
            Core.SetParameterNames.Static.SetParameterNames.Weight,
            Core.SetParameterNames.Static.SetParameterNames.Reps,
        });

    private static ExerciseRecord RopePushDownExercise =
        new ExerciseRecord("Prostowanie przedramion linkami na wyciągu górnym", TricepsCategoryName, new List<string>()
        {
            Core.SetParameterNames.Static.SetParameterNames.Weight,
            Core.SetParameterNames.Static.SetParameterNames.Reps,
        });

    private static ExerciseRecord DumbbellCurlExercise =
        new ExerciseRecord("Uginanie przedramion z hantlami", BicepsCategoryName, new List<string>()
        {
            Core.SetParameterNames.Static.SetParameterNames.Weight,
            Core.SetParameterNames.Static.SetParameterNames.Reps,
        });

    private static ExerciseRecord BarbellCurlExercise =
        new ExerciseRecord("Uginanie przedramion z sztangą", BicepsCategoryName, new List<string>()
        {
            Core.SetParameterNames.Static.SetParameterNames.Weight,
            Core.SetParameterNames.Static.SetParameterNames.Reps,
        });

    private static ExerciseRecord CableCurlExercise =
        new ExerciseRecord("Uginanie przedramion na wyciągu dolnym", BicepsCategoryName, new List<string>()
        {
            Core.SetParameterNames.Static.SetParameterNames.Weight,
            Core.SetParameterNames.Static.SetParameterNames.Reps,
        });

    private static ExerciseRecord SquatExercise = new ExerciseRecord("Przysiady", LegsCategoryName, new List<string>()
        {
            Core.SetParameterNames.Static.SetParameterNames.Weight,
            Core.SetParameterNames.Static.SetParameterNames.Reps,
        });
    private static ExerciseRecord DeadliftExercise = new ExerciseRecord("Martwy ciąg", LegsCategoryName, new List<string>()
        {
            Core.SetParameterNames.Static.SetParameterNames.Weight,
            Core.SetParameterNames.Static.SetParameterNames.Reps,
        });

    private static ExerciseRecord LegPressExercise =
        new ExerciseRecord("Wypychanie ciężaru na suwnicy", LegsCategoryName, new List<string>()
        {
            Core.SetParameterNames.Static.SetParameterNames.Weight,
            Core.SetParameterNames.Static.SetParameterNames.Reps,
        });

    private static ExerciseRecord SitUpExercise = new ExerciseRecord("Brzuszki", AbsCategoryName, new List<string>()
        {
            Core.SetParameterNames.Static.SetParameterNames.Weight,
            Core.SetParameterNames.Static.SetParameterNames.Reps,
        });

    private static ExerciseRecord CableCrunchExercise =
        new ExerciseRecord("Spięcia brzucha na wyciągu", AbsCategoryName, new List<string>()
        {
            Core.SetParameterNames.Static.SetParameterNames.Weight,
            Core.SetParameterNames.Static.SetParameterNames.Reps,
        });

    private static ExerciseRecord HangingKneeRaiseExercise =
        new ExerciseRecord("Przyciąganie kolan do klatki w zwisie na drążku", AbsCategoryName, new List<string>()
        {
            Core.SetParameterNames.Static.SetParameterNames.Weight,
            Core.SetParameterNames.Static.SetParameterNames.Reps,
        });

    private static ExerciseRecord RunningOutdoorExercise =
        new ExerciseRecord("Bieganie na zewnątrz", CardioCategoryName, new List<string>()
        {
            Core.SetParameterNames.Static.SetParameterNames.Time,
            Core.SetParameterNames.Static.SetParameterNames.Distance,
        });

    private static ExerciseRecord RunningThreadmillExercise =
        new ExerciseRecord("Bieganie na bieżni", CardioCategoryName, new List<string>()
        {
            Core.SetParameterNames.Static.SetParameterNames.Time,
            Core.SetParameterNames.Static.SetParameterNames.Distance,
        });

    private static ExerciseRecord CyclingExercise = new ExerciseRecord("Jazda na rowerze", CardioCategoryName, new List<string>()
    {
        Core.SetParameterNames.Static.SetParameterNames.Time,
        Core.SetParameterNames.Static.SetParameterNames.Distance,
    });


    static CategoriesAndExercisesSeeder()
    {
        _categoryNames = new List<string>
        {
            ChestCategoryName,
            ShouldersCategoryName,
            TricepsCategoryName,
            BicepsCategoryName,
            BackCategoryName,
            LegsCategoryName,
            AbsCategoryName,
            CardioCategoryName
        };

        _exercises = new List<ExerciseRecord>
        {
            FlatBenchPressExercise,
            FlyExercise,
            PushUpsExercise,
            PullUpExercise,
            BarbelRowExercise,
            SeatedCableRowExercise,
            OverheadPressExercise,
            DumbbellLateralRaiseExercise,
            DumbbellFrontRaiseExercise,
            FrenchPressExercise,
            DipsExercise,
            RopePushDownExercise,
            DumbbellCurlExercise,
            BarbellCurlExercise,
            CableCurlExercise,
            SquatExercise,
            DeadliftExercise,
            LegPressExercise,
            SitUpExercise,
            CableCrunchExercise,
            HangingKneeRaiseExercise,
            RunningOutdoorExercise,
            RunningThreadmillExercise,
            CyclingExercise,
        };
    }

    public static async Task CategoriesSeedAsync(EFContext context, CancellationToken cancellationToken)
    {
        var existingCategories = await context.ExerciseCategories.Where(c => _categoryNames.Contains(c.Name))
            .ToListAsync(cancellationToken);

        if (existingCategories.Count == _categoryNames.Count()) return;

        var missingCategories = _categoryNames
            .Where(c => !existingCategories.Any(ec => ec.Name.Equals(c)))
            .Select(ExerciseCategory.Create).ToList();

        if (missingCategories.Count == 0) return;

        await context.AddRangeAsync(missingCategories, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
    }

    public static async Task ExerciseSeedAsync(EFContext context, CancellationToken cancellationToken)
    {
        var groupedExercises = _exercises.GroupBy((e) => e.CategoryName);
        var categories = await context.ExerciseCategories.Include(ec => ec.Exercises).ToListAsync(cancellationToken);
        var parameters = await context.SetParameterNames.ToListAsync(cancellationToken);

        var exercisesToAdd = GetMissingExercises(groupedExercises, categories, parameters);

        await context.Exercises.AddRangeAsync(exercisesToAdd, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
    }

    private static IEnumerable<Exercise> GetMissingExercises(IEnumerable<IGrouping<string, ExerciseRecord>> groupedExercises, List<ExerciseCategory> categories, List<SetParameterName> parameters)
    {
        var missingExercises = new List<Exercise>();
        
        foreach (var exercises in groupedExercises)
        {
            var category = categories.FirstOrDefault(c => c.Name == exercises.Key);

            if (category == null) continue;
            foreach (var exerciseData in exercises)
            {
                if (category.Exercises.Any(e => e.Name == exerciseData.Name)) 
                    continue;

                var exercise = Exercise.Create(exerciseData.Name, category.Id);
                var exerciseParameters = parameters.Where(e => exerciseData.ParameterNames.Contains(e.Name));

                exercise.AddSetParameterNameRange(exerciseParameters);
                missingExercises.Add(exercise);
            }
        }
        
        return missingExercises;
    }

    private record ExerciseRecord(string Name, string CategoryName, IEnumerable<string> ParameterNames);
}