using AutoMapper;
using ProjectAPI.Dto;
using ProjectAPI.Models;

namespace ProjectAPI.Mapping
{
    public class GiftProfile : Profile
    {


        public GiftProfile()
        {
            CreateMap<Gifts, GifttResponseDTOs>()
            .ForMember(dest => dest.nameDonors,
            opt => opt.MapFrom(src => src.Donor.Name))
            .ForMember(dest => dest.CategoryName,
             opt => opt.MapFrom(src => src.Catgory.Name));
            CreateMap<GiftCreateDTOs, Gifts>();
            CreateMap<GiftUpdateDTOs, Gifts>();
        }
    } 
}
