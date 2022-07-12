
using System.Runtime.InteropServices.ComTypes;
using cdod.Services;
using cdod.Models;
using cdod.Schema.OutputTypes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace cdod.Schema.Queries
{
    public class Query
    {
        /* Main queries */

        //Students queries

        [UseDbContext(typeof(CdodDbContext))]
        [UseProjection]
        public IQueryable<StudentType> GetStudents(int? courseId, int? groupId, int? parentId, 
            [ScopedService] CdodDbContext ctx)
        {
            if (courseId is not null)
                return from s in ctx.Students
                    join stc in ctx.StudentToCourses on s.Id equals stc.StudentId
                    where stc.CourseId == courseId
                    select new StudentType()
                    {
                        Id = s.Id,
                        FirstName = s.FirstName,
                        LastName = s.LastName,
                        Patronymic = s.Patronymic,
                        BirthDate = s.BirthDate,
                        Description = s.Description,
                        ParentId = s.ParentId,
                        SchoolId = s.SchoolId
                    };

            if (groupId is not null)
                return ctx.Students.Where(s => s.Groups.Any(g => g.Id == groupId))
                    .Select(s => new StudentType()
            {
                Id = s.Id,
                FirstName = s.FirstName,
                LastName = s.LastName,
                Patronymic = s.Patronymic,
                BirthDate = s.BirthDate,
                Description = s.Description,
                ParentId = s.ParentId,
                SchoolId = s.SchoolId
            });

            if (parentId is not null)
                return ctx.Students.Where(s => s.ParentId == parentId)
                    .Select(s => new StudentType()
                    {
                        Id = s.Id,
                        FirstName = s.FirstName,
                        LastName = s.LastName,
                        Patronymic = s.Patronymic,
                        BirthDate = s.BirthDate,
                        Description = s.Description,
                        ParentId = s.ParentId,
                        SchoolId = s.SchoolId
                    });

            return ctx.Students.Select(s => new StudentType()
            {
                Id = s.Id,
                FirstName = s.FirstName,
                LastName = s.LastName,
                Patronymic = s.Patronymic,
                BirthDate = s.BirthDate,
                Description = s.Description,
                ParentId = s.ParentId,
                SchoolId = s.SchoolId
            });
        }

        [UseDbContext(typeof(CdodDbContext))]
        [UseProjection]
        public async Task<StudentType> GetStudentAsync(int id, [ScopedService] CdodDbContext cdodContext)
        {
            var s = await cdodContext.Students.FindAsync(id);
            if (s is null) throw new GraphQLException($"{typeof(Student)} not found!");
            return new StudentType()
            {
                Id = s.Id,
                FirstName = s.FirstName,
                LastName = s.LastName,
                Patronymic = s.Patronymic,
                BirthDate = s.BirthDate,
                Description = s.Description,
                ParentId = s.ParentId,
                SchoolId = s.SchoolId
            };
        }

        //Group queries
        [UseDbContext(typeof(CdodDbContext))]
        [UseProjection]
        public IQueryable<GroupType> GetGroups(int? courseId, [ScopedService] CdodDbContext ctx)
        {
            if (courseId is not null)
                return ctx.Groups.Where(g => g.CourseId == courseId)
                    .Select(g => new GroupType()
                    {
                        Id = g.Id,
                        Name = g.Name,
                        StartDate = g.StartDate,
                        CourseId = g.CourseId,
                        TeacherId = g.TeacherId
                    });

            return ctx.Groups.Select(g => new GroupType()
                {
                    Id = g.Id,
                    Name = g.Name,
                    StartDate = g.StartDate,
                    CourseId = g.CourseId,
                    TeacherId = g.TeacherId
                });
        }

        [UseDbContext(typeof(CdodDbContext))]
        [UseProjection]
        public async Task<GroupType> GetGroupAsync(int id, [ScopedService] CdodDbContext cdodContext)
        {
            var g = await cdodContext.Groups.FindAsync(id);
            if (g is null) throw new GraphQLException($"{typeof(Group)} not found!");
            return new GroupType()
            {
                Id = g.Id,
                Name = g.Name,
                StartDate = g.StartDate,
                CourseId = g.CourseId,
                TeacherId = g.TeacherId
            };
        }

        //Course queries
        [UseDbContext(typeof(CdodDbContext))]
        [UseProjection]
        public IQueryable<CourseType> GetCourse([ScopedService] CdodDbContext ctx)
        {
            return ctx.Courses.Select(c => new CourseType()
            {
                Id = c.Id,
                Name = c.Name,
                ProgramFileUrl = c.ProgramFileUrl,
                CoursePrice = c.CoursePrice,
                EquipmentPriceWithRobot = c.EquipmentPriceWithRobot,
                EquipmentPriceWithoutRobot = c.EquipmentPriceWithoutRobot,
                DurationInMonths = c.DurationInMonths
            });
        }

        [UseDbContext(typeof(CdodDbContext))]
        [UseProjection]
        public async Task<CourseType> GetCourseAsync(int id, [ScopedService] CdodDbContext cdodContext)
        {
            var c = await cdodContext.Courses.FindAsync(id);
            if (c is null) throw new GraphQLException($"{typeof(Course)} not found!");
            return new CourseType()
            {
                Id = c.Id,
                Name = c.Name,
                ProgramFileUrl = c.ProgramFileUrl,
                CoursePrice = c.CoursePrice,
                EquipmentPriceWithRobot = c.EquipmentPriceWithRobot,
                EquipmentPriceWithoutRobot = c.EquipmentPriceWithoutRobot,
                DurationInMonths = c.DurationInMonths
            };
        }

        //Teacher queries
        [UseDbContext(typeof(CdodDbContext))]
        public IQueryable<TeacherType> GetTeachers(int? lessonId, [ScopedService] CdodDbContext ctx)
        {
            if (lessonId is not null)
                return from t in ctx.Teachers
                    join ttl in ctx.TeacherToLessons on t.UserId equals ttl.TeacherId
                    where ttl.LessonId == lessonId
                    select new TeacherType(t.User)
                    {
                        Id = t.UserId,
                        PostId = t.PostId,
                        WorkPlace = t.WorkPlace
                    };
            return ctx.Teachers.Select(t => new TeacherType(t.User)
            {
                Id = t.UserId,
                PostId = t.PostId,
                WorkPlace = t.WorkPlace
            });
        }

        [UseDbContext(typeof(CdodDbContext))]
        public async Task<TeacherType> GetTeacherAsync(int? id, int? groupId, [ScopedService] CdodDbContext cdodContext)
        {
            if (id is not null)
            {
                var query = from t in cdodContext.Teachers
                    where t.UserId == id
                    select new TeacherType(t.User)
                    {
                        Id = t.UserId,
                        PostId = t.PostId,
                        WorkPlace = t.WorkPlace
                    };

                var teacher = await query.FirstOrDefaultAsync();

                if (teacher is null) throw new GraphQLException($"{typeof(Teacher)} not found!");
                return teacher;
            }
            else if (groupId is not null)
            {
                var query = from t in cdodContext.Teachers 
                            join g in cdodContext.Groups on t.UserId equals g.TeacherId
                            where g.Id == groupId
                            select new TeacherType(t.User)
                            {
                                Id = t.UserId,
                                PostId = t.PostId,
                                WorkPlace = t.WorkPlace
                            };

                var teacher = await query.FirstOrDefaultAsync();

                if (teacher is null) throw new GraphQLException($"{typeof(Teacher)} not found!");
                return teacher;
            }

            throw new GraphQLException("Not enough params");
        }

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
