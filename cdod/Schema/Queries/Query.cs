
using cdod.Services;
using cdodDTOs.DTOs;
using Microsoft.EntityFrameworkCore;

namespace cdod.Schema.Queries
{
    public class Query
    {
        //StudentsQueries
        [UseDbContext(typeof(CdodDbContext))]
        [UsePaging(IncludeTotalCount = true, DefaultPageSize = 10)]
        [UseProjection]
        [UseFiltering]
        [UseSorting]
        public IQueryable<StudentDTO> GetStudents([ScopedService] CdodDbContext cdodContext) => cdodContext.Students;

        [UseDbContext(typeof(CdodDbContext))]
        public async Task<StudentDTO> GetStudentByIdAsync(int id, [ScopedService] CdodDbContext cdodContext) => await cdodContext.Students.FirstOrDefaultAsync(s => s.Id == id);

        //CourseQueries
        [UseDbContext(typeof(CdodDbContext))]
        [UsePaging(IncludeTotalCount = true, DefaultPageSize = 10)]
        [UseProjection]
        [UseFiltering]
        [UseSorting]
        public IQueryable<CourseDTO> GetCourses([ScopedService] CdodDbContext cdodContext) => cdodContext.Courses;

        [UseDbContext(typeof(CdodDbContext))]
        public async Task<CourseDTO> GetCourseByIdAsync(int id, [ScopedService] CdodDbContext cdodContext) => await cdodContext.Courses.FirstOrDefaultAsync(s => s.Id == id);

        //GroupQueries
        [UseDbContext(typeof(CdodDbContext))]
        [UsePaging(IncludeTotalCount = true, DefaultPageSize = 10)]
        [UseProjection]
        [UseFiltering]
        [UseSorting]
        public IQueryable<GroupDTO> GetGroups([ScopedService] CdodDbContext cdodContext) => cdodContext.Groups;

        [UseDbContext(typeof(CdodDbContext))]
        public async Task<GroupDTO> GetGroupByIdAsync(int id, [ScopedService] CdodDbContext cdodContext) => await cdodContext.Groups.FirstOrDefaultAsync(s => s.Id == id);
    }
}
