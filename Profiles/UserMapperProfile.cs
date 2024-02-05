using AutoMapper;
using base_dotnet.Databases.Entities;
using base_dotnet.DTOs;

namespace base_dotnet.Profiles
{
    public class UserMapperProfile : Profile
    {
        public UserMapperProfile()
        {
            CreateMap<User, UserDto>()
                .ForMember(
                    dest => dest.UserTypeName,
                    opt => opt.MapFrom(src => src.UserType.Name)
                );
        }
    }
}