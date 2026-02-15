using AutoMapper;
using ProjectAPI.DTOs;
using ProjectAPI.Models;

namespace ProjectAPI.Mapping
{
    public class WinnerProfile : Profile
    {
        public WinnerProfile()
        {
            // DTO ➜ Entity (ליצירה)
            CreateMap<WinnerCreatedDTO, Winner>()
                .ForMember(dest => dest.User, opt => opt.Ignore())
                .ForMember(dest => dest.Gift, opt => opt.Ignore());
                //.ForMember(dest => dest.WinDate, opt => opt.MapFrom(_ => DateTime.Now));

            // Entity ➜ DTO (לתשובה)
            CreateMap<Winner, WinnerResponseDTO>()
                .ForMember(dest => dest.Gift,
                    opt => opt.MapFrom(src => src.Gift.Name))
                .ForMember(dest => dest.UserName,
                    opt => opt.MapFrom(src => src.User.FirstName));
        }
    }
}
