using Korepetynder.Data.DbModels;
using Microsoft.EntityFrameworkCore;

namespace Korepetynder.Data
{
    internal static class Seeder
    {
        public static void SeedData(ModelBuilder modelBuilder)
        {
            var languages = new Language[]
            {
                new Language("Polski") { Id = 1 },
                new Language("Angielski") { Id = 2 },
                new Language("Niemiecki") { Id = 3 }
            };

            var levels = new Level[]
            {
                new Level("Szkoła podstawowa", 1) { Id = 1 },
                new Level("Liceum", 2) { Id = 2 },
                new Level("Studia wyższe", 3) { Id = 3 }
            };

            var subjects = new Subject[]
            {
                new Subject("Matematyka") { Id = 1 },
                new Subject("Informatyka") { Id = 2 },
                new Subject("Chemia") { Id = 3 }
            };

            var locations = new Location[]
            {
                new Location("Warszawa") { Id = 1 },
                new Location("Wilanów") { Id = 2, ParentLocationId = 1 },
                new Location("Śródmieście") { Id = 3, ParentLocationId = 1 },
                new Location("Łódź") { Id = 4 },
                new Location("Kraków") { Id = 5 }
            };

            modelBuilder.Entity<Language>().HasData(languages);
            modelBuilder.Entity<Level>().HasData(levels);
            modelBuilder.Entity<Subject>().HasData(subjects);
            modelBuilder.Entity<Location>().HasData(locations);
        }
    }
}
