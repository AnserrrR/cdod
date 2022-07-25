using cdod.Schema.InputTypes;
using cdod.Services;
using cdod.Models;
using cdod.Schema.OutputTypes;

namespace cdod.Schema.Mutations
{
    [ExtendObjectType(typeof(Mutation))]
    public class MutationSchool
    {
        [UseDbContext(typeof(CdodDbContext))]
        public async Task<SchoolType> CreateSchool(SchoolCreateInput school, [ScopedService] CdodDbContext dbContext)
        {
            School _school = new School()
            {
                Name = school.Name,
                District = school.District,
            };
            dbContext.Schools.Add(_school);
            await dbContext.SaveChangesAsync();
            SchoolType schoolOutput = new SchoolType()
            {
                Name = _school.Name,
                District = _school.District,
            };
            return schoolOutput;
        }

        [UseDbContext(typeof(CdodDbContext))]
        public async Task<bool> SchoolUpdateMany(List<SchoolUpdateInput> schools, [ScopedService] CdodDbContext dbContext)
        {
            List<int> errorSchoolIds = new List<int>();
            List<School> schoolUpdated = new List<School>();
            foreach (var school in schools)
            {
                School? _school = dbContext.Schools.FirstOrDefault(s => s.Id == school.Id);
                if (_school == null) { errorSchoolIds.Add(school.Id); continue; };

                _school.Name = school.Name ?? _school.Name;
                _school.District = school.District ?? _school.District;
                schoolUpdated.Add(_school);
            }

            dbContext.Schools.UpdateRange(schoolUpdated);

            return await dbContext.SaveChangesAsync() > 0;
        }

        [UseDbContext(typeof(CdodDbContext))]
        public async Task<bool> SchoolDeleteMany(List<int> schoolIds, [ScopedService] CdodDbContext dbContext)
        {
            dbContext.Schools.RemoveRange(schoolIds.Select(s =>
            {
                School? school = dbContext.Schools.FirstOrDefault(sid => sid.Id == s);
                if (school == null) throw new Exception($"Вы указали для удаления несущесствующего ученика ID:{s}");
                return school;
            }));
            return await dbContext.SaveChangesAsync() > 0;
        }
    }
}
