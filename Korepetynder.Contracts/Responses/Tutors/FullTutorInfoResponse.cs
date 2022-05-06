using Korepetynder.Data.DbModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Korepetynder.Contracts.Responses.Tutors
{
    public class FullTutorInfoResponse
    {
        public TutorResponse Teacher { get; set; }
        public ICollection<TutorLessonResponse> Lessons { get; set; }

        public FullTutorInfoResponse(Tutor tutor, ICollection<TutorLesson> lessons)
        {
            this.Teacher = new TutorResponse(tutor.UserId, tutor.TeachingLocations.Select(location => location.Id));
            this.Lessons = lessons.Select(lesson => new TutorLessonResponse(lesson)).ToList();
        }
    }
}
