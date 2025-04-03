using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Authentification.JWT.DAL.Models;
using Authentification.JWT.Service.DTOs;
using AutoMapper;

namespace Authentification.JWT.Service.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UserDto, User>()
    .ForMember(dest => dest.PasswordHash, opt => opt.MapFrom(src => src.Password));

            CreateMap<User, UserDto>()
                .ForMember(dest => dest.Password, opt => opt.Ignore()); // Sécurité : ne jamais exposer le hash

        }
    }   
}
