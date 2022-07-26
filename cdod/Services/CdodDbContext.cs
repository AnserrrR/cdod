using cdod.Models;
using Microsoft.EntityFrameworkCore;

namespace cdod.Services
{
    public class CdodDbContext : DbContext
    {
        public CdodDbContext(DbContextOptions<CdodDbContext> options)
           : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Parent> Parents { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<School> Schools { get; set; }
        public DbSet<Announcement> Announcements { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Lesson> Lessons { get; set; }
        public DbSet<PayNote> PayNotes { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<CourseProgram> Programs { get; set; }
        public DbSet<Topic> Topics { get; set; }
        public DbSet<WageRate> WageRates { get; set; }

        public DbSet<StudentToCourse> StudentToCourses { get; set; }
        public DbSet<StudentToLesson> StudentToLessons { get; set; }
        public DbSet<TeacherToLesson> TeacherToLessons { get; set; }
        public DbSet<AnnouncementToUser> AnnouncementsToUsers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // USER TABLE BEGIN
            modelBuilder.Entity<User>().Property(u => u.IsAdmin).HasDefaultValue(0);
            // USER TABLE END

            //PARENT TABLE BEGIN
            //modelBuilder.Entity<Parent>().Property(u => u.SignDate).HasDefaultValue("GETDATE()");
            //PARENT TABLE END

            //modelBuilder.Entity<Student>()
            //    .HasMany(s => s.Groups)
            //    .WithMany(g => g.Students)
            //    .UsingEntity(j => j.ToTable("StudentsToGroups"));

            modelBuilder.Entity<Teacher>()
                .HasMany(t => t.Courses)
                .WithMany(c => c.Teachers)
                .UsingEntity(j => j.ToTable("TeachersToCourses"));

            modelBuilder.Entity<Announcement>()
                .HasMany(a => a.Groups)
                .WithMany(g => g.Announcements)
                .UsingEntity(j => j.ToTable("AnnouncementsToGroups"));

            modelBuilder.Entity<Announcement>()
                .HasMany(a => a.Courses)
                .WithMany(c => c.Announcements)
                .UsingEntity(j => j.ToTable("AnnouncementsToCourses"));

           modelBuilder.Entity<StudentToCourse>().HasKey(e => new {e.CourseId, e.StudentId, e.Attempt});

           modelBuilder.Entity<TeacherToLesson>().HasKey(e => new {e.TeacherId, e.LessonId});

           modelBuilder.Entity<StudentToLesson>().HasKey(e => new {e.LessonId, e.StudentId});

           modelBuilder.Entity<AnnouncementToUser>().HasKey(e => new {e.AnnouncementId, e.UserId});
        }
    }
}
