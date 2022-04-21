using Korepetynder.Contracts.Requests.Students;
using Korepetynder.Contracts.Responses.Students;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Korepetynder.Services.Students
{
    public interface IStudentService
    {
        Task<StudentResponse> AddStudent(StudentCreationRequest request);
    }
}
