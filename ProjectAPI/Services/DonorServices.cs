using ProjectAPI.Dto;
using ProjectAPI.Models;
using ProjectAPI.Repository;
using ProjectFinal.Dto;
using ProjectFinal.Interfaces;

namespace ProjectAPI.Services
{
    public class DonorServices : IDonorServicecs
    {
        private readonly DonorRepository _repository;
        public DonorServices(DonorRepository repository)
        {
            _repository = repository;
        }
        public async Task<DonorResponseDTOs?> CreateDonor(DonorCreateDTOs d)
        {
            var createDonor = new Donors
            {
                Name = d.Name,
                Email = d.Email,
                Phone = d.Phone,
                City = d.City,
                Nieghbrhood = d.Nieghbrhood,
                Street = d.Street
            };
            var donor = await _repository.CreateDonor(createDonor);
            return MapToDto(donor);

        }
        public async Task<List<DonorResponseDTOs>> GetAllDonors()
        {
            var ListDonors = await _repository.GetAllDonors();
            return ListDonors.Select(d => MapToDto(d)).ToList();
        }
        public async Task<DonorResponseDTOs?> GetDonorById(int id)
        {
            var donor = await _repository.GetDonorById(id);
            return MapToDto(donor);
        }
        public async Task<DonorResponseDTOs> UpdateDonor(DonorUpdateDTOs d, int id)

        {
            var existingDonor = await _repository.GetDonorById(id);

            if (existingDonor == null) return null;
            if (d.Name != null) existingDonor.Name = d.Name;
            if (d.Email != null) existingDonor.Email = d.Email;
            if (d.Phone != null) existingDonor.Phone =d.Phone;
            if (d.City != null) existingDonor.City = d.City;
            if (d.Nieghbrhood != null) existingDonor.Nieghbrhood = d.Nieghbrhood;
            if (d.Street != null) existingDonor.Street = d.Street;
             var updateDonor = await _repository.UpdateDonor(existingDonor);
            return MapToDto(updateDonor);
        }
        public async Task<bool> DeleteDonor(int id)
        {
            return await _repository.DeleteDonor(id);
        }

        public static DonorResponseDTOs MapToDto(Donors donor)
        {
            return new DonorResponseDTOs
            {
                Id = donor.Id,
                Name = donor.Name,
                Email = donor.Email,
                Phone = donor.Phone,
                City = donor.City,
                Nieghbrhood = donor.Nieghbrhood,
                Street = donor.Street
            };
        }

       public async Task<DonorResponseDTOs> FindDonorByGifts(int idGift)
        {
            var donor = await _repository.FindDonorByGifts(idGift);
            if (donor == null) return null;
            return MapToDto(donor);
        }
    }
}
