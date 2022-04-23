using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Korepetynder.Contracts.Requests.User
{
    public class UserCreationRequest
    {

        [MaxLength(50)]
        public string FirstName { get; set; }
        [MaxLength(50)]
        public string LastName { get; set; }
        [MaxLength(100)]
        public string UserName { get; set; }
        public int Age { get; set; }
        public UserCreationRequest(string firstName, string lastName, string userName, int age)
        {
            FirstName = firstName;
            LastName = lastName;
            UserName = userName;
            Age = age;
        }
    }
}
