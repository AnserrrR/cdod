using cdod.Schema.InputTypes;
using cdod.Services;
using cdod.Models;
using cdod.Schema.OutputTypes;
using HotChocolate.AspNetCore.Authorization;

namespace cdod.Schema.Mutations
{
    [ExtendObjectType(typeof(Mutation))]
    public class MutationWageRate
    {
        [Authorize(Roles = new[] { "admin" })]
        [UseDbContext(typeof(CdodDbContext))]
        public async Task<WageRateType> WageRateCreate(WageRateCreateInput wageRate, [ScopedService] CdodDbContext dbContext)
        {
            WageRate wageRateToCreate = new WageRate()
            {
                Rate = wageRate.Rate
            };
            dbContext.WageRates.Add(wageRateToCreate);
            await dbContext.SaveChangesAsync();
            WageRateType wageRateOutput = new WageRateType()
            {
                Id = wageRateToCreate.Id,
                Rate = wageRateToCreate.Rate
            };
            return wageRateOutput;
        }

        [Authorize(Roles = new[] { "admin" })]
        [UseDbContext(typeof(CdodDbContext))]
        public async Task<bool> WageRateUpdateMany(List<WageRateUpdateInput> wageRates, [ScopedService] CdodDbContext dbContext)
        {
            List<int> errorWageRateIds = new List<int>();
            List<WageRate> wageRateUpdated = new List<WageRate>();
            foreach (var wageRate in wageRates)
            {
                WageRate? wageRateToUpdate = dbContext.WageRates.FirstOrDefault(wr => wr.Id == wageRate.Id);
                if (wageRateToUpdate == null) { errorWageRateIds.Add(wageRate.Id); continue; };

                wageRateToUpdate.Rate = wageRate.Rate;
                wageRateUpdated.Add(wageRateToUpdate);
            }

            dbContext.WageRates.UpdateRange(wageRateUpdated);

            return await dbContext.SaveChangesAsync() > 0;
        }

        [Authorize(Roles = new[] { "admin" })]
        [UseDbContext(typeof(CdodDbContext))]
        public async Task<bool> WageRateDeleteMany(List<int> wageRateIds, [ScopedService] CdodDbContext dbContext)
        {
            List<int> errorNotWageRateIds = new List<int>();
            foreach (int id in wageRateIds)
            {
                WageRate? wageRate = dbContext.WageRates.FirstOrDefault(wg => wg.Id == id);
                if (wageRate is null) { errorNotWageRateIds.Add(id); continue; }
                dbContext.WageRates.Remove(wageRate);
            }
            if (errorNotWageRateIds.Count() > 0) throw new GraphQLException($"Невозможно удалить следующие зарплатные ставки:\n" +
    $": {string.Join(" ", errorNotWageRateIds)}");
            return await dbContext.SaveChangesAsync() > 0;
        }
    }
}
