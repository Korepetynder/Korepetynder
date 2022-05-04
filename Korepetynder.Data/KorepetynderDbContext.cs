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
            Seeder.SeedData(modelBuilder);

            modelBuilder.Entity<MultimediaFile>()
                .Property(file => file.Type)
                .HasConversion<int>();

            modelBuilder.Entity<User>()
                .Property(user => user.FullName)
                .HasComputedColumnSql($"[{nameof(User.FirstName)}] + ' ' + [{nameof(User.LastName)}]");
            modelBuilder.Entity<StudentLesson>()
                .HasMany(lesson => lesson.Levels)
                .WithMany(level => level.StudentLessons)
                .UsingEntity(join => join.ToTable("LessonLevels"));
            modelBuilder.Entity<TutorLesson>()
                .HasMany(lesson => lesson.Levels)
                .WithMany(level => level.TutorLessons)
                .UsingEntity(join => join.ToTable("TutorLessonLevels"));
            modelBuilder.Entity<StudentLesson>()
                .HasMany(lesson => lesson.Languages)
                .WithMany(language => language.StudentLessons)
                .UsingEntity(join => join.ToTable("LessonLanguages"));
            modelBuilder.Entity<TutorLesson>()
                .HasMany(lesson => lesson.Languages)
                .WithMany(language => language.TutorLessons)
                .UsingEntity(join => join.ToTable("TutorLessonLanguages"));

            modelBuilder.Entity<Student>()
                .HasMany(student => student.PreferredLocations)
                .WithMany(location => location.Students)
                .UsingEntity(join => join.ToTable("StudentPreferredLocations"));

            modelBuilder.Entity<Tutor>()
                .HasMany(tutor => tutor.DiscardedByStudents)
                .WithMany(student => student.DiscardedTeachers)
                .UsingEntity(join => join.ToTable("DiscardedTutorStudents"));
            modelBuilder.Entity<Tutor>()
                .HasMany(tutor => tutor.FavouritedByStudents)
                .WithMany(student => student.FavouriteTeachers)
                .UsingEntity(join => join.ToTable("FavouriteTutorStudents"));
            modelBuilder.Entity<Tutor>()
                .HasMany(tutor => tutor.TeachingLocations)
                .WithMany(location => location.Teachers)
                .UsingEntity(join => join.ToTable("TutorTeachingLocations"));

            modelBuilder.Entity<User>()
                .Property(user => user.BirthDate)
                .HasConversion(date => date, date => DateTime.SpecifyKind(date, DateTimeKind.Utc));

            modelBuilder.Entity<User>()
                .HasOne(user => user.Student)
                .WithOne(student => student.User)
                .HasForeignKey<Student>(student => student.UserId)
                .OnDelete(DeleteBehavior.ClientCascade);

            modelBuilder.Entity<User>()
                .HasOne(user => user.Tutor)
                .WithOne(tutor => tutor.User)
                .HasForeignKey<Tutor>(tutor => tutor.UserId)
                .OnDelete(DeleteBehavior.ClientCascade);
        }

        public DbSet<User> Users => Set<User>();
        public DbSet<Tutor> Tutors => Set<Tutor>();
        public DbSet<Student> Students => Set<Student>();
        public DbSet<Subject> Subjects => Set<Subject>();
        public DbSet<MultimediaFile> MultimediaFiles => Set<MultimediaFile>();
        public DbSet<ProfilePicture> ProfilePictures => Set<ProfilePicture>();
        public DbSet<Location> Locations => Set<Location>();
        public DbSet<Level> Levels => Set<Level>();
        public DbSet<Language> Languages => Set<Language>();
        public DbSet<StudentLesson> StudentLessons => Set<StudentLesson>();
        public DbSet<TutorLesson> TutorLessons => Set<TutorLesson>();

    }
}
