using AutoMapper;
using ProjectAPI.DTOs;
using ProjectAPI.Models;

namespace ProjectAPI.Mapping
{
    public class UserProfile:Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserResponseDTO>();
            CreateMap<UserCreateDTO, User>();
            CreateMap<UserUpdateDTO,User>();
        }
    }
}
