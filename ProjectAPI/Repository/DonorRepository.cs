using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using ProjectAPI.Data;
using ProjectAPI.Models;

namespace ProjectAPI.Repository
{
    public class DonorRepository
    {

        LotteryContext context = LotteryContextFactory.CreateContext();

        public async Task<bool> CreateDonor(Donors d)

        {
            bool  existingDonor = context.Donors.Any(a => d.Phone == a.Phone);
            if (existingDonor)
                return false;
            await context.Donors.AddAsync(d);
            await context.SaveChangesAsync();
            return true;
        }
        public async Task<List<Donors>> GetAllDonors()
        {
            var listDonors = await context.Donors.Select(x => x).ToListAsync();
            return listDonors;
        }

        public async Task< Donors> GetDonorsByID(int id)
        {
            var donor = await context.Donors.FindAsync(id);
            if (donor == null)
                return null;

            return donor;
        }

        public async Task<Donors> UpdateDonor(Donors d, int id)
        {
            var existingDonor = await context.Donors.FindAsync(id);
            if (existingDonor == null)
                return null;
            existingDonor.Name = d.Name;
            existingDonor.Email = d.Email;
            existingDonor.Phone = d.Phone;
        
           await  context.SaveChangesAsync();
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

    }
}
