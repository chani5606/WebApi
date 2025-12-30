using ProjectFinal.Dto;

namespace ProjectFinal.Interfaces

{
    public interface IDonorServicecs
    {

        Task<DonorResponseDTOs?> CreateDonor(DonorCreateDTOs d);
        Task<List<DonorResponseDTOs>> GetAllDonors();
        Task<DonorResponseDTOs?> GetDonorById(int id);
        Task<DonorResponseDTOs?> UpdateDonor(DonorUpdateDTOs dto, int id);
        Task<bool> DeleteDonor(int id);

        Task<DonorResponseDTOs?> FindDonorByGifts(int idGift);             

    }
}
