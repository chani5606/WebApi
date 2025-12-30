using ProjectAPI.Models;
using ProjectFinal.Dto;

namespace ProjectFinal.Interfaces
{
    public interface IDonorRepository
    {
        Task<Donors?> CreateDonor(Donors donor);
        Task<List<Donors?>> GetAllDonors();
        Task<Donors?> GetDonorById(int id);
        Task<Donors?> UpdateDonor(Donors donor);
        Task<bool> DeleteDonor(int id);

        Task<Donors?> FindDonorByGifts(int idGifts);

    }
}
