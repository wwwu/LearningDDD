using AutoMapper;
using LearningDDD.Application.ViewModels.User;
using LearningDDD.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace LearningDDD.Application.AutoMapper
{
    public class ViewModelToDomainMappingProfile : Profile
    {
        public ViewModelToDomainMappingProfile()
        {
            CreateMap<UserVM, User>();
        }
    }
}
