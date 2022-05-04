using Korepetynder.Data.DbModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Korepetynder.Contracts.Responses.Teachers
{
    public class FullTeacherInfoResponse
    {
        public TeacherResponse Teacher { get; set; }
        public ICollection<TeacherLessonResponse> Lessons { get; set; }

        public FullTeacherInfoResponse(Teacher teacher, ICollection<TeacherLesson> lessons)
        {
            this.Teacher = new TeacherResponse(teacher.Id, teacher.TeachingLocations.Select(location => location.Id));
            this.Lessons = lessons.Select(lesson => new TeacherLessonResponse(lesson)).ToList();
        }
    }
}
