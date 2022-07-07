using cdod.Schema.InputTypes;
using cdod.Services;
using cdodDTOs.DTOs;

namespace cdod.Schema.Mutations
{
    [ExtendObjectType(typeof(Mutation))]
    public class MutationSchool
    {
        [UseDbContext(typeof(CdodDbContext))]
        public async Task<SchoolDTO> CreateSchool(SchoolInput schoolForm, [ScopedService] CdodDbContext dbContext)
        {
            SchoolDTO school = new SchoolDTO()
            {
                Name = schoolForm.Name,
                District = schoolForm.District,
            };
            dbContext.Schools.Add(school);
            await dbContext.SaveChangesAsync();
            return school;
        }

        [UseDbContext(typeof(CdodDbContext))]
        public async Task<SchoolDTO> UpdateSchool(int id, SchoolInput schoolForm, [ScopedService] CdodDbContext dbContext)
        {
            SchoolDTO school = new SchoolDTO()
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
            SchoolDTO school = new SchoolDTO() { Id = schoolId };
            dbContext.Schools.Remove(school);
            return await dbContext.SaveChangesAsync() > 0;
        }
    }
}
