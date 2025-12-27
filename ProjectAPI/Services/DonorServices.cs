using ProjectAPI.Dto;
using ProjectAPI.Models;
using ProjectAPI.Repository;

namespace ProjectAPI.Services
{
    public class DonorServices
    {
        private readonly DonorRepository _repository = new();
        public async Task< bool> CreateDonor(Donors d)
        {
           
            return await _repository.CreateDonor(d);
        }
        public async Task< List<Donors>> GetAllDonors()
        {
            return await _repository.GetAllDonors();
        }
        public async Task<Donors> GetDonorByID(int id)
        {
            return await _repository.GetDonorsByID(id);
        }
        public async Task<Donors> UpdateDonor(Donors d, int id)

        {
            
            return await _repository.UpdateDonor(d, id);
        }
        public async Task<bool> DeleteDonor(int id)
        {
            return await _repository.DeleteDonor(id);
        }

    }
}
