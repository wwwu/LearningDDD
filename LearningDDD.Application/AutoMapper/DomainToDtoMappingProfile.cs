using AutoMapper;
using LearningDDD.Application.Dto.User;
using LearningDDD.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace LearningDDD.Application.AutoMapper
{
    public class EntityToDtoMappingProfile: Profile
    {
        public EntityToDtoMappingProfile()
        {
            CreateMap<User, UserDto>();
        }
    }
}
