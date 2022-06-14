using Korepetynder.Data;
using Korepetynder.Data.DbModels;
using Microsoft.EntityFrameworkCore;

Random random = new();

var images = new List<string>
{
    "https://elearningindustry.com/wp-content/uploads/2019/10/professional-development-tools-for-teachers.jpg",
    "https://www.portaloswiatowy.pl/appFiles/site_8/images/doc/f4eW6xOBu8Ig2E7.jpeg",
    "https://emp-scs-uat.img-osdw.pl/img-p/1/kipwn/d576082e/std/2bc-452/734349414o.jpg",
    "https://media.geeksforgeeks.org/wp-content/uploads/00nfa.png"
};

IList<T> RandomValues<T>(IList<T> values)
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

ICollection<TutorLesson> GenerateLessons(int numberOfLessons, IList<Subject> subjects, IList<Level> levels, IList<Language> languages)
{
    var result = new List<TutorLesson>();
    var generatedNumberOfLessons = random.Next(1, numberOfLessons + 1);
    var subjectsToUse = RandomValues(subjects);
    for (int i = 0; i < Math.Min(generatedNumberOfLessons, subjectsToUse.Count); i++)
    {
        result.Add(new TutorLesson
        {
            Cost = random.Next(20, 101),
            Frequency = random.Next(1, 11),
            Subject = subjectsToUse[i],
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
    var userId = Guid.NewGuid();
    var user = new User(userId, "Tutor", tutorNumber.ToString(), DateTime.UtcNow.AddYears(-(random.Next(18, 60))), "user@example.com", "+48123456789")
    {
        Tutor = new Tutor(userId)
        {
            TeachingLocations = RandomValues(locations),
            TutorLessons = GenerateLessons(numberOfLessonsPerTutor, subjects, levels, languages),
            MultimediaFiles = RandomValues(images).Select(image => new MultimediaFile(image)).ToList()
        }
    };
    users.Add(user);
}

context.Users.AddRange(users);
await context.SaveChangesAsync();

return 0;
