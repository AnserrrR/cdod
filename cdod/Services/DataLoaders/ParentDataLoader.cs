using cdod.Models;
using Microsoft.EntityFrameworkCore;

namespace cdod.Services.DataLoaders
{
    public class ParentDataLoader : BatchDataLoader<int, Parent>
    {
        private readonly IDbContextFactory<CdodDbContext> _cdodContextFactory;

        public ParentDataLoader(
            IDbContextFactory<CdodDbContext> contextFactory,
            IBatchScheduler batchScheduler,
            DataLoaderOptions options = null)
            : base(batchScheduler, options)
        {
            _cdodContextFactory = contextFactory;
        }

        protected override async Task<IReadOnlyDictionary<int, Parent>> LoadBatchAsync(IReadOnlyList<int> keys, CancellationToken cancellationToken)
        {
            using var context = _cdodContextFactory.CreateDbContext();

            IEnumerable<Parent> parents = await context.Parents
                .Where(i => keys.Contains(i.UserId))
                .ToListAsync();

            return parents.ToDictionary(i => i.UserId);
        }
    }
}
