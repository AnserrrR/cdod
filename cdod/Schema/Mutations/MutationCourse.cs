﻿using cdod.Models;
using cdod.Schema.InputTypes;
using cdod.Schema.OutputTypes;
using cdod.Services;
using HotChocolate.AspNetCore.Authorization;

namespace cdod.Schema.Mutations
{
    [ExtendObjectType(typeof(Mutation))]
    public class MutationCourse
    {
        [Authorize(Roles = new[] { "admin" })]
        [UseDbContext(typeof(CdodDbContext))]// work
        public async Task<CourseType> CourseCreateCourse(CourseCreateInput course, [ScopedService] CdodDbContext dbContext)
        {
            Course _course = new Course()
            {
                Name = course.Name,
                ProgramId = course.ProgramId,
                Price = course.CoursePrice,
                EquipmentPriceWithoutRobot = course.EquipmentPriceWithoutRobot,
                EquipmentPriceWithRobot = course.EquipmentPriceWithRobot,
                DurationInMonths = course.DurationInMonths,
                SvgIconUrl = course.SvgIconColor,
                Color = course.Color,

            };
            dbContext.Courses.Add(_course);
            await dbContext.SaveChangesAsync();
            CourseType courseOutput = new CourseType()
            {
                Id = _course.Id,
                Name = _course.Name,
                ProgramId = _course.ProgramId,
                CoursePrice = _course.Price,
                EquipmentPriceWithoutRobot = _course.EquipmentPriceWithoutRobot,
                EquipmentPriceWithRobot = _course.EquipmentPriceWithRobot,
                DurationInMonths = _course.DurationInMonths,
                SvgIconUrl = course.SvgIconColor,
                Color = course.Color,
            };
            return courseOutput;
        }

        [Authorize(Roles = new[] { "admin" })]
        [UseDbContext(typeof(CdodDbContext))]// work
        public async Task<bool> CourseUpdateMany(List<CourseUpdateInput> courses, [ScopedService] CdodDbContext dbContext)
        {
            List<string> errorCourseIds = new List<string>();
            List<Course> courseUpdated = new List<Course>();
            foreach (CourseUpdateInput el in courses)
            {
                Course? _course = dbContext.Courses.FirstOrDefault(с => с.Id == el.Id);
                if (_course == null) { errorCourseIds.Add(el.Name); continue; }
                _course.Name = el.Name ?? _course.Name;
                _course.ProgramId = el.ProgramId ?? _course.ProgramId;
                _course.Price = el.CoursePrice ?? _course.Price;
                _course.EquipmentPriceWithRobot = el.EquipmentPriceWithRobot ?? _course.EquipmentPriceWithRobot;
                _course.EquipmentPriceWithoutRobot = el.EquipmentPriceWithoutRobot ?? _course.EquipmentPriceWithoutRobot;
                _course.SvgIconUrl = el.SvgIconColor ?? _course.SvgIconUrl;
                _course.Color = el.Color ?? _course.Color;
                courseUpdated.Add(_course);
            }
            dbContext.Courses.UpdateRange(courseUpdated);
            if (errorCourseIds.Count() > 0)
            {
                throw new GraphQLException($"Невозможно обновить следующие курсы:\n" +
                    $"следующих курсов нет в системе: {string.Join(" ", errorCourseIds)}\n");

            }
            return await dbContext.SaveChangesAsync() > 0;
        }

