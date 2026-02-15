using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using ProjectAPI.Data;
using ProjectAPI.Models;
using ProjectFinal.Dto;
using ProjectFinal.Interfaces;

namespace ProjectAPI.Repository
{
    public class DonorRepository : IDonorRepository
    {

        LotteryContext context;

        public DonorRepository(LotteryContext _context)
        {
            context = _context;
        }

        public async Task<Donors?> CreateDonor(Donors d)

        {
            await context.Donors.AddAsync(d);
            await context.SaveChangesAsync();
            return d;
        }
        public async Task<List<Donors?>> GetAllDonors()
        {
            var listDonors = await context.Donors.
                Include(c => c.Gifts).
                ToListAsync();
            return listDonors;
        }

        public async Task<Donors?> GetDonorById(int id)
        {
            var donor = await context.Donors.FirstOrDefaultAsync(c => c.Id == id);

            return donor;
        }

        public async Task<Donors?> UpdateDonor(Donors d)
        {
            var existingDonor = await context.Donors.FindAsync(d.Id);
            if (existingDonor == null)
                return null;
            existingDonor.Name = d.Name;
            existingDonor.Email = d.Email;
            existingDonor.Phone = d.Phone;
            existingDonor.City = d.City;
            existingDonor.Nieghbrhood = d.Nieghbrhood;
            existingDonor.Street = d.Street;
            existingDonor.Gifts = d.Gifts;


            await context.SaveChangesAsync();
            return existingDonor;
        }

        public async Task<bool> DeleteDonor(int id)
        {
            var existingdonor = await context.Donors.FindAsync(id);
            if (existingdonor == null)
                return false;

            context.Donors.Remove(existingdonor);
            await context.SaveChangesAsync();
            return true;

        }

        public async Task<Donors?> FindDonorByGifts(int id)
        {
            var donor = await context.Donors
                .Include(d => d.Gifts)
                .FirstOrDefaultAsync(d => d.Gifts.Any(g => g.Id == id));

            return donor;

        }
  
    }
}
