using Korepetynder.Contracts.Requests.Students;
using Korepetynder.Contracts.Responses.Students;
using Korepetynder.Data;
using Korepetynder.Data.DbModels;
using Korepetynder.Services.Models;
using System;
using System.Collections.Generic;

using Microsoft.EntityFrameworkCore;
using Sieve.Models;
using Sieve.Services;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Korepetynder.Services.Students
{
    internal class StudentService : IStudentService
    {
        private readonly KorepetynderDbContext _korepetynderDbContext;
        private readonly ISieveProcessor _sieveProcessor;

        public StudentService(KorepetynderDbContext korepetynderDbContext, ISieveProcessor sieveProcessor)
        {
            _korepetynderDbContext = korepetynderDbContext;
            _sieveProcessor = sieveProcessor;
        }

        public async Task<StudentResponse> AddStudent(StudentCreationRequest request)
        {
            Guid currentId = new Guid(); //tu musi być id, nie wiem skąd
            var studentUser = await _korepetynderDbContext.Users.Where(user => user.Id == currentId).SingleAsync();
            if (studentUser.Student is not null)
            {
                throw new InvalidOperationException("User with id: " + currentId + " already is a student");
            }
            var student = new Student
            {
                PreferredCostMinimum = request.MinimalCost,
                PreferredCostMaximum = request.MaximalCost,
            };
            var locations = await _korepetynderDbContext.Locations.Where(location => request.Locations.Contains(location.Id)).ToListAsync();
            if (locations.Count != request.Locations.Count)
            {
                throw new ArgumentException("Location does not exists");
            }
            student.PreferredLocations = locations;
            studentUser.Student = student;
            _korepetynderDbContext.Students.Add(student);
            await _korepetynderDbContext.SaveChangesAsync();
            return new StudentResponse(student.Id);
        }

    }
}
