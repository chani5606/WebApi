using AutoMapper;
using ProjectAPI.DTOs;
using ProjectAPI.Models;

namespace ProjectAPI.Mapping
{
    public class BasketProfile:Profile
    {
        public BasketProfile()
        {
            CreateMap<Basket, BasketResponseDTO>()
             .ForMember(dest => dest.User,
                 opt => opt.MapFrom(src => src.User.FirstName))

             .ForMember(dest => dest.GiftName,
                 opt => opt.MapFrom(src => src.Gifts.Name))

             .ForMember(dest => dest.CategoryName,
                 opt => opt.MapFrom(src => src.Gifts.Catgory.Name))

             .ForMember(dest => dest.DonorName,
                 opt => opt.MapFrom(src => src.Gifts.Donor.Name))

             .ForMember(dest => dest.Price,
                 opt => opt.MapFrom(src => src.Gifts.Price))
              .ForMember(dest => dest.pathImage,
                 opt => opt.MapFrom(src => src.Gifts.PathImage ));
          
            CreateMap<BasketCreateDTO, Basket>();
            CreateMap<BasketUpdateDTO, Catgories>();
        }
    }
}
