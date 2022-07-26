using cdod.Models;
using cdod.Services.HelperTypes;
using Microsoft.EntityFrameworkCore;

namespace cdod.Services.DataLoaders
{
    public class GroupByStudentIdCourseIdDataLoader : BatchDataLoader<(int, int, int), StudentCourseGroup>
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

        protected override async Task<IReadOnlyDictionary<(int, int, int), StudentCourseGroup>> LoadBatchAsync(
            IReadOnlyList<(int, int, int)> keys, CancellationToken cancellationToken)
        {
            using var context = _cdodContextFactory.CreateDbContext();

            var courseIds = keys.Select(k => k.Item1);
            var studentIds = keys.Select(k => k.Item2);
            var attempts = keys.Select(k => k.Item3);

            var rawSelection = from g in context.Groups
                join stc in context.StudentToCourses on g.Id equals stc.GroupId
                where courseIds.Contains(stc.CourseId) && studentIds.Contains(stc.StudentId) && attempts.Contains(stc.Attempt)
                select new StudentCourseGroup()
                {
                    StudentId = stc.StudentId,
                    CourseId = stc.CourseId,
                    GroupId = stc.GroupId,
                    Attempt = stc.Attempt,
                    Group = g
                };


            var scgs = from scg in await rawSelection.ToListAsync()
                join key in keys on new { scg.CourseId, scg.StudentId, scg.Attempt }
                    equals new { CourseId = key.Item1, StudentId = key.Item2, Attempt = key.Item3 }
                select scg;


            return scgs.ToDictionary(i => ( i.CourseId, i.StudentId, i.Attempt ));

        }
    }
}
