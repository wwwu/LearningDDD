using AutoMapper;
using LearningDDD.Application.ViewModels.User;
using LearningDDD.Domain.Commands.User;
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
            CreateMap<UserVM, CreateUserCommand>()
                .ConvertUsing(s => new CreateUserCommand(s.Id, s.Password, s.Name, s.Email
                    , s.Address.City, s.Address.Province, s.Address.StreetAndNumber));
            CreateMap<CreateUserCommand, User>();
        }
    }
}
