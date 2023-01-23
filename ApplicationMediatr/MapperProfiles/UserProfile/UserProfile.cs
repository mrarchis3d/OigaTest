using AutoMapper;
using Domain.DTOs.User;
using Domain.EntitiesModels;


namespace ApplicationMediatr.MapperProfiles.UserProfile
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {

            CreateMap<UserDTO, User>().ReverseMap();

            CreateMap<UserCreateUpdate, User>().ReverseMap();

        }
    }
}
