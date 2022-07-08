using cdod.Schema.InputTypes;
using cdod.Services;
using cdods.s;
using Microsoft.EntityFrameworkCore;

namespace cdod.Schema.Mutations
{
    [ExtendObjectType(typeof(Mutation))]
    public class MutationCourse
    {
        [UseDbContext(typeof(CdodDbContext))]
        public async Task<Course> CreateCourse(CourseInput courseForm, [ScopedService] CdodDbContext dbContext)
        {
            Course course = new Course()
            {
                Name = courseForm.Name,
                ProgramFileUrl = courseForm.ProgramFileUrl,
                CoursePrice = (double)courseForm.CoursePrice,
                EquipmentPriceWithRobot = courseForm.EquipmentPrice,
                DurationInMonths = (int)courseForm.DurationInMonths,
            };
            dbContext.Courses.Add(course);
            await dbContext.SaveChangesAsync();
            return course;
        }

        [UseDbContext(typeof(CdodDbContext))]
        public async Task<Course> UpdateCourse(int id, CourseInput courseForm, [ScopedService] CdodDbContext dbContext)
        {
            Course? course = dbContext.Courses.FirstOrDefault(c => c.Id == id);
            if (course == null) throw new Exception("Вы пытаетесь обновить не существующий курс");

            course.Name = courseForm.Name ?? course.Name;
            course.ProgramFileUrl = courseForm.ProgramFileUrl ?? course.ProgramFileUrl;
            course.CoursePrice = courseForm.CoursePrice ?? course.CoursePrice;
            course.EquipmentPriceWithRobot = courseForm.EquipmentPrice ?? course.EquipmentPriceWithRobot;
            course.DurationInMonths = courseForm.DurationInMonths ?? course.DurationInMonths;
            
            dbContext.Courses.Update(course);
            await dbContext.SaveChangesAsync();
            return course;
        }

        [UseDbContext(typeof(CdodDbContext))]
        public async Task<bool> DeleteCourse(int id, [ScopedService] CdodDbContext dbContext)
        {
            Course? course = dbContext.Courses.FirstOrDefault(c => c.Id == id);
            if (course == null) throw new Exception("Вы пытаетесь удалить не существующий курс");
            dbContext.Courses.Remove(course);
            return await dbContext.SaveChangesAsync() > 0;
        }

        [UseDbContext(typeof(CdodDbContext))]
        public async Task<bool> CreateManyStudentForCourse(int idCourse, List<int> idStudents, [ScopedService] CdodDbContext dbContext)
        {
            dbContext.StudentToCourses.AddRange(idStudents.Select(s =>
            { 
            if (dbContext.StudentToCourses.FirstOrDefault(sid => sid.StudentId == s && (sid.CourseId == idCourse)) is not null) throw new Exception($"Ученика с таким ID не существует: {s}");
            StudentToCourse bufStudent = new StudentToCourse()
            {
                StudentId = s,
                CourseId = idCourse,
            };
                
            return bufStudent;
            }));
            await dbContext.SaveChangesAsync();
            return true;
        }

    }
}
