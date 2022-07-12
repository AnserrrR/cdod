using cdod.Models;
using Microsoft.EntityFrameworkCore;

namespace cdod.Services.DataLoaders
{
    public class TeacherDataLoader : BatchDataLoader<int, Teacher>
    {
        private readonly IDbContextFactory<CdodDbContext> _cdodContextFactory;

        public TeacherDataLoader(
            IDbContextFactory<CdodDbContext> contextFactory,
            IBatchScheduler batchScheduler,
            DataLoaderOptions options = null)
            : base(batchScheduler, options)
        {
            _cdodContextFactory = contextFactory;
        }

        protected override async Task<IReadOnlyDictionary<int, Teacher>> LoadBatchAsync(IReadOnlyList<int> keys, CancellationToken cancellationToken)
        {
            using var context = _cdodContextFactory.CreateDbContext();

            IEnumerable<Teacher> teachers = await context.Teachers
                .Where(i => keys.Contains(i.UserId))
                .ToListAsync();

            return teachers.ToDictionary(i => i.UserId);
        }
    }
}
