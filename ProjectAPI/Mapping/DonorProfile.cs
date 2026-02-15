using AutoMapper;
using ProjectAPI.Models;
using ProjectFinal.Dto;

namespace ProjectAPI.Mapping
{
    public class DonorProfile: Profile
    {
        public DonorProfile()
        {
             CreateMap<Donors, DonorResponseDTOs>();
             CreateMap<DonorCreateDTOs, Donors>();
            CreateMap<DonorUpdateDTOs, Donors>();
        }
    }
}
