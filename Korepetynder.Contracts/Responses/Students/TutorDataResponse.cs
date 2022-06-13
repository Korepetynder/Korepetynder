using Korepetynder.Contracts.Responses.Locations;
using Korepetynder.Contracts.Responses.Tutors;
using Korepetynder.Data.DbModels;

namespace Korepetynder.Contracts.Responses.Students
{
    public class TutorDataResponse
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public string? PhoneNumber { get; set; }
        public string Email { get; set; }
        public IEnumerable<LocationResponse> Locations { get; set; }
        public int Age { get; set; }
        public IEnumerable<TutorLessonResponse> Lessons { get; set; }
        public decimal Score { get; set; }

        public TutorDataResponse(User tutor)
        {
            Id = tutor.Id;
            Email = tutor.Email;
            PhoneNumber = tutor.PhoneNumber;
            FullName = tutor.FullName;
            Score = tutor.Tutor!.Score;
            //Age = tutor.Age;
            Age = 20;
            List<LocationResponse> locations = new List<LocationResponse>();
            foreach (var location in tutor.Tutor!.TeachingLocations)
            {
                locations.Add(new LocationResponse(location.Id, location.Name, location.ParentLocationId));
            }
            Locations = locations;
            List<TutorLessonResponse> lessons = new List<TutorLessonResponse>();
            foreach (var lesson in tutor.Tutor!.TutorLessons)
            {
                lessons.Add(new TutorLessonResponse(lesson));
            }
            Lessons = lessons;

        }

    }
}
