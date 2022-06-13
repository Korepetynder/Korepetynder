using Korepetynder.Contracts.Responses.Locations;
using Korepetynder.Contracts.Responses.Media;
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

        public IEnumerable<MultimediaFileResponse> MultimediaFiles { get; set; }

        public TutorDataResponse(User tutor)
        {
            Id = tutor.Id;
            Email = tutor.Email;
            PhoneNumber = tutor.PhoneNumber;
            FullName = tutor.FullName;
            Score = tutor.Tutor!.Score;
            //Age = tutor.Age;
            Age = 20;
            var locations = new List<LocationResponse>();
            foreach (var location in tutor.Tutor!.TeachingLocations)
            {
                locations.Add(new LocationResponse(location.Id, location.Name, location.ParentLocationId));
            }
            Locations = locations;
            var lessons = new List<TutorLessonResponse>();
            var multimediaFiles = new HashSet<MultimediaFileResponse>();
            foreach (var lesson in tutor.Tutor!.TutorLessons)
            {
                lessons.Add(new TutorLessonResponse(lesson));
                foreach (var multimediaFile in lesson.MultimediaFiles)
                {
                    multimediaFiles.Add(new MultimediaFileResponse(multimediaFile.Id, multimediaFile.Url,
                        multimediaFile.TutorLessons.Select(tutorLesson => tutorLesson.Id)));
                }
            }
            Lessons = lessons;
            foreach (var multimediaFile in tutor.Tutor!.MultimediaFiles)
            {
                multimediaFiles.Add(new MultimediaFileResponse(multimediaFile.Id, multimediaFile.Url,
                    multimediaFile.TutorLessons.Select(tutorLesson => tutorLesson.Id)));
            }
            MultimediaFiles = multimediaFiles;
        }

    }
}
