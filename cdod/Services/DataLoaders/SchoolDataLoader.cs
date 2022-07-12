using cdod.Models;
using cdod.Services.Repositories;
using Microsoft.EntityFrameworkCore;

namespace cdod.Services.DataLoaders
{
    public class SchoolDataLoader : BatchDataLoader<int, School>
    {
        private readonly IDbContextFactory<CdodDbContext> _cdodContextFactory;

        public SchoolDataLoader(
            IDbContextFactory<CdodDbContext> contextFactory,
            IBatchScheduler batchScheduler,
            DataLoaderOptions options = null)
            : base(batchScheduler, options)
        {
            _cdodContextFactory = contextFactory;
        }

        protected override async Task<IReadOnlyDictionary<int, School>> LoadBatchAsync(IReadOnlyList<int> keys, CancellationToken cancellationToken)
        {
            using var context = _cdodContextFactory.CreateDbContext();

            IEnumerable<School> schools = await context.Schools
                .Where(i => keys.Contains(i.Id))
                .ToListAsync();

            return schools.ToDictionary(i => i.Id);
        }
    }
}
