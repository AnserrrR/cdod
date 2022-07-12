using cdod.Models;
using Microsoft.EntityFrameworkCore;

namespace cdod.Services.DataLoaders
{
    public class StcDataLoader : GroupedDataLoader<int, StudentToCourse>
    {
        private readonly IDbContextFactory<CdodDbContext> _cdodContextFactory;

        public StcDataLoader(
            IDbContextFactory<CdodDbContext> contextFactory,
            IBatchScheduler batchScheduler,
            DataLoaderOptions options = null)
            : base(batchScheduler, options)
        {
            _cdodContextFactory = contextFactory;
        }

        protected override async Task<ILookup<int, StudentToCourse>> LoadGroupedBatchAsync(IReadOnlyList<int> keys, CancellationToken cancellationToken)
        {
            using var context = _cdodContextFactory.CreateDbContext();

            IEnumerable<StudentToCourse> STCs = await context.StudentToCourses
                .Where(stc => keys.Contains(stc.StudentId))
                .ToListAsync();
            return STCs.ToLookup(q => q.StudentId);
        }
    }
}
