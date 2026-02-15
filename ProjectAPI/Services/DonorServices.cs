using AutoMapper;
using Microsoft.Extensions.Logging;
using ProjectAPI.Dto;
using ProjectAPI.Models;
using ProjectAPI.Repository;
using ProjectFinal.Dto;
using ProjectFinal.Interfaces;
using System.Drawing;

namespace ProjectAPI.Services
{
    public class DonorServices : IDonorServicecs
    {
        private readonly IDonorRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<DonorServices> _logger;

        public DonorServices(IDonorRepository repository, IMapper mapper, ILogger<DonorServices> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<DonorResponseDTOs?> CreateDonor(DonorCreateDTOs d)
        {
            _logger.LogInformation("Creating donor with Name {Name}", d.Name);
            var createDonor = _mapper.Map<Donors>(d);
            var donor = await _repository.CreateDonor(createDonor);
            _logger.LogInformation("Donor created with ID {Id}", donor.Id);
            return _mapper.Map<DonorResponseDTOs>(donor);
        }

        public async Task<List<DonorResponseDTOs>> GetAllDonors()
        {
            _logger.LogInformation("Retrieving all donors");
            var ListDonors = await _repository.GetAllDonors();
            _logger.LogInformation("Retrieved {Count} donors", ListDonors.Count);
            return ListDonors.Select(d => _mapper.Map<DonorResponseDTOs>(d)).ToList();
        }

        public async Task<DonorResponseDTOs?> GetDonorById(int id)
        {
            _logger.LogInformation("Retrieving donor with ID {Id}", id);
            var donor = await _repository.GetDonorById(id);
            if (donor == null)
                _logger.LogWarning("Donor with ID {Id} not found", id);
            return donor != null ? _mapper.Map<DonorResponseDTOs>(donor) : null;
        }

        public async Task<DonorResponseDTOs> UpdateDonor(DonorUpdateDTOs d, int id)
        {
            _logger.LogInformation("Updating donor with ID {Id}", id);
            var existingDonor = await _repository.GetDonorById(id);

            if (existingDonor == null)
            {
                _logger.LogWarning("Donor with ID {Id} not found", id);
                return null;
            }

            if (d.Name != null) existingDonor.Name = d.Name;
            if (d.Email != null) existingDonor.Email = d.Email;
            if (d.Phone != null) existingDonor.Phone = d.Phone;
            if (d.City != null) existingDonor.City = d.City;
            if (d.Nieghbrhood != null) existingDonor.Nieghbrhood = d.Nieghbrhood;
            if (d.Street != null) existingDonor.Street = d.Street;

            var updateDonor = await _repository.UpdateDonor(existingDonor);
            _logger.LogInformation("Donor with ID {Id} updated successfully", id);

            return _mapper.Map<DonorResponseDTOs>(updateDonor);
        }

        public async Task<bool> DeleteDonor(int id)
        {
            _logger.LogInformation("Deleting donor with ID {Id}", id);
            var result = await _repository.DeleteDonor(id);
            if (result)
                _logger.LogInformation("Donor with ID {Id} deleted successfully", id);
            else
                _logger.LogWarning("Failed to delete donor with ID {Id}", id);
            return result;
        }

        public async Task<DonorResponseDTOs> FindDonorByGifts(int idGift)
        {
            _logger.LogInformation("Finding donor by Gift ID {GiftId}", idGift);
            var donor = await _repository.FindDonorByGifts(idGift);

            if (donor == null)
            {
                _logger.LogWarning("No donor found for Gift ID {GiftId}", idGift);
                return null;
            }

            _logger.LogInformation("Donor found for Gift ID {GiftId}: {DonorName}", idGift, donor.Name);
            return _mapper.Map<DonorResponseDTOs>(donor);
        }
    }
}
