using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Models
{
    public class LoginData
    {
        public LoginData(string username, string password)
        {
            Username = username;
            Password = password;
        }

        public string Username { get; set; }
        public string Password { get; set; }


    }
}
