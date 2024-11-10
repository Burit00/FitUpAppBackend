using FitUpAppBackend.Core.SetParameterNames.Entities;
using FitUpAppBackend.Infrastructure.DAL.EF.Context;
using Microsoft.EntityFrameworkCore;

namespace FitUpAppBackend.Infrastructure.DAL.EF.Seeder.SetParameterNames;

public static class SetParameterNamesSeeder
{
    public static async Task SeedAsync(EFContext context, CancellationToken cancellationToken)
    {
        var setParameterNames = Core.SetParameterNames.Static.SetParameterNames.Parameters;
        var setParameterNamesFromDb = await context.SetParameterNames.ToListAsync(cancellationToken);
        var setParameterNamesToCreate = setParameterNames
            .Where(spn => setParameterNamesFromDb.All(spnDb => spn.ToLower() != spnDb.Name))
            .Select(SetParameterName.Create);

        await context.SetParameterNames.AddRangeAsync(setParameterNamesToCreate, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
    }
}