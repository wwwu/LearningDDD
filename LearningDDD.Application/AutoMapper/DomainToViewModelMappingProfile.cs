using AutoMapper;
using LearningDDD.Application.ViewModels;
using LearningDDD.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace LearningDDD.Application.AutoMapper
{
    public class DomainToViewModelMappingProfile: Profile
    {
        public DomainToViewModelMappingProfile()
        {
            CreateMap<User, UserVM>();
        }
    }
}
