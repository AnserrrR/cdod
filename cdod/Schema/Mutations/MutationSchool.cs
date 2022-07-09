using cdod.Schema.InputTypes;
using cdod.Services;
using cdod.Models;

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
                District = (District)schoolForm.District,
            };
            dbContext.Schools.Add(school);
            await dbContext.SaveChangesAsync();
            return school;
        }

        [UseDbContext(typeof(CdodDbContext))]
        public async Task<School> UpdateSchool(int id, SchoolInput schoolForm, [ScopedService] CdodDbContext dbContext)
        {
            School? school = dbContext.Schools.FirstOrDefault(s => s.Id == id);
            if (school == null) throw new Exception("Уазанной школы не существует");

            school.Name = schoolForm.Name ?? school.Name;
            school.District = schoolForm.District ?? school.District;

            dbContext.Schools.Update(school);
            await dbContext.SaveChangesAsync();
            return school;
        }

        [UseDbContext(typeof(CdodDbContext))]
        public async Task<bool> DeleteSchool(int schoolId, [ScopedService] CdodDbContext dbContext)
        {
            School? school = dbContext.Schools.FirstOrDefault(s => s.Id == schoolId);
            if (school == null) throw new Exception("Уазанной школы не существует");

            dbContext.Schools.Remove(school);
            return await dbContext.SaveChangesAsync() > 0;
        }
    }
}
