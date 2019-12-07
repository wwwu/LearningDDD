using AutoMapper;
using LearningDDD.Application.Dto.User;
using LearningDDD.Domain.Commands.User;
using LearningDDD.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace LearningDDD.Application.AutoMapper
{
    public class DtoToDomainMappingProfile : Profile
    {
        public DtoToDomainMappingProfile()
        {
            CreateMap<UserDto, User>();
            CreateMap<CreateUserDto, User>();
            CreateMap<UpdateUserDto, User>();
            CreateMap<CreateUserCommand, User>();

            CreateMap<CreateUserDto, CreateUserCommand>()
                .ConvertUsing(s => new CreateUserCommand(s.Password, s.Name, s.Email
                    , s.Address.City, s.Address.Province, s.Address.StreetAndNumber));
            CreateMap<UpdateUserDto, UpdateUserCommand>()
                .ConvertUsing(s => new UpdateUserCommand(s.Id, s.Name, s.Address.City
                    , s.Address.Province, s.Address.StreetAndNumber));
        }
    }
}
