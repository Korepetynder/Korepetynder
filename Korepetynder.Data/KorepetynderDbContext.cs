using Korepetynder.Data.DbModels;
using Microsoft.EntityFrameworkCore;
namespace Korepetynder.Data
{
    public class KorepetynderDbContext : DbContext
    {
        public KorepetynderDbContext(DbContextOptions<KorepetynderDbContext> options)
            : base(options)
        {
        }

        // Useful docs about working with EF Core and nullable reference types:
        // https://docs.microsoft.com/en-us/ef/core/miscellaneous/nullable-reference-types#dbcontext-and-dbset

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MultimediaFile>()
                .Property(file => file.Type)
                .HasConversion<int>();

            modelBuilder.Entity<User>()
                .Property(user => user.FullName)
                .HasComputedColumnSql($"[{nameof(User.FirstName)}] + ' ' + [{nameof(User.LastName)}]");
            modelBuilder.Entity<StudentLesson>()
                .HasMany(lesson => lesson.Levels)
                .WithMany(level => level.Lessons)
                .UsingEntity(join => join.ToTable("LessonLevels"));
            modelBuilder.Entity<StudentLesson>()
                .HasMany(lesson => lesson.Languages)
                .WithMany(language => language.Lessons)
                .UsingEntity(join => join.ToTable("LessonLanguages"));

            modelBuilder.Entity<Student>()
                .HasMany(student => student.PreferredLocations)
                .WithMany(location => location.Students)
                .UsingEntity(join => join.ToTable("StudentPreferredLocations"));

            modelBuilder.Entity<Teacher>()
                .HasMany(teacher => teacher.Students)
                .WithMany(student => student.Teachers)
                .UsingEntity(join => join.ToTable("TeacherStudents"));
            modelBuilder.Entity<Teacher>()
                .HasMany(teacher => teacher.TeachingLocations)
                .WithMany(location => location.Teachers)
                .UsingEntity(join => join.ToTable("TeacherTeachingLocations"));

            modelBuilder.Entity<User>()
                .Property(user => user.BirthDate)
                .HasConversion(date => date, date => DateTime.SpecifyKind(date, DateTimeKind.Utc));
        }

        public DbSet<User> Users => Set<User>();
        public DbSet<Teacher> Teachers => Set<Teacher>();
        public DbSet<Student> Students => Set<Student>();
        public DbSet<Subject> Subjects => Set<Subject>();
        public DbSet<MultimediaFile> MultimediaFiles => Set<MultimediaFile>();
        public DbSet<ProfilePicture> ProfilePictures => Set<ProfilePicture>();
        public DbSet<Location> Locations => Set<Location>();
        public DbSet<Level> Levels => Set<Level>();
        public DbSet<Language> Languages => Set<Language>();
        public DbSet<StudentLesson> StudentLesson => Set<StudentLesson>();
        public DbSet<TeacherLesson> TeacherLesson => Set<TeacherLesson>();

    }
}
