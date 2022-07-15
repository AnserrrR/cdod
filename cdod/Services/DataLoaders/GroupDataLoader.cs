using cdod.Models;
using Microsoft.EntityFrameworkCore;

namespace cdod.Services.DataLoaders
{
    public class GroupDataLoader : BatchDataLoader<int, Group>
    {
        private readonly IDbContextFactory<CdodDbContext> _cdodContextFactory;

        public GroupDataLoader(
            IDbContextFactory<CdodDbContext> contextFactory,
            IBatchScheduler batchScheduler,
            DataLoaderOptions options = null)
            : base(batchScheduler, options)
        {
            _cdodContextFactory = contextFactory;
        }

        protected override async Task<IReadOnlyDictionary<int, Group>> LoadBatchAsync(IReadOnlyList<int> keys, CancellationToken cancellationToken)
        {
            using var context = _cdodContextFactory.CreateDbContext();

            IEnumerable<Group> groups = await context.Groups
                .Where(i => keys.Contains(i.Id))
                .ToListAsync();

            return groups.ToDictionary(i => i.Id);
        }
    }
}
