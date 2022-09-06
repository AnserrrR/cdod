
using System.Runtime.InteropServices.ComTypes;
using cdod.Services;
using cdod.Models;
using cdod.Schema.OutputTypes;
using HotChocolate.AspNetCore.Authorization;
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
        [UseFiltering]
        [UseSorting]
        [Authorize(Roles = new[] {"admin"})]
        public IQueryable<StudentType> GetStudents(int? courseId, int? groupId, int? parentId, int? schoolId,
            [ScopedService] CdodDbContext ctx)
        {
            if (courseId is not null)
            {
                if (!ctx.Courses.Any(c => c.Id == courseId))
                    throw new GraphQLException($"{nameof(Course)} not found!");

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
            }

            if (groupId is not null)
            {
                if (!ctx.Groups.Any(g => g.Id == groupId))
                    throw new GraphQLException($"{nameof(Group)} not found!");

                return ctx.Students.Where(s => s.StudentToCourses
                        .Any(stg => stg.GroupId == groupId))
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
            }

            if (parentId is not null)
            {
                if (!ctx.Parents.Any(p => p.UserId == parentId))
                    throw new GraphQLException($"{nameof(Parent)} not found!");

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
            }

            if (schoolId is not null)
            {
                if (!ctx.Schools.Any(sc => sc.Id == schoolId))
                    throw new GraphQLException($"{nameof(School)} not found!");

                return from sc in ctx.Schools
                    where sc.Id == schoolId
                    join s in ctx.Students on sc.Id equals s.SchoolId
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
            }

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
        [Authorize(Roles = new[] { "admin" })]
        public async Task<StudentType> GetStudentAsync(int id, [ScopedService] CdodDbContext cdodContext)
        {
            var s = await cdodContext.Students.FindAsync(id);
            if (s is null) throw new GraphQLException($"{nameof(Student)} not found!");
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
        [UseFiltering]
        [UseSorting]
        [Authorize(Roles = new[] { "admin" })]
        public IQueryable<GroupType> GetGroups(int? courseId, [ScopedService] CdodDbContext ctx)
        {
            if (courseId is not null)
            {
                if (!ctx.Courses.Any(c => c.Id == courseId))
                    throw new GraphQLException($"{nameof(Course)} not found!");

                return ctx.Groups.Where(g => g.CourseId == courseId)
                    .Select(g => new GroupType()
                    {
                        Id = g.Id,
                        Name = g.Name,
                        StartDate = g.StartDate,
                        CourseId = g.CourseId,
                        TeacherId = g.TeacherId
                    });
            }

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
        [Authorize(Roles = new[] { "admin" })]
        public async Task<GroupType> GetGroupAsync(int id, [ScopedService] CdodDbContext cdodContext)
        {
            var g = await cdodContext.Groups.FindAsync(id);
            if (g is null) throw new GraphQLException($"{nameof(Group)} not found!");
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
        [UseFiltering]
        [UseSorting]
        [Authorize(Roles = new[] { "admin" })]
        public IQueryable<CourseType> GetCourses([ScopedService] CdodDbContext ctx)
        {
            return ctx.Courses.Select(c => new CourseType()
            {
                Id = c.Id,
                Name = c.Name,
                ProgramId = c.ProgramId,
                CoursePrice = c.Price,
                EquipmentPriceWithRobot = c.EquipmentPriceWithRobot,
                EquipmentPriceWithoutRobot = c.EquipmentPriceWithoutRobot,
                DurationInMonths = c.DurationInMonths,
                Color = c.Color,
                SvgIconUrl = c.SvgIconUrl
            });
        }

        [UseDbContext(typeof(CdodDbContext))]
        [UseProjection]
        [Authorize(Roles = new[] { "admin" })]
        public async Task<CourseType> GetCourseAsync(int id, [ScopedService] CdodDbContext cdodContext)
        {
            var c = await cdodContext.Courses.FindAsync(id);
            if (c is null) throw new GraphQLException($"{nameof(Course)} not found!");
            return new CourseType()
            {
                Id = c.Id,
                Name = c.Name,
                ProgramId = c.ProgramId,
                CoursePrice = c.Price,
                EquipmentPriceWithRobot = c.EquipmentPriceWithRobot,
                EquipmentPriceWithoutRobot = c.EquipmentPriceWithoutRobot,
                DurationInMonths = c.DurationInMonths,
                Color = c.Color,
                SvgIconUrl = c.SvgIconUrl
            };
        }

        //Teacher queries
        [UseDbContext(typeof(CdodDbContext))]
        [UseFiltering]
        [UseSorting]
        [Authorize(Roles = new[] { "admin" })]
        public IQueryable<TeacherType> GetTeachers(int? lessonId, [ScopedService] CdodDbContext ctx)
        {
            if (lessonId is not null)
            {
                if (!ctx.Lessons.Any(l => l.Id == lessonId))
                    throw new GraphQLException($"{nameof(Lesson)} not found!");
                return from t in ctx.Teachers
                    join ttl in ctx.TeacherToLessons on t.UserId equals ttl.TeacherId
                    where ttl.LessonId == lessonId
                    select new TeacherType(t.User)
                    {
                        Id = t.UserId,
                        PostId = t.PostId,
                        WorkPlace = t.WorkPlace
                    };
            }

            return ctx.Teachers.Select(t => new TeacherType(t.User)
            {
                Id = t.UserId,
                PostId = t.PostId,
                WorkPlace = t.WorkPlace
            });
        }

        [UseDbContext(typeof(CdodDbContext))]
        [Authorize(Roles = new[] { "admin" })]
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

                if (teacher is null) throw new GraphQLException($"{nameof(Teacher)} not found!");
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

                if (teacher is null) throw new GraphQLException($"{nameof(Teacher)} not found!");
                return teacher;
            }

            throw new GraphQLException("Not enough params");
        }

        [UseDbContext(typeof(CdodDbContext))]
        [UseProjection]
        [UseFiltering]
        [UseSorting]
        [Authorize(Roles = new[] { "admin" })]
        public IQueryable<TeachersWorkTimeType> GetTeachersWorkTime(int? lessonId, int? teacherId,
            [ScopedService] CdodDbContext ctx)
        {
            if (lessonId is not null)
            {
                if (!ctx.Lessons.Any(l => l.Id == lessonId))
                    throw new GraphQLException($"{nameof(Lesson)} not found!");

                return from ttl in ctx.TeacherToLessons
                    where ttl.LessonId == lessonId
                    select new TeachersWorkTimeType()
                    {
                        TeacherId = ttl.TeacherId,
                        LessonId = ttl.LessonId,
                        WorkTime = ttl.WorkTime
                    };
            }
            if (teacherId is not null)
            {
                if (!ctx.Teachers.Any(t => t.UserId == teacherId))
                    throw new GraphQLException($"{nameof(Teacher)} not found!");

                return from ttl in ctx.TeacherToLessons
                    where ttl.TeacherId == teacherId
                    select new TeachersWorkTimeType()
                    {
                        TeacherId = ttl.TeacherId,
                        LessonId = ttl.LessonId,
                        WorkTime = ttl.WorkTime
                    };
            }

            throw new GraphQLException("Not enough params");
        }

        //Lesson queries
        [UseDbContext(typeof(CdodDbContext))]
        [UseProjection]
        [UseFiltering]
        [UseSorting]
        [Authorize(Roles = new[] { "admin" })]
        public IQueryable<LessonType> GetLessons(int? studentId, int? teacherId, int? groupId, 
            [ScopedService] CdodDbContext ctx)
        {
            if (studentId is not null)
            {
                if (!ctx.Students.Any(s => s.Id == studentId))
                    throw new GraphQLException($"{nameof(Student)} not found!");

                return from l in ctx.Lessons
                       join stl in ctx.StudentToLessons on l.Id equals stl.LessonId
                       where stl.StudentId == studentId
                       select new LessonType()
                       {
                           Id = l.Id,
                           Homework = l.Homework,
                           StartDateTime = l.StartDateTime,
                           Duration = l.Duration,
                           ClassRoom = l.ClassRoom,
                           LessonTopic = l.LessonTopic,
                           GroupId = l.GroupId

                       };
            }

            if (teacherId is not null)
            {
                if (!ctx.Teachers.Any(t => t.UserId == teacherId))
                    throw new GraphQLException($"{nameof(Teacher)} not found!");

                return from l in ctx.Lessons
                    join ttl in ctx.TeacherToLessons on l.Id equals ttl.LessonId
                    where ttl.TeacherId == teacherId
                    select new LessonType()
                    {
                        Id = l.Id,
                        Homework = l.Homework,
                        StartDateTime = l.StartDateTime,
                        Duration = l.Duration,
                        ClassRoom = l.ClassRoom,
                        LessonTopic = l.LessonTopic,
                        GroupId = l.GroupId

                    };
            }


            if (groupId is not null)
            {
                if (!ctx.Groups.Any(g => g.Id == groupId))
                    throw new GraphQLException($"{nameof(Group)} not found!");

                return from l in ctx.Lessons
                    where l.GroupId == groupId
                    select new LessonType()
                    {
                        Id = l.Id,
                        Homework = l.Homework,
                        StartDateTime = l.StartDateTime,
                        Duration = l.Duration,
                        ClassRoom = l.ClassRoom,
                        LessonTopic = l.LessonTopic,
                        GroupId = l.GroupId

                    };
            }

            return ctx.Lessons.Select(l => new LessonType()
            {
                Id = l.Id,
                Homework = l.Homework,
                StartDateTime = l.StartDateTime,
                Duration = l.Duration,
                ClassRoom = l.ClassRoom,
                LessonTopic = l.LessonTopic,
                GroupId = l.GroupId

            });
        }

        [UseDbContext(typeof(CdodDbContext))]
        [UseProjection]
        [Authorize(Roles = new[] { "admin" })]
        public async Task<LessonType> GetLessonAsync(int id, [ScopedService] CdodDbContext cdodContext)
        {
            var l = await cdodContext.Lessons.FindAsync(id);
            if (l is null) throw new GraphQLException($"{nameof(Lesson)} not found!");
            return new LessonType()
            {
                Id = l.Id,
                Homework = l.Homework,
                StartDateTime = l.StartDateTime,
                Duration = l.Duration,
                ClassRoom = l.ClassRoom,
                LessonTopic = l.LessonTopic,
                GroupId = l.GroupId
            };
        }

        //Attendance query
        [UseDbContext(typeof(CdodDbContext))]
        [UseProjection]
        [UseFiltering]
        [UseSorting]
        [Authorize(Roles = new[] { "admin" })]
        public IQueryable<AttendanceType> GetAttendances(int? lessonId, int? studentId, int? courseId,
            [ScopedService] CdodDbContext ctx)
        {
            if ((studentId is not null) && (courseId is not null))
            {
                if (!ctx.Students.Any(s => s.Id == studentId))
                    throw new GraphQLException($"{nameof(Student)} not found!");

                if (!ctx.Courses.Any(c => c.Id == courseId))
                    throw new GraphQLException($"{nameof(Course)} not found!");

                return from stl in ctx.StudentToLessons
                    join stc in ctx.StudentToCourses on stl.StudentId equals stc.CourseId
                    where stc.StudentId == studentId && stc.CourseId == courseId
                    select new AttendanceType()
                    {
                        LessonId = stl.LessonId,
                        Mark = stl.Mark,
                        Note = stl.Note,
                        Status = stl.Status,
                        StudentId = stl.StudentId
                    };
            }

            if (studentId is not null)
            {
                if (!ctx.Students.Any(s => s.Id == studentId))
                    throw new GraphQLException($"{nameof(Student)} not found!");

                return from stl in ctx.StudentToLessons
                    where stl.StudentId == studentId
                    select new AttendanceType()
                    {
                        LessonId = stl.LessonId,
                        Mark = stl.Mark,
                        Note = stl.Note,
                        Status = stl.Status,
                        StudentId = stl.StudentId
                    };
            }


            if (studentId is not null)
            {
                if (!ctx.Lessons.Any(l => l.Id == lessonId))
                    throw new GraphQLException($"{nameof(Lesson)} not found!");

                return from stl in ctx.StudentToLessons
                    where stl.LessonId == lessonId
                    select new AttendanceType()
                    {
                        LessonId = stl.LessonId,
                        Mark = stl.Mark,
                        Note = stl.Note,
                        Status = stl.Status,
                        StudentId = stl.StudentId
                    };
            }

            throw new GraphQLException("Not enough params");
        }

        //Parent queries
        [UseDbContext(typeof(CdodDbContext))]
        [UseFiltering]
        [UseSorting]
        [Authorize(Roles = new[] { "admin" })]
        public IQueryable<ParentType> GetParents([ScopedService] CdodDbContext ctx)
        {
            return ctx.Parents.Select(p => new ParentType(p.User)
            {
                Id = p.UserId,
                SignDate = p.SignDate,
                SecondEmail = p.SecondEmail,
                SecondPhoneNumber = p.SecondPhoneNumber,
                Type = p.Type
            });
        }

        [UseDbContext(typeof(CdodDbContext))]
        [Authorize(Roles = new[] { "admin" })]
        public async Task<ParentType> GetParentAsync(int id, [ScopedService] CdodDbContext cdodContext)
        {
            var query = from p in cdodContext.Parents
                where p.UserId == id
                select new ParentType(p.User)
                {
                    Id = p.UserId,
                    SignDate = p.SignDate,
                    SecondEmail = p.SecondEmail,
                    SecondPhoneNumber = p.SecondPhoneNumber,
                    Type = p.Type
                };

            var parent = await query.FirstOrDefaultAsync();

            if (parent is null) throw new GraphQLException($"{nameof(Parent)} not found!");
            return parent;
        }

        //School queries
        [UseDbContext(typeof(CdodDbContext))]
        [UseProjection]
        [UseFiltering]
        [UseSorting]
        [Authorize(Roles = new[] { "admin" })]
        public IQueryable<SchoolType> GetSchools([ScopedService] CdodDbContext ctx)
        {
            return ctx.Schools.Select(s => new SchoolType()
            {
                Id = s.Id,
                Name = s.Name,
                District = s.District,
            });
        }

        [UseDbContext(typeof(CdodDbContext))]
        [UseProjection]
        [Authorize(Roles = new[] { "admin" })]
        public async Task<SchoolType> GetSchoolAsync(int id, [ScopedService] CdodDbContext cdodContext)
        {
            var s = await cdodContext.Schools.FindAsync(id);
            if (s is null) throw new GraphQLException($"{nameof(School)} not found!");
            return new SchoolType()
            {
                Id = s.Id,
                Name = s.Name,
                District = s.District
            };
        }

        //Post queries
        [UseDbContext(typeof(CdodDbContext))]
        [UseProjection]
        [UseFiltering]
        [UseSorting]
        [Authorize(Roles = new[] { "admin" })]
        public IQueryable<PostType> GetPosts([ScopedService] CdodDbContext ctx)
        {
            return ctx.Posts.Select(p => new PostType()
            {
                Id = p.Id,
                Name = p.Name
            });
        }

        [UseDbContext(typeof(CdodDbContext))]
        [UseProjection]
        [Authorize(Roles = new[] { "admin" })]
        public async Task<PostType> GetPostAsync(int id, [ScopedService] CdodDbContext cdodContext)
        {
            var p = await cdodContext.Posts.FindAsync(id);
            if (p is null) throw new GraphQLException($"{nameof(Post)} not found!");
            return new PostType()
            {
                Id = p.Id,
                Name = p.Name
            };
        }

        //Program queries
        [UseDbContext(typeof(CdodDbContext))]
        [UseProjection]
        [UseFiltering]
        [UseSorting]
        [Authorize(Roles = new[] { "admin" })]
        public IQueryable<ProgramType> GetPrograms([ScopedService] CdodDbContext ctx)
        {
            return ctx.Programs.Select(p => new ProgramType()
            {
                Id = p.Id,
                Name = p.Name,
                ProgramFileUrl = p.ProgramFileUrl,
                Hours = p.Hours,
                Topics = p.Topics
            });
        }

        [UseDbContext(typeof(CdodDbContext))]
        [UseProjection]
        [Authorize(Roles = new[] { "admin" })]
        public async Task<ProgramType> GetProgramAsync(int id, [ScopedService] CdodDbContext cdodContext)
        {
            var p = await cdodContext.Programs.FindAsync(id);
            if (p is null) throw new GraphQLException($"{nameof(Program)} not found!");
            return new ProgramType()
            {
                Id = p.Id,
                Name = p.Name,
                ProgramFileUrl = p.ProgramFileUrl,
                Topics = p.Topics,
                Hours = p.Hours
            };
        }
    }
}
