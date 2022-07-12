using cdod.Models;
using Microsoft.EntityFrameworkCore;

namespace cdod.Services.DataLoaders
{
    public class CourseDataLoader : BatchDataLoader<int, Course>
    {
        private readonly IDbContextFactory<CdodDbContext> _cdodContextFactory;

        public CourseDataLoader(
            IDbContextFactory<CdodDbContext> contextFactory,
            IBatchScheduler batchScheduler,
            DataLoaderOptions options = null)
            : base(batchScheduler, options)
        {
            _cdodContextFactory = contextFactory;
        }

        protected override async Task<IReadOnlyDictionary<int, Course>> LoadBatchAsync(IReadOnlyList<int> keys, CancellationToken cancellationToken)
        {
            using var context = _cdodContextFactory.CreateDbContext();

            IEnumerable<Course> courses = await context.Courses
                .Where(i => keys.Contains(i.Id))
                .ToListAsync();

            return courses.ToDictionary(i => i.Id);
        }
    }
}
