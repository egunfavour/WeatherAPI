using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Weather_API.Core.DTO;
using Weather_API.Domain.Models;

namespace Weather_API.Core.Utilities
{
    public class Web_APIProfile : Profile
    {
        public Web_APIProfile()
        {
            CreateMap<RegistrationDTO, AppUser>()
                 .ForMember(dest => dest.Email, act => act.MapFrom(src => src.Email.ToLower()))
                 .ForMember(dest => dest.UserName, act => act.MapFrom(src => src.Email.ToLower())).ReverseMap();
        }
    }
}
