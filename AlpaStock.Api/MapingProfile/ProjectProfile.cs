using AlpaStock.Core.DTOs.Request.Auth;
using AlpaStock.Core.DTOs.Response.Auth;
using AlpaStock.Core.Entities;
using AutoMapper;


namespace AlpaStock.Api.MapingProfile
{
    public class ProjectProfile : Profile
    {
        public ProjectProfile()
        {
            CreateMap<ApplicationUser, DisplayFindUserDTO>().ReverseMap();
       
            
            CreateMap<ApplicationUser, UpdateUserDto>().ReverseMap();
         
        }
    }
}