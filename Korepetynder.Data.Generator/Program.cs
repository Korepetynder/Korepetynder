using Korepetynder.Data;
using Korepetynder.Data.DbModels;
using Microsoft.EntityFrameworkCore;

Random random = new();

ICollection<T> RandomValues<T>(IList<T> values)
{
    int numberOfValues = random.Next(1, values.Count() + 1);
    var resultSet = new HashSet<int>(numberOfValues);
    var result = new List<T>(numberOfValues);
    while (resultSet.Count != numberOfValues)
    {
        int index = random.Next(0, values.Count());
        if (resultSet.Contains(index))
        {
            continue;
        }
        resultSet.Add(index);
        result.Add(values[index]);
    }

    return result;
}

ICollection<TeacherLesson> GenerateLessons(int numberOfLessons, IList<Subject> subjects, IList<Level> levels, IList<Language> languages)
{
    var result = new List<TeacherLesson>();
    for (int i = 0; i < numberOfLessons; i++)
    {
        result.Add(new TeacherLesson
        {
            Cost = random.Next(20, 101),
            Frequency = random.Next(1, 11),
            Subject = subjects[random.Next(0, subjects.Count())],
            Languages = RandomValues(languages),
            Levels = RandomValues(levels)
        });
    }
    return result;
}

if (args.Length != 3)
{
    Console.WriteLine("Usage: Korepetynder.Data.Generator <connection string> <number of tutors> <number of lessons per tutor>");
    return 1;
}

var connectionString = args[0];
var numberOfTutors = int.Parse(args[1]);
var numberOfLessonsPerTutor = int.Parse(args[2]);

var contextOptions = new DbContextOptionsBuilder<KorepetynderDbContext>()
    .UseSqlServer(connectionString)
    .Options;

using var context = new KorepetynderDbContext(contextOptions);

var locations = await context.Locations.ToListAsync();
var subjects = await context.Subjects.ToListAsync();
var levels = await context.Levels.ToListAsync();
var languages = await context.Languages.ToListAsync();

var users = new List<User>(numberOfTutors);
for (int tutorNumber = 0; tutorNumber < numberOfTutors; tutorNumber++)
{
    var user = new User(Guid.NewGuid(), "Tutor", tutorNumber.ToString(), DateTime.UtcNow.AddYears(-(random.Next(18, 60))), "user@example.com", null)
    {
        Teacher = new Teacher
        {
            TeachingLocations = RandomValues(locations),
            Lessons = GenerateLessons(numberOfLessonsPerTutor, subjects, levels, languages)
        }
    };
    users.Add(user);
}

context.Users.AddRange(users);
await context.SaveChangesAsync();

return 0;
