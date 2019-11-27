using System;
using System.Collections.Generic;
using System.Text;
using LearningDDD.Domain.Models.Base;

namespace LearningDDD.Domain.Models
{
    public class User : BaseEntity
    {
        public string Password { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public Address Address { get; set; }
    }
}
