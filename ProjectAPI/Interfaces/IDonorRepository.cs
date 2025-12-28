using ProjectFinal.Dto;

namespace ProjectFinal.Interfaces
{
    public interface IDonorRepository
    {
        Task<bool> CreateDonor(DonorCreateDto donor);
        Task<List<DonorResponseDto>> GetAllDonors();
        Task<DonorResponseDto> GetDonorById(int id);
        Task<DonorResponseDto> UppdateDonor(DonorUpdateDto donor, int id);
        Task<bool> DeletDonor(int id);
    }
}