        /*        [UseDbContext(typeof(CdodDbContext))]
                public async Task<bool> CourseDeleteMany(List<int> courseIds, [ScopedService] CdodDbContext dbContext)
                {
                    dbContext.Courses.RemoveRange(courseIds.Select(c =>
                    {
                        Course? _course = dbContext.Courses.FirstOrDefault(cid => cid.Id == c);
                        if (_course == null) throw new Exception($"Вы указали для удаления несущесствующий ID курса :{c}");
                        return _course;
                    }));
                    return await dbContext.SaveChangesAsync() > 0;
                }

                [UseDbContext(typeof(CdodDbContext))]
                public async Task<bool> AttachStudentsToCourses(List<StudentToCourseCreateInput> studentsToCourses, [ScopedService] CdodDbContext dbContext)
                {
                    List<int> ErrorStudentToCourseIds = new List<int>();
                    List<int> ErrorStudentIds = new List<int>();
                    List<int> ErrorCourseIds = new List<int>();
                    List<StudentToCourse> studentToCourseAdded = new List<StudentToCourse>();
                    foreach (var el in studentsToCourses)
                    {
                        var studentToCourses = dbContext.StudentToCourses.FirstOrDefault(stc => ((stc.StudentId == el.StudentId)
                                                                                               && (stc.CourseId == el.CourseId)
                                                                                               && (stc.ContractState == el.ContractState)));
                        var course = dbContext.Courses.FirstOrDefault(c => c.Id == el.CourseId);
                        var student = dbContext.Students.FirstOrDefault(s => s.Id == el.StudentId);
                        if (studentToCourses is not null) { ErrorStudentToCourseIds.Add(el.CourseId); continue; }
                        if (course is null) { ErrorStudentIds.Add(el.CourseId); continue; }
                        if (student is null) { ErrorCourseIds.Add(el.CourseId); continue; }

                        StudentToCourse studentToCourse = new StudentToCourse()
                        {
                            StudentId = el.StudentId,
                            CourseId = el.CourseId,
                            SignDate = el.admissionDate,
                            ContractState = el.ContractState,
                            ContractUrl = el.ContractUrl,
                            EquipmentPriceWithRobot = el.isGetRobot
                        };
                        studentToCourseAdded.Add(studentToCourse);
                    }

                    dbContext.StudentToCourses.AddRange(studentToCourseAdded);
                    return await dbContext.SaveChangesAsync() > 0;
                }

                [UseDbContext(typeof(CdodDbContext))]
                public async Task<bool> UpdateStudentsToCourses(List<StudentToCourseUpdateInput> studentsToCourses, [ScopedService] CdodDbContext dbContext)
                {
                    List<int> ErrorStudentToCourseIds = new List<int>();
                    List<int> ErrorStudentIds = new List<int>();
                    List<int> ErrorCourseIds = new List<int>();
                    List<StudentToCourse> studentToCourseUpdated = new List<StudentToCourse>();
                    foreach (var el in studentsToCourses)
                    {
                        var studentToCourse = dbContext.StudentToCourses.FirstOrDefault(stc => ((stc.StudentId == el.StudentId)
                                                                                               && (stc.CourseId == el.CourseId)));
                        var course = dbContext.Courses.FirstOrDefault(c => c.Id == el.CourseId);
                        var student = dbContext.Students.FirstOrDefault(s => s.Id == el.StudentId);
                        if (studentToCourse is null) { ErrorStudentToCourseIds.Add(el.CourseId); continue; }
                        if (course is null) { ErrorStudentIds.Add(el.CourseId); continue; }
                        if (student is null) { ErrorCourseIds.Add(el.CourseId); continue; }
                        studentToCourse.SignDate = el.admissionDate ?? studentToCourse.SignDate;
                        studentToCourse.ContractState = el.ContractState ?? studentToCourse.ContractState;
                        studentToCourse.ContractUrl = el.ContractUrl ?? studentToCourse.ContractUrl;
                        studentToCourse.EquipmentPriceWithRobot = el.isGetRobot ?? studentToCourse.EquipmentPriceWithRobot;
                        studentToCourseUpdated.Add(studentToCourse);
                    }
                    dbContext.StudentToCourses.UpdateRange(studentToCourseUpdated);
                    return await dbContext.SaveChangesAsync() > 0;
                }

                [UseDbContext(typeof(CdodDbContext))]
                public async Task<bool> DetachStudentsToCourses(List<StudentToCourseDetach> studentsToCourses, [ScopedService] CdodDbContext dbContext)
                {
                    dbContext.StudentToCourses.RemoveRange(studentsToCourses.Select(stc =>
                    {
                        var studentToCourse = dbContext.StudentToCourses.FirstOrDefault(_stc =>
                        ((stc.courseId == _stc.CourseId) && (stc.studentId == _stc.StudentId)));
                        if (studentToCourse == null) throw new Exception($"Вы указали для удаления несущесствующего ученика ID:{stc.studentId}");
                        return studentToCourse;
                    }));
                    return await dbContext.SaveChangesAsync() > 0;
                }*/
    }
}
