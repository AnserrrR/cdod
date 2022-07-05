﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using cdod.Services;

#nullable disable

namespace cdod.Migrations
{
    [DbContext(typeof(CdodDbContext))]
    partial class CdodDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("AnnouncementDTOCourseDTO", b =>
                {
                    b.Property<int>("AnnouncementsId")
                        .HasColumnType("int");

                    b.Property<int>("CoursesId")
                        .HasColumnType("int");

                    b.HasKey("AnnouncementsId", "CoursesId");

                    b.HasIndex("CoursesId");

                    b.ToTable("AnnouncementDTOCourseDTO");
                });

            modelBuilder.Entity("AnnouncementDTOGroupDTO", b =>
                {
                    b.Property<int>("AnnouncementsId")
                        .HasColumnType("int");

                    b.Property<int>("GroupsId")
                        .HasColumnType("int");

                    b.HasKey("AnnouncementsId", "GroupsId");

                    b.HasIndex("GroupsId");

                    b.ToTable("AnnouncementDTOGroupDTO");
                });

            modelBuilder.Entity("cdodDTOs.DTOs.AnnouncementDTO", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateOnly>("EndDate")
                        .HasColumnType("date");

                    b.Property<string>("Heading")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("PictureUrl")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateOnly>("StartDate")
                        .HasColumnType("date");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Announcements");
                });

            modelBuilder.Entity("cdodDTOs.DTOs.ContractStateDTO", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("ContractStates");
                });

            modelBuilder.Entity("cdodDTOs.DTOs.CourseDTO", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<decimal>("CoursePrice")
                        .HasColumnType("decimal(65,30)");

                    b.Property<decimal>("EquipmentPrice")
                        .HasColumnType("decimal(65,30)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("ProgramFileUrl")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Courses");
                });

            modelBuilder.Entity("cdodDTOs.DTOs.GroupDTO", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("CourseId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("StartYear")
                        .HasColumnType("int");

                    b.Property<int>("TeacherId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CourseId");

                    b.HasIndex("TeacherId");

                    b.ToTable("Groups");
                });

            modelBuilder.Entity("cdodDTOs.DTOs.LessonDTO", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("ClassRoom")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("Duration")
                        .HasColumnType("int");

                    b.Property<int>("GroupId")
                        .HasColumnType("int");

                    b.Property<string>("Homework")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("LessonTopic")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<TimeOnly>("StartTime")
                        .HasColumnType("time(6)");

                    b.HasKey("Id");

                    b.HasIndex("GroupId");

                    b.ToTable("Lessons");
                });

            modelBuilder.Entity("cdodDTOs.DTOs.ParentDTO", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<string>("SecondEmail")
                        .HasColumnType("longtext");

                    b.Property<string>("SecondPhoneNumber")
                        .HasColumnType("longtext");

                    b.Property<DateOnly>("SignDate")
                        .HasColumnType("date");

                    b.HasKey("UserId");

                    b.ToTable("Parents");
                });

            modelBuilder.Entity("cdodDTOs.DTOs.PayNoteDTO", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("Appointment")
                        .HasColumnType("int");

                    b.Property<string>("CheckURL")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateOnly>("Date")
                        .HasColumnType("date");

                    b.Property<int>("StudentId")
                        .HasColumnType("int");

                    b.Property<decimal>("Sum")
                        .HasColumnType("decimal(65,30)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("StudentId");

                    b.HasIndex("UserId");

                    b.ToTable("PayNotes");
                });

            modelBuilder.Entity("cdodDTOs.DTOs.PostDTO", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Posts");
                });

            modelBuilder.Entity("cdodDTOs.DTOs.SchoolDTO", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("District")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Schools");
                });

            modelBuilder.Entity("cdodDTOs.DTOs.StudentDTO", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateOnly>("BirthDate")
                        .HasColumnType("date");

                    b.Property<string>("Descriotion")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("ParentId")
                        .HasColumnType("int");

                    b.Property<string>("Patronymic")
                        .HasColumnType("longtext");

                    b.Property<int>("SchoolId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ParentId");

                    b.HasIndex("SchoolId");

                    b.ToTable("Students");
                });

            modelBuilder.Entity("cdodDTOs.DTOs.StudentToCourseDTO", b =>
                {
                    b.Property<int>("StudentId")
                        .HasColumnType("int");

                    b.Property<int>("CourseId")
                        .HasColumnType("int");

                    b.Property<int>("ContractStateId")
                        .HasColumnType("int");

                    b.Property<int>("SignYear")
                        .HasColumnType("int");

                    b.HasKey("StudentId", "CourseId");

                    b.HasIndex("ContractStateId");

                    b.HasIndex("CourseId");

                    b.ToTable("StudentToCourses");
                });

            modelBuilder.Entity("cdodDTOs.DTOs.StudentToLessonDTO", b =>
                {
                    b.Property<int>("StudentId")
                        .HasColumnType("int");

                    b.Property<int>("LessonId")
                        .HasColumnType("int");

                    b.Property<int>("Mark")
                        .HasColumnType("int");

                    b.Property<string>("Note")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("StudentId", "LessonId");

                    b.HasIndex("LessonId");

                    b.ToTable("StudentToLessons");
                });

            modelBuilder.Entity("cdodDTOs.DTOs.TeacherDTO", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<int>("PostId")
                        .HasColumnType("int");

                    b.Property<string>("WorkPlace")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("UserId");

                    b.HasIndex("PostId");

                    b.ToTable("Teachers");
                });

            modelBuilder.Entity("cdodDTOs.DTOs.TeacherToLessonDTO", b =>
                {
                    b.Property<int>("TeacherId")
                        .HasColumnType("int");

                    b.Property<int>("LessonId")
                        .HasColumnType("int");

                    b.Property<TimeOnly>("WorkTime")
                        .HasColumnType("time(6)");

                    b.HasKey("TeacherId", "LessonId");

                    b.HasIndex("LessonId");

                    b.ToTable("TeacherToLessons");
                });

            modelBuilder.Entity("cdodDTOs.DTOs.UserDTO", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Address")
                        .HasColumnType("longtext");

                    b.Property<DateOnly>("Birthday")
                        .HasColumnType("date");

                    b.Property<string>("Education")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Firstname")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Inn")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<bool>("IsAdmin")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("tinyint(1)")
                        .HasDefaultValue(false);

                    b.Property<string>("Lastname")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Patronymic")
                        .HasColumnType("longtext");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Snils")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("passportCode")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateOnly>("passportDate")
                        .HasColumnType("date");

                    b.Property<string>("passportIssue")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("passportNo")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("PhoneNumber");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("CourseDTOTeacherDTO", b =>
                {
                    b.Property<int>("CoursesId")
                        .HasColumnType("int");

                    b.Property<int>("TeachersUserId")
                        .HasColumnType("int");

                    b.HasKey("CoursesId", "TeachersUserId");

                    b.HasIndex("TeachersUserId");

                    b.ToTable("CourseDTOTeacherDTO");
                });

            modelBuilder.Entity("GroupDTOStudentDTO", b =>
                {
                    b.Property<int>("GroupsId")
                        .HasColumnType("int");

                    b.Property<int>("StudentsId")
                        .HasColumnType("int");

                    b.HasKey("GroupsId", "StudentsId");

                    b.HasIndex("StudentsId");

                    b.ToTable("GroupDTOStudentDTO");
                });

            modelBuilder.Entity("AnnouncementDTOCourseDTO", b =>
                {
                    b.HasOne("cdodDTOs.DTOs.AnnouncementDTO", null)
                        .WithMany()
                        .HasForeignKey("AnnouncementsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("cdodDTOs.DTOs.CourseDTO", null)
                        .WithMany()
                        .HasForeignKey("CoursesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("AnnouncementDTOGroupDTO", b =>
                {
                    b.HasOne("cdodDTOs.DTOs.AnnouncementDTO", null)
                        .WithMany()
                        .HasForeignKey("AnnouncementsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("cdodDTOs.DTOs.GroupDTO", null)
                        .WithMany()
                        .HasForeignKey("GroupsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("cdodDTOs.DTOs.AnnouncementDTO", b =>
                {
                    b.HasOne("cdodDTOs.DTOs.UserDTO", "User")
                        .WithMany("Announcements")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("cdodDTOs.DTOs.GroupDTO", b =>
                {
                    b.HasOne("cdodDTOs.DTOs.CourseDTO", "Course")
                        .WithMany("Groups")
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("cdodDTOs.DTOs.TeacherDTO", "Teacher")
                        .WithMany("Groups")
                        .HasForeignKey("TeacherId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Course");

                    b.Navigation("Teacher");
                });

            modelBuilder.Entity("cdodDTOs.DTOs.LessonDTO", b =>
                {
                    b.HasOne("cdodDTOs.DTOs.GroupDTO", "Group")
                        .WithMany("Lessons")
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Group");
                });

            modelBuilder.Entity("cdodDTOs.DTOs.ParentDTO", b =>
                {
                    b.HasOne("cdodDTOs.DTOs.UserDTO", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("cdodDTOs.DTOs.PayNoteDTO", b =>
                {
                    b.HasOne("cdodDTOs.DTOs.StudentDTO", "Student")
                        .WithMany("PayNotes")
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("cdodDTOs.DTOs.UserDTO", "User")
                        .WithMany("PayNotes")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Student");

                    b.Navigation("User");
                });

            modelBuilder.Entity("cdodDTOs.DTOs.StudentDTO", b =>
                {
                    b.HasOne("cdodDTOs.DTOs.ParentDTO", "Parent")
                        .WithMany("Students")
                        .HasForeignKey("ParentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("cdodDTOs.DTOs.SchoolDTO", "School")
                        .WithMany("Students")
                        .HasForeignKey("SchoolId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Parent");

                    b.Navigation("School");
                });

            modelBuilder.Entity("cdodDTOs.DTOs.StudentToCourseDTO", b =>
                {
                    b.HasOne("cdodDTOs.DTOs.ContractStateDTO", "ContractState")
                        .WithMany("StudentsToCourses")
                        .HasForeignKey("ContractStateId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("cdodDTOs.DTOs.CourseDTO", "Course")
                        .WithMany("StudentToCourses")
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("cdodDTOs.DTOs.StudentDTO", "Student")
                        .WithMany("StudentToCourses")
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ContractState");

                    b.Navigation("Course");

                    b.Navigation("Student");
                });

            modelBuilder.Entity("cdodDTOs.DTOs.StudentToLessonDTO", b =>
                {
                    b.HasOne("cdodDTOs.DTOs.LessonDTO", "Lesson")
                        .WithMany("StudentsToLessons")
                        .HasForeignKey("LessonId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("cdodDTOs.DTOs.StudentDTO", "Student")
                        .WithMany("StudentToLessons")
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Lesson");

                    b.Navigation("Student");
                });

            modelBuilder.Entity("cdodDTOs.DTOs.TeacherDTO", b =>
                {
                    b.HasOne("cdodDTOs.DTOs.PostDTO", "Post")
                        .WithMany()
                        .HasForeignKey("PostId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("cdodDTOs.DTOs.UserDTO", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Post");

                    b.Navigation("User");
                });

            modelBuilder.Entity("cdodDTOs.DTOs.TeacherToLessonDTO", b =>
                {
                    b.HasOne("cdodDTOs.DTOs.LessonDTO", "Lesson")
                        .WithMany("TeacherToLessons")
                        .HasForeignKey("LessonId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("cdodDTOs.DTOs.TeacherDTO", "Teacher")
                        .WithMany("TeachersToLessons")
                        .HasForeignKey("TeacherId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Lesson");

                    b.Navigation("Teacher");
                });

            modelBuilder.Entity("CourseDTOTeacherDTO", b =>
                {
                    b.HasOne("cdodDTOs.DTOs.CourseDTO", null)
                        .WithMany()
                        .HasForeignKey("CoursesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("cdodDTOs.DTOs.TeacherDTO", null)
                        .WithMany()
                        .HasForeignKey("TeachersUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("GroupDTOStudentDTO", b =>
                {
                    b.HasOne("cdodDTOs.DTOs.GroupDTO", null)
                        .WithMany()
                        .HasForeignKey("GroupsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("cdodDTOs.DTOs.StudentDTO", null)
                        .WithMany()
                        .HasForeignKey("StudentsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("cdodDTOs.DTOs.ContractStateDTO", b =>
                {
                    b.Navigation("StudentsToCourses");
                });

            modelBuilder.Entity("cdodDTOs.DTOs.CourseDTO", b =>
                {
                    b.Navigation("Groups");

                    b.Navigation("StudentToCourses");
                });

            modelBuilder.Entity("cdodDTOs.DTOs.GroupDTO", b =>
                {
                    b.Navigation("Lessons");
                });

            modelBuilder.Entity("cdodDTOs.DTOs.LessonDTO", b =>
                {
                    b.Navigation("StudentsToLessons");

                    b.Navigation("TeacherToLessons");
                });

            modelBuilder.Entity("cdodDTOs.DTOs.ParentDTO", b =>
                {
                    b.Navigation("Students");
                });

            modelBuilder.Entity("cdodDTOs.DTOs.SchoolDTO", b =>
                {
                    b.Navigation("Students");
                });

            modelBuilder.Entity("cdodDTOs.DTOs.StudentDTO", b =>
                {
                    b.Navigation("PayNotes");

                    b.Navigation("StudentToCourses");

                    b.Navigation("StudentToLessons");
                });

            modelBuilder.Entity("cdodDTOs.DTOs.TeacherDTO", b =>
                {
                    b.Navigation("Groups");

                    b.Navigation("TeachersToLessons");
                });

            modelBuilder.Entity("cdodDTOs.DTOs.UserDTO", b =>
                {
                    b.Navigation("Announcements");

                    b.Navigation("PayNotes");
                });
#pragma warning restore 612, 618
        }
    }
}
