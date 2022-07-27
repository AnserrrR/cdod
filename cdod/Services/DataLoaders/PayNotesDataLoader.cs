using cdod.Models;
using cdod.Services.HelperTypes;
using Microsoft.EntityFrameworkCore;

namespace cdod.Services.DataLoaders
{
    public class PayNotesDataLoader : BatchDataLoader<(int, int, int, Appointment), PayNotesSum>
    {
        private readonly IDbContextFactory<CdodDbContext> _cdodContextFactory;

        public PayNotesDataLoader(
            IDbContextFactory<CdodDbContext> contextFactory,
            IBatchScheduler batchScheduler,
            DataLoaderOptions options = null)
            : base(batchScheduler, options)
        {
            _cdodContextFactory = contextFactory;
        }

        protected override async Task<IReadOnlyDictionary<(int, int, int, Appointment), PayNotesSum>> 
            LoadBatchAsync(IReadOnlyList<(int, int, int, Appointment)> keys, CancellationToken cancellationToken)
        {
            using var context = _cdodContextFactory.CreateDbContext();

            var courseIds = keys.Select(k => k.Item1);
            var studentIds = keys.Select(k => k.Item2);
            var attempts = keys.Select(k => k.Item3);
            var appointments = keys.Select(k => k.Item4);

            var query = from pn in context.PayNotes
                where courseIds.Contains(pn.CourseId) && studentIds.Contains(pn.StudentId)
                                                      && attempts.Contains(pn.Attempt)
                                                      && appointments.Contains(pn.Appointment)
                group pn by new {pn.CourseId, pn.StudentId, pn.Attempt, pn.Appointment} into res
                select new PayNotesSum()
                {
                    StudentId = res.Key.StudentId,
                    CourseId = res.Key.CourseId,
                    Attempt = res.Key.Attempt,
                    Appointment = res.Key.Appointment,
                    TotalSum = res.Sum(pn => pn.Sum)
                };

            IEnumerable<PayNotesSum> PNSs = from pns in await query.ToListAsync()
                join key in keys on new { pns.CourseId, pns.StudentId, pns.Attempt, pns.Appointment }
                    equals new { CourseId = key.Item1, StudentId = key.Item2, 
                        Attempt = key.Item3, Appointment = key.Item4 }
                select pns;

            return PNSs.ToDictionary(i => (i.CourseId, i.StudentId, i.Attempt, i.Appointment));
        }
    }
}
