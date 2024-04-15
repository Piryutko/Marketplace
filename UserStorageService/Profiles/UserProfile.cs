using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using UserStorageService.Dtos;
using UserStorageService.Models;

namespace UserStorageService.Profiles
{
    public class UserProfile : Profile
    {
       public UserProfile()
       {
            CreateMap<UserPublishedDto, User>().ForMember(dest => dest.Id, opt => opt.MapFrom(scr => scr.Id));
       }
    }
}