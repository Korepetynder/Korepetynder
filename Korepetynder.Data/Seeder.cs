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
                new Language("Polski") { Id = 1, WasAccepted = true },
                new Language("Angielski") { Id = 2 , WasAccepted = true},
                new Language("Niemiecki") { Id = 3 , WasAccepted = true}
            };

            var levels = new Level[]
            {
                new Level("Szkoła podstawowa", 1) { Id = 1 , WasAccepted = true},
                new Level("Liceum", 2) { Id = 2 , WasAccepted = true},
                new Level("Studia wyższe", 3) { Id = 3 , WasAccepted = true}
            };

            var subjects = new Subject[]
            {
                new Subject("Matematyka") { Id = 1 , WasAccepted = true},
                new Subject("Informatyka") { Id = 2 , WasAccepted = true},
                new Subject("Chemia") { Id = 3, WasAccepted = true }
            };

            var locations = new Location[]
            {
                new Location("Warszawa") { Id = 1, WasAccepted = true },
                new Location("Wilanów") { Id = 2, ParentLocationId = 1, WasAccepted = true },
                new Location("Śródmieście") { Id = 3, ParentLocationId = 1, WasAccepted = true },
                new Location("Łódź") { Id = 4 , WasAccepted = true},
                new Location("Kraków") { Id = 5 , WasAccepted = true}
            };

            modelBuilder.Entity<Language>().HasData(languages);
            modelBuilder.Entity<Level>().HasData(levels);
            modelBuilder.Entity<Subject>().HasData(subjects);
            modelBuilder.Entity<Location>().HasData(locations);
        }
    }
}
