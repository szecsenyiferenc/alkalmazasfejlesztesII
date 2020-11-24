using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Core.Models.Input
{
    public class CustomerInputDto
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }

        public CustomerInputDto()
        {

        }

        public CustomerInputDto(string username, string password)
        {
            Username = username;
            Password = password;
        }
    }
}
