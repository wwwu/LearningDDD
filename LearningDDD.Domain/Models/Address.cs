using LearningDDD.Domain.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace LearningDDD.Domain.Models
{
    public class Address : ValueObject<Address>
    {
        public Address()
        {
        }

        public Address(string province, string city, string streetAndNumber)
        {
            Province = province;
            City = city;
            StreetAndNumber = streetAndNumber;
        }

        public string Province { get; set; }

        public string City { get; set; }

        public string StreetAndNumber { get; set; }

        /// <summary>
        /// 拼接完整地址 广东省,深圳市,XXXX
        /// </summary>
        /// <returns></returns>
        public string GetFullAddress()
        {
            var list = new List<string> { Province, City, StreetAndNumber };
            list.RemoveAll(s => string.IsNullOrWhiteSpace(s));
            return string.Join('，', list);
        }
    }
}
