using Korepetynder.Contracts.Responses.Locations;
using Korepetynder.Contracts.Responses.Teachers;
using Korepetynder.Data.DbModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Korepetynder.Contracts.Responses.Students
{
    public class TeacherDataResponse
    {
        public string FullName { get; set; }
        public string? TelephoneNumber { get; set; }
        public string Email { get; set; }
        public IEnumerable<LocationResponse> Locations { get; set; }
        public int Age { get; set; }
        public IEnumerable<TeacherLessonResponse> Lessons { get; set; }

        public TeacherDataResponse(User teacher)
        {
            Email = teacher.Email;
            TelephoneNumber = teacher.TelephoneNumber;
            FullName = teacher.FullName;
            Age = teacher.Age;
            List<LocationResponse> locations = new List<LocationResponse>();
            foreach (var location in teacher.Teacher!.TeachingLocations)
            {
                locations.Add(new LocationResponse(location.Id, location.Name, location.ParentLocationId));
            }
            Locations = locations;
            List<TeacherLessonResponse> lessons = new List<TeacherLessonResponse>();
            foreach (var lesson in teacher.Teacher!.Lessons)
            {
                lessons.Add(new TeacherLessonResponse(lesson));
            }
            Lessons = lessons;

        } 

    }
}
