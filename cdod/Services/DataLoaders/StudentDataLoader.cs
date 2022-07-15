using cdod.Models;
using Microsoft.EntityFrameworkCore;

namespace cdod.Services.DataLoaders
{
    public class StudentDataLoader : BatchDataLoader<int, Student>
    {
        private readonly IDbContextFactory<CdodDbContext> _cdodContextFactory;

        public StudentDataLoader(
            IDbContextFactory<CdodDbContext> contextFactory,
            IBatchScheduler batchScheduler,
            DataLoaderOptions options = null)
            : base(batchScheduler, options)
        {
            _cdodContextFactory = contextFactory;
        }

        protected override async Task<IReadOnlyDictionary<int, Student>> LoadBatchAsync(IReadOnlyList<int> keys, CancellationToken cancellationToken)
        {
            using var context = _cdodContextFactory.CreateDbContext();

            IEnumerable<Student> students = await context.Students
                .Where(i => keys.Contains(i.Id))
                .ToListAsync();

            return students.ToDictionary(i => i.Id);
        }
    }
}
