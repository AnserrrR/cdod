using cdod.Schema.InputTypes;
using cdod.Services;
using cdods.s;

namespace cdod.Schema.Mutations
{
    [ExtendObjectType(typeof(Mutation))]
    public class MutationSchool
    {
        [UseDbContext(typeof(CdodDbContext))]
        public async Task<School> CreateSchool(SchoolInput schoolForm, [ScopedService] CdodDbContext dbContext)
        {
            School school = new School()
            {
                Name = schoolForm.Name,
                District = schoolForm.District,
            };
            dbContext.Schools.Add(school);
            await dbContext.SaveChangesAsync();
            return school;
        }

        [UseDbContext(typeof(CdodDbContext))]
        public async Task<School> UpdateSchool(int id, SchoolInput schoolForm, [ScopedService] CdodDbContext dbContext)
        {
            School school = new School()
            {
                Id = id,
                Name = schoolForm.Name,
                District = schoolForm.District,
            };
            dbContext.Schools.Update(school);
            await dbContext.SaveChangesAsync();
            return school;
        }

        [UseDbContext(typeof(CdodDbContext))]
        public async Task<bool> DeleteSchool(int schoolId, [ScopedService] CdodDbContext dbContext)
        {
            School school = new School() { Id = schoolId };
            dbContext.Schools.Remove(school);
            return await dbContext.SaveChangesAsync() > 0;
        }
    }
}
