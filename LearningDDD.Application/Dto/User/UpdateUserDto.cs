using System;
using System.Collections.Generic;
using System.Text;
using LearningDDD.Domain.Models;

namespace LearningDDD.Application.Dto.User
{
    public class UpdateUserDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public Address Address { get; set; }
    }
}
