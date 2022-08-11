using cdod.Schema.InputTypes;
using cdod.Services;
using cdod.Models;
using cdod.Schema.OutputTypes;
using HotChocolate.AspNetCore.Authorization;

namespace cdod.Schema.Mutations
{
    [ExtendObjectType(typeof(Mutation))]
    public class MutationSchool
    {
        [Authorize(Roles = new[] { "admin" })]
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

        [Authorize(Roles = new[] { "admin" })]
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

        [Authorize(Roles = new[] { "admin" })]
        [UseDbContext(typeof(CdodDbContext))]
        public async Task<bool> SchoolDeleteMany(List<int> schoolIds, [ScopedService] CdodDbContext dbContext)
        {
            List<int> errorNotschoolIds = new List<int>();
            foreach (int id in schoolIds)
            {
                School? school = dbContext.Schools.FirstOrDefault(s => s.Id == id);
                if (school is null) { errorNotschoolIds.Add(id); continue; }
                dbContext.Schools.Remove(school);
            }
            if (errorNotschoolIds.Count() > 0) throw new GraphQLException($"Невозможно удалить следующих пользователей:\n" +
    $"ID следующих пользователей не являющихся родителями: {string.Join(" ", errorNotschoolIds)}");
            return await dbContext.SaveChangesAsync() > 0;
        }
    }
}
