
using cdod.Services;
using cdods.s;
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
        public IQueryable<Student> GetStudents([ScopedService] CdodDbContext cdodContext) => cdodContext.Students;

        [UseDbContext(typeof(CdodDbContext))]
        public async Task<Student> GetStudentByIdAsync(int id, [ScopedService] CdodDbContext cdodContext) => await cdodContext.Students.FirstOrDefaultAsync(s => s.Id == id);

        //CourseQueries
        [UseDbContext(typeof(CdodDbContext))]
        [UsePaging(IncludeTotalCount = true, DefaultPageSize = 10)]
        [UseProjection]
        [UseFiltering]
        [UseSorting]
        public IQueryable<Course> GetCourses([ScopedService] CdodDbContext cdodContext) => cdodContext.Courses;

        [UseDbContext(typeof(CdodDbContext))]
        public async Task<Course> GetCourseByIdAsync(int id, [ScopedService] CdodDbContext cdodContext) => await cdodContext.Courses.FirstOrDefaultAsync(s => s.Id == id);

        //GroupQueries
        [UseDbContext(typeof(CdodDbContext))]
        [UsePaging(IncludeTotalCount = true, DefaultPageSize = 10)]
        [UseProjection]
        [UseFiltering]
        [UseSorting]
        public IQueryable<Group> GetGroups([ScopedService] CdodDbContext cdodContext) => cdodContext.Groups;

        [UseDbContext(typeof(CdodDbContext))]
        public async Task<Group> GetGroupByIdAsync(int id, [ScopedService] CdodDbContext cdodContext) => await cdodContext.Groups.FirstOrDefaultAsync(s => s.Id == id);
    }
}
