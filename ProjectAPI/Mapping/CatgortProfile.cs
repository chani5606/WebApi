using AutoMapper;
using ProjectAPI.DTOs;
using ProjectAPI.Models;

namespace ProjectAPI.Mapping
{
    public class CatgortProfile:Profile
    {
        public CatgortProfile()
        {
            CreateMap<Catgories, CategoryResponseDto>();
            CreateMap<CategoryCreateDto,Catgories>();
            CreateMap<CategoryUpdateDto, Catgories>();
        }
    }
}
