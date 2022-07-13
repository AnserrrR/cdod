using cdod.Models;
using cdod.Services.HelperTypes;
using Microsoft.EntityFrameworkCore;

namespace cdod.Services.DataLoaders
{
    public class StudentsCountDataLoader : BatchDataLoader<int, StudentCountInGroups>
    {
        private readonly IDbContextFactory<CdodDbContext> _cdodContextFactory;

        public StudentsCountDataLoader(
            IDbContextFactory<CdodDbContext> contextFactory,
            IBatchScheduler batchScheduler,
            DataLoaderOptions options = null)
            : base(batchScheduler, options)
        {
            _cdodContextFactory = contextFactory;
        }

        protected override async Task<IReadOnlyDictionary<int, StudentCountInGroups>> LoadBatchAsync(
            IReadOnlyList<int> keys, CancellationToken cancellationToken)
        {
            using var context = _cdodContextFactory.CreateDbContext();

            IEnumerable<StudentCountInGroups> studentCount = await context.Groups
                .Where(g => keys.Contains(g.Id))
                .Select(g => new StudentCountInGroups()
                {
                    StudentsCount = g.StudentsToGroups.Count(),
                    GroupId = g.Id
                })
                .ToListAsync();

            return studentCount.ToDictionary(i => i.GroupId);
        }
    }
}
