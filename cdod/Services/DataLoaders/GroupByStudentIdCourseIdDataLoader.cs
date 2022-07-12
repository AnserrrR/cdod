using cdod.Models;
using cdod.Services.HelperTypes;
using Microsoft.EntityFrameworkCore;

namespace cdod.Services.DataLoaders
{
    public class GroupByStudentIdCourseIdDataLoader : BatchDataLoader<(int, int), StudentCourseGroup>
    {
        private readonly IDbContextFactory<CdodDbContext> _cdodContextFactory;

        public GroupByStudentIdCourseIdDataLoader(
            IDbContextFactory<CdodDbContext> contextFactory,
            IBatchScheduler batchScheduler,
            DataLoaderOptions options = null)
            : base(batchScheduler, options)
        {
            _cdodContextFactory = contextFactory;
        }

        protected override async Task<IReadOnlyDictionary<(int, int), StudentCourseGroup>> LoadBatchAsync(
            IReadOnlyList<(int, int)> keys, CancellationToken cancellationToken)
        {
            using var context = _cdodContextFactory.CreateDbContext();

            var courseIds = keys.Select(k => k.Item1);
            var studentIds = keys.Select(k => k.Item2);

            var query = from g in context.Groups
                join c in context.Courses on g.CourseId equals c.Id
                join stc in context.StudentToCourses on c.Id equals stc.CourseId
                where courseIds.Contains(c.Id) && studentIds.Contains(stc.StudentId)
                select new StudentCourseGroup()
                {
                    StudentId = stc.StudentId,
                    CourseId = c.Id,
                    GroupId = g.Id,
                    Group = g
                };

            IEnumerable<StudentCourseGroup> scgs = await query.ToListAsync();

            return scgs.ToDictionary(i => ( i.CourseId, i.StudentId ));

        }
    }
}
