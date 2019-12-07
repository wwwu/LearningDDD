using System;
using System.Collections.Generic;
using System.Text;
using LearningDDD.Domain.Models;

namespace LearningDDD.Application.Dto.User
{
    public class CreateUserDto
    {
        public string Password { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public Address Address { get; set; }
    }
}
