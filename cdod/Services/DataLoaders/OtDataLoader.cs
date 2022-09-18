using cdod.Models;
using Microsoft.EntityFrameworkCore;

namespace cdod.Services.DataLoaders
{
    public class OtDataLoader<T> : BatchDataLoader<int, T> where T : class
    {
        private readonly IDbContextFactory<CdodDbContext> _cdodContextFactory;

        public OtDataLoader(
            IDbContextFactory<CdodDbContext> contextFactory,
            IBatchScheduler batchScheduler,
            DataLoaderOptions options = null)
            : base(batchScheduler, options)
        {
            _cdodContextFactory = contextFactory;
        }

        protected override async Task<IReadOnlyDictionary<int, T>> LoadBatchAsync(IReadOnlyList<int> keys, CancellationToken cancellationToken)
        {
            using var context = _cdodContextFactory.CreateDbContext();

            IEnumerable<Course> courses = await context.Set<T>()
                .Where(i => keys.Contains(i.Id))
                .ToListAsync();

            return courses.ToDictionary(i => i.Id);
        }
    }
}
