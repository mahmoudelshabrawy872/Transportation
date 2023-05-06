using AutoMapper;
using Microsoft.AspNetCore.Identity;
using TransportationAPI.Models;
using TransportationAPI.Models.Dto.CarsDto;
using TransportationAPI.Models.Dto.UserDto;

namespace TransportationAPI.Helper
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            CreateMap<UserDto, IdentityUser>().ReverseMap();
            CreateMap<Car, CarDto>().ReverseMap();
            CreateMap<Car, CarUpdateDto>().ReverseMap();




        }
    }
}
