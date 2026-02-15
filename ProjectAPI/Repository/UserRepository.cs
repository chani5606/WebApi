using Microsoft.EntityFrameworkCore;
using ProjectAPI.Data;
using ProjectAPI.Models;

namespace ProjectAPI.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly LotteryContext context;

        public UserRepository(LotteryContext _context)
        {
            context = _context;
        }

        public async Task<User> CreateUser(User user)
        {
            context.Users.Add(user);
            await context.SaveChangesAsync();
            return user;
        }

        public async Task<List<User>> GetAllUsers()
        {
            var listUsers = await context.Users.
                Include(u => u.Gifts).
                ToListAsync();

            return listUsers;
        }

        public async Task<User?> GetUsersByID(int id)
        {
            var user = await context.Users.
                FirstOrDefaultAsync(c => c.Id == id);


            return user;
        }

        public async Task<User?> UpdateGifts(User user)
        {
            var existingUser = await context.Users.FindAsync(user.Id);
            if (existingUser == null)
                return null;
            if (existingUser.FirstName != null) existingUser.FirstName = user.FirstName;
            if (existingUser.LastName != null) existingUser.LastName = user.LastName;
            if (existingUser.Email != null) existingUser.Email = user.Email;
            if (existingUser.Phone != null) existingUser.Phone = user.Phone;
            if (existingUser.City != null) existingUser.City = user.City;
            if (existingUser.Nieghbrhood != null) existingUser.Nieghbrhood = user.Nieghbrhood;
            if (existingUser.Street != null) existingUser.Street = user.Street;


            await context.SaveChangesAsync();
            return existingUser;
        }

        public async Task<bool> DeleteUser(int id)
        {
            var existingUser = await context.Users.FindAsync(id);
            if (existingUser == null)
                return false;

            context.Users.Remove(existingUser);
            await context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> EmailExists(string email)
        {
            var user = await context.Users.
                FirstOrDefaultAsync(c => c.Email == email);
            if (user != null)
            {
                return true;
            }
            return false;
        }
        public async Task<User?> GetByEmail(string email)
        {
            return await context.Users
                .FirstOrDefaultAsync(u => u.Email == email);
        }




    }
}

