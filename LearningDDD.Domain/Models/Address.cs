using LearningDDD.Domain.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace LearningDDD.Domain.Models
{
    public class Address : ValueObject<Address>
    {
        public Address(string province, string city, string streetAndNumber)
        {
            Province = province;
            City = city;
            StreetAndNumber = streetAndNumber;
        }

        public string Province { get; set; }

        public string City { get; set; }

        public string StreetAndNumber { get; set; }
    }
}
