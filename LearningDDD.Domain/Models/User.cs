using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace LearningDDD.Domain.Models
{
    public class User
    {
        public Guid Id { get; set; }

        public string Password { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }
    }
}
