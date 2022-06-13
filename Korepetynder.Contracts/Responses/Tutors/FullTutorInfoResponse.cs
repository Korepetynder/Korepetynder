using Korepetynder.Data.DbModels;

namespace Korepetynder.Contracts.Responses.Tutors
{
    public class FullTutorInfoResponse
    {
        public TutorResponse Tutor { get; set; }
        public ICollection<TutorLessonResponse> Lessons { get; set; }

        public FullTutorInfoResponse(Tutor tutor, ICollection<TutorLesson> lessons)
        {
            this.Tutor = new TutorResponse(tutor.UserId, tutor.TeachingLocations.Select(location => location.Id), tutor.Score);
            this.Lessons = lessons.Select(lesson => new TutorLessonResponse(lesson)).ToList();
        }
    }
}
