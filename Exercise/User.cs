﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercise
{
    public class User
    {
        public string UserId { get; set; }
        public string Name { get; set; } 
        public string Password { get; set; }

        public User(string userId, string name, string password) { 
            UserId = userId;
            Name = name;
            Password = password;
        }
    }
}
