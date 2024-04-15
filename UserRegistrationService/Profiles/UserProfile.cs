using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using UserRegistrationService.Dtos;
using UserRegistrationService.Models;

namespace UserRegistrationService.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserPublishedDto>().ForMember(dest => dest.Id, opt => opt.MapFrom(scr => scr.Id));
        }
    }
}