using cdod.Models;
using Microsoft.EntityFrameworkCore;

namespace cdod.Services.DataLoaders
{
    public class WageRateDataLoader : BatchDataLoader<int, WageRate>
    {
        private readonly IDbContextFactory<CdodDbContext> _cdodContextFactory;

        public WageRateDataLoader(
            IDbContextFactory<CdodDbContext> contextFactory,
            IBatchScheduler batchScheduler,
            DataLoaderOptions options = null)
            : base(batchScheduler, options)
        {
            _cdodContextFactory = contextFactory;
        }

        protected override async Task<IReadOnlyDictionary<int, WageRate>> LoadBatchAsync(IReadOnlyList<int> keys, CancellationToken cancellationToken)
        {
            using var context = _cdodContextFactory.CreateDbContext();

            IEnumerable<WageRate> wageRates = await context.WageRates
                .Where(i => keys.Contains(i.Id))
                .ToListAsync();

            return wageRates.ToDictionary(i => i.Id);
        }
    }
}
