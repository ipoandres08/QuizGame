using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercise
{
    public sealed class LoginService
    {
        private Dictionary<string, User> credentials = new Dictionary<string, User>();

        private LoginService() { } // Prevent instantiation outside

        //1st try
        //private static readonly LoginService instance = new LoginService();
        //static LoginService() { } // Make sure it's truly lazy
        //public static LoginService Instance { get { return instance; } }

        //2nd try
        private static LoginService instance = null;
        public static LoginService Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new LoginService();
                }
                return instance;
            }
        }

        public void AddUser(User user)
        {
            credentials.Add(user.UserId, user);
            Console.WriteLine($"El user: {user.Name}");
        }

        public bool ValidateUser(string username, string password)
        {
            return username.Length >= 8 && password.Length >= 8;
        }

        public string GetUserId(string username)
        {
            foreach (var user in credentials.Values)
            {
                if(user.Name == username)
                {
                    return user.UserId;
                }
            }
            return "None";
        }

        public void RemoveUser(string userId) 
        { 
            credentials.Remove(userId);
        }
    }
}
