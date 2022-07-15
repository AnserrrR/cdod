using cdod.Models;
using Microsoft.EntityFrameworkCore;

namespace cdod.Services.DataLoaders
{
    public class LessonDataLoader : BatchDataLoader<int, Lesson>
    {
        private readonly IDbContextFactory<CdodDbContext> _cdodContextFactory;

        public LessonDataLoader(
            IDbContextFactory<CdodDbContext> contextFactory,
            IBatchScheduler batchScheduler,
            DataLoaderOptions options = null)
            : base(batchScheduler, options)
        {
            _cdodContextFactory = contextFactory;
        }

        protected override async Task<IReadOnlyDictionary<int, Lesson>> LoadBatchAsync(IReadOnlyList<int> keys, CancellationToken cancellationToken)
        {
            using var context = _cdodContextFactory.CreateDbContext();

            IEnumerable<Lesson> lessons = await context.Lessons
                .Where(i => keys.Contains(i.Id))
                .ToListAsync();

            return lessons.ToDictionary(i => i.Id);
        }
    }
}
