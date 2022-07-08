
using cdod.Services;
using cdods.s;
using Microsoft.EntityFrameworkCore;

namespace cdod.Schema.Queries
{
    public class Query
    {
        /* Main queries */

        //Students queries
        [UseDbContext(typeof(CdodDbContext))]
        [UsePaging(IncludeTotalCount = true, DefaultPageSize = 10)]
        [UseProjection]
        [UseFiltering]
        [UseSorting]
        public IQueryable<Student> GetStudents([ScopedService] CdodDbContext cdodContext) => cdodContext.Students;

        [UseDbContext(typeof(CdodDbContext))]
        public async Task<Student> GetStudentByIdAsync(int id, [ScopedService] CdodDbContext cdodContext) => await cdodContext.Students.FirstOrDefaultAsync(e => e.Id == id);

        //Course queries
        [UseDbContext(typeof(CdodDbContext))]
        [UsePaging(IncludeTotalCount = true, DefaultPageSize = 10)]
        [UseProjection]
        [UseFiltering]
        [UseSorting]
        public IQueryable<Course> GetCourses([ScopedService] CdodDbContext cdodContext) => cdodContext.Courses;

        [UseDbContext(typeof(CdodDbContext))]
        public async Task<Course> GetCourseByIdAsync(int id, [ScopedService] CdodDbContext cdodContext) => await cdodContext.Courses.FirstOrDefaultAsync(e => e.Id == id);

        //Group queries
        [UseDbContext(typeof(CdodDbContext))]
        [UsePaging(IncludeTotalCount = true, DefaultPageSize = 10)]
        [UseProjection]
        [UseFiltering]
        [UseSorting]
        public IQueryable<Group> GetGroups([ScopedService] CdodDbContext cdodContext) => cdodContext.Groups;

        [UseDbContext(typeof(CdodDbContext))]
        public async Task<Group> GetGroupByIdAsync(int id, [ScopedService] CdodDbContext cdodContext) => await cdodContext.Groups.FirstOrDefaultAsync(e => e.Id == id);


        /* Other queries */

        //Announcement queries
        [UseDbContext(typeof(CdodDbContext))]
        [UsePaging(IncludeTotalCount = true, DefaultPageSize = 10)]
        [UseProjection]
        [UseFiltering]
        [UseSorting]
        public IQueryable<Announcement> GetAnnouncements([ScopedService] CdodDbContext cdodContext) => cdodContext.Announcements;

        [UseDbContext(typeof(CdodDbContext))]
        public async Task<Announcement> GetAnnouncementByIdAsync(int id, [ScopedService] CdodDbContext cdodContext) => await cdodContext.Announcements.FirstOrDefaultAsync(e => e.Id == id);

        //ContractState queries
        [UseDbContext(typeof(CdodDbContext))]
        [UsePaging(IncludeTotalCount = true, DefaultPageSize = 10)]
        [UseProjection]
        [UseFiltering]
        [UseSorting]
        public IQueryable<ContractState> GetContactStates([ScopedService] CdodDbContext cdodContext) => cdodContext.ContractStates;

        [UseDbContext(typeof(CdodDbContext))]
        public async Task<ContractState> GetContactStateByIdAsync(int id, [ScopedService] CdodDbContext cdodContext) => await cdodContext.ContractStates.FirstOrDefaultAsync(e => e.Id == id);
        
        //Lesson queries
        [UseDbContext(typeof(CdodDbContext))]
        [UsePaging(IncludeTotalCount = true, DefaultPageSize = 10)]
        [UseProjection]
        [UseFiltering]
        [UseSorting]
        public IQueryable<Lesson> GetLessons([ScopedService] CdodDbContext cdodContext) => cdodContext.Lessons;

        [UseDbContext(typeof(CdodDbContext))]
        public async Task<Lesson> GetLessonByIdAsync(int id, [ScopedService] CdodDbContext cdodContext) => await cdodContext.Lessons.FirstOrDefaultAsync(e => e.Id == id);

        //Parent queries
        [UseDbContext(typeof(CdodDbContext))]
        [UsePaging(IncludeTotalCount = true, DefaultPageSize = 10)]
        [UseProjection]
        [UseFiltering]
        [UseSorting]
        public IQueryable<Parent> GetParents([ScopedService] CdodDbContext cdodContext) => cdodContext.Parents;

        [UseDbContext(typeof(CdodDbContext))]
        public async Task<Parent> GetParentByIdAsync(int id, [ScopedService] CdodDbContext cdodContext) => await cdodContext.Parents.FirstOrDefaultAsync(e => e.UserId == id);

        //PayNote queries
        [UseDbContext(typeof(CdodDbContext))]
        [UsePaging(IncludeTotalCount = true, DefaultPageSize = 10)]
        [UseProjection] 
        [UseFiltering]
        [UseSorting]
        public IQueryable<PayNote> GetPayNotes([ScopedService] CdodDbContext cdodContext) => cdodContext.PayNotes;

        [UseDbContext(typeof(CdodDbContext))]
        public async Task<PayNote> GetPayNoteByIdAsync(int id, [ScopedService] CdodDbContext cdodContext) => await cdodContext.PayNotes.FirstOrDefaultAsync(e => e.Id == id);

        //Post queries
        [UseDbContext(typeof(CdodDbContext))]
        [UsePaging(IncludeTotalCount = true, DefaultPageSize = 10)]
        [UseProjection]
        [UseFiltering]
        [UseSorting]
        public IQueryable<Post> GetPosts([ScopedService] CdodDbContext cdodContext) => cdodContext.Posts;

        [UseDbContext(typeof(CdodDbContext))]
        public async Task<Post> GetPostByIdAsync(int id, [ScopedService] CdodDbContext cdodContext) => await cdodContext.Posts.FirstOrDefaultAsync(e => e.Id == id);

        //School queries
        [UseDbContext(typeof(CdodDbContext))]
        [UsePaging(IncludeTotalCount = true, DefaultPageSize = 10)]
        [UseProjection]
        [UseFiltering]
        [UseSorting]
        public IQueryable<School> GetSchools([ScopedService] CdodDbContext cdodContext) => cdodContext.Schools;

        [UseDbContext(typeof(CdodDbContext))]
        public async Task<School> GetSchoolByIdAsync(int id, [ScopedService] CdodDbContext cdodContext) => await cdodContext.Schools.FirstOrDefaultAsync(e => e.Id == id);


        //Teacher queries
        [UseDbContext(typeof(CdodDbContext))]
        [UsePaging(IncludeTotalCount = true, DefaultPageSize = 10)]
        [UseProjection]
        [UseFiltering]
        [UseSorting]
        public IQueryable<Teacher> GetTeachers([ScopedService] CdodDbContext cdodContext) => cdodContext.Teachers;

        [UseDbContext(typeof(CdodDbContext))]
        public async Task<Teacher> GetTeacherByIdAsync(int id, [ScopedService] CdodDbContext cdodContext) => await cdodContext.Teachers.FirstOrDefaultAsync(e => e.UserId == id);

        //User queries
        [UseDbContext(typeof(CdodDbContext))]
        [UsePaging(IncludeTotalCount = true, DefaultPageSize = 10)]
        [UseProjection]
        [UseFiltering]
        [UseSorting]
        public IQueryable<User> GetUsers([ScopedService] CdodDbContext cdodContext) => cdodContext.Users;

        [UseDbContext(typeof(CdodDbContext))]
        public async Task<User> GetUserByIdAsync(int id, [ScopedService] CdodDbContext cdodContext) => await cdodContext.Users.FirstOrDefaultAsync(e => e.Id == id);

    }
}
