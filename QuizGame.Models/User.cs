using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizGame.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public User(int userId, string userName, string firstName, string lastName)
        {
            UserId = userId;
            UserName = userName;
            FirstName = firstName;
            LastName = lastName;
        }
    }
}
