using AutoMapper;
using Domain.DTOs.User;
using Domain.EntitiesModels;
using Domain.Utils;

namespace ApplicationMediatr.MapperProfiles.UserProfile
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {

            CreateMap<UserDTO, User>().ReverseMap();
            CreateMap<PagedResult<UserDTO>, PagedResult<User>>().ReverseMap();
            CreateMap<UserCreateUpdate, User>().ReverseMap();

        }
    }
}
