using cdodDTOs.DTOs;
using Microsoft.EntityFrameworkCore;

namespace cdod.Services
{
    public class CdodDbContext : DbContext
    {
        public CdodDbContext(DbContextOptions<CdodDbContext> options)
           : base(options) { }

        public DbSet<UserDTO> Users { get; set; }
        public DbSet<ParentDTO> Parents { get; set; }
        public DbSet<StudentDTO> Students { get; set; }
        public DbSet<SchoolDTO> Schools { get; set; }
        public DbSet<AnnouncementDTO> Announcements { get; set; }
        public DbSet<ContractStateDTO> ContractStates { get; set; }
        public DbSet<CourseDTO> Courses { get; set; }
        public DbSet<GroupDTO> Groups { get; set; }
        public DbSet<LessonDTO> Lessons { get; set; }
        public DbSet<PayNoteDTO> PayNotes { get; set; }
        public DbSet<PostDTO> Posts { get; set; }
        public DbSet<StudentToCourseDTO> StudentToCourses { get; set; }
        public DbSet<StudentToLessonDTO> StudentToLessons { get; set; }
        public DbSet<TeacherDTO> Teachers { get; set; }
        public DbSet<TeacherToLessonDTO> TeacherToLessons { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // USER TABLE BEGIN
            modelBuilder.Entity<UserDTO>().Property(u => u.IsAdmin).HasDefaultValue(0);
            // USER TABLE END

            //PARENT TABLE BEGIN
            //modelBuilder.Entity<ParentDTO>().Property(u => u.SignDate).HasDefaultValue("GETDATE()");
            //PARENT TABLE END

            modelBuilder.Entity<StudentDTO>()
                .HasMany(s => s.Groups)
                .WithMany(g => g.Students)
                .UsingEntity(j => j.ToTable("StudentsToGroups"));

            modelBuilder.Entity<TeacherDTO>()
                .HasMany(t => t.Courses)
                .WithMany(c => c.Teachers)
                .UsingEntity(j => j.ToTable("TeachersToCourses"));

            modelBuilder.Entity<AnnouncementDTO>()
                .HasMany(a => a.Groups)
                .WithMany(g => g.Announcements)
                .UsingEntity(j => j.ToTable("AnnouncementsToGroups"));

            modelBuilder.Entity<AnnouncementDTO>()
                .HasMany(a => a.Courses)
                .WithMany(c => c.Announcements)
                .UsingEntity(j => j.ToTable("AnnouncementsToCourses"));

            modelBuilder
                .Entity<StudentDTO>()
                .HasMany(s => s.Courses)
                .WithMany(c => c.Students)
                .UsingEntity<StudentToCourseDTO>(
                    j => j
                        .HasOne(pt => pt.Course)
                        .WithMany(t => t.StudentToCourses)
                        .HasForeignKey(pt => pt.CourseId),
                    j => j
                        .HasOne(pt => pt.Student)
                        .WithMany(p => p.StudentToCourses)
                        .HasForeignKey(pt => pt.StudentId),
                    j =>
                    {
                        j.Property(pt => pt.SignYear);
                        j.HasKey(t => new { t.CourseId, t.StudentId });
                        j.ToTable("StudentsToCourses");
                    });

            modelBuilder
                .Entity<StudentDTO>()
                .HasMany(s => s.Lessons)
                .WithMany(l => l.Students)
                .UsingEntity<StudentToLessonDTO>(
                    j => j
                        .HasOne(pt => pt.Lesson)
                        .WithMany(t => t.StudentsToLessons)
                        .HasForeignKey(pt => pt.LessonId),
                    j => j
                        .HasOne(pt => pt.Student)
                        .WithMany(p => p.StudentToLessons)
                        .HasForeignKey(pt => pt.StudentId),
                    j =>
                    {
                        j.Property(pt => pt.Mark);
                        j.Property(pt => pt.Note);
                        j.Property(pt => pt.Status);
                        j.HasKey(t => new { t.LessonId, t.StudentId });
                        j.ToTable("StudentsToLessons");
                    });

            modelBuilder
                .Entity<TeacherDTO>()
                .HasMany(t => t.Lessons)
                .WithMany(l => l.Teachers)
                .UsingEntity<TeacherToLessonDTO>(
                    j => j
                        .HasOne(pt => pt.Lesson)
                        .WithMany(t => t.TeacherToLessons)
                        .HasForeignKey(pt => pt.LessonId),
                    j => j
                        .HasOne(pt => pt.Teacher)
                        .WithMany(p => p.TeacherToLessons)
                        .HasForeignKey(pt => pt.TeacherId),
                    j =>
                    {
                        j.Property(pt => pt.WorkTime);
                        j.HasKey(t => new { t.TeacherId, t.LessonId });
                        j.ToTable("TeachersToLessons");
                    });
        }
    }
}
