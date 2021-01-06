using System.Linq;
using API.DTOs;
using API.Entities;
using API.Extensions;
using AutoMapper;

namespace API.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            //SourceMemberNamingConvention = new LowerUnderscoreNamingConvention();
            //DestinationMemberNamingConvention = new PascalCaseNamingConvention();

            CreateMap<Employee, EmployeeDto>()
                    .ForMember(dest => dest.PhotoUrl, 
                                opt => opt.MapFrom(src => src.Photos.FirstOrDefault(x => x.IsMain).Url))
                    .ForMember(dest => dest.Age, opt => opt.MapFrom(src => src.DateOfBirth.CalculateAge())).ReverseMap();

            CreateMap<UserPhoto, UserPhotoDto>();

            CreateMap<EmployeeUpdateDto, Employee>();
                
        }
    }
}