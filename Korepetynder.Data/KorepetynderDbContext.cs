using Microsoft.EntityFrameworkCore;
using Korepetynder.Data.DbModels;
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
           
        }

        public DbSet<User> Users => Set<User>();
        public DbSet<Teacher> Teachers => Set<Teacher>();
        public DbSet<Student> Students => Set<Student>();
        public DbSet<Subject> Subjects => Set<Subject>();
        public DbSet<MultimediaFile> Files => Set<MultimediaFile>();

    }
}
